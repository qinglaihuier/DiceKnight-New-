using System.Collections.Generic;
using Model;
using OfficeOpenXml.FormulaParsing.Excel.Functions.RefAndLookup;
using QFramework;
using ScriptableObjectData;
using UnityEngine.AddressableAssets;
using UnityEngine;
namespace System
{
    public interface IShopSystem : ISystem
    {
        public BindableProperty<int> Money { get; }
        public BindableProperty<int> RefreshCost { get; }
        public BindableProperty<string> InformationBoxDescription { get; }

        public void PlayerBuyCommodity(int index);
        public void PlayerSellCommodity();
        public void InitCommodity();
    }
    public class ShopSystem : AbstractSystem, IShopSystem
    {
        public BindableProperty<int> Money { get; private set; } = new BindableProperty<int>();
        public BindableProperty<int> RefreshCost { get; private set; } = new BindableProperty<int>();
        public List<PurchasedPropertyData> purchasedPropertyDataList = new List<PurchasedPropertyData>(6);
        public List<DicePropertyData> dicePropertyDataList = new List<DicePropertyData>(3) { null, null, null };

        public BindableProperty<string> InformationBoxDescription { get; private set; } = new BindableProperty<string>("");

        #region  Model
        IDicePropertyDataConfigurationModel dicePropertyDataConfigurationModel;
        #endregion

        #region  数据缓存
        private string moneyIsShort;
        private string purchasedTwo;
        #endregion
        protected override void OnInit()
        {
            var data = Addressables.LoadAssetAsync<ShopData>("ShopData");
            data.WaitForCompletion();
            Money.Value = data.Result.initialMoney;
            RefreshCost.Value = data.Result.refreshCost;

            moneyIsShort = data.Result.moneyIsShort;
            purchasedTwo = data.Result.purchasedTwo;

            dicePropertyDataConfigurationModel = this.GetModel<IDicePropertyDataConfigurationModel>();
        }


        private DicePropertyData GetOnePurchasableCommodityData()
        {
            int randomNum = UnityEngine.Random.Range(0, dicePropertyDataConfigurationModel.DataList.Count);
            int count = 0;
            while (dicePropertyDataConfigurationModel.DataList[randomNum].purchaseTimes <= 0)
            {
                randomNum = UnityEngine.Random.Range(0, dicePropertyDataConfigurationModel.DataList.Count);
                count++;
                if (count > 15)
                {
#if UNITY_EDITOR
                    Debug.LogError("获取可购买商品失败");
#endif
                    return null;
                }
            }
            return dicePropertyDataConfigurationModel.DataList[randomNum];
        }

        public void PlayerBuyCommodity(int index)
        {
            if (dicePropertyDataList[index].purchaseTimes <= 0)
            {
                InformationBoxDescription.Value = purchasedTwo;
                return;
            }
            if (dicePropertyDataList[index].purchasePrice > Money.Value)
            {
                InformationBoxDescription.Value = moneyIsShort;
                return;
            }
            this.SendEvent<BuyCommodityEvent>(new BuyCommodityEvent(dicePropertyDataList[index]));
            dicePropertyDataList[index].purchaseTimes -= 1;
            DicePropertyData data = GetOnePurchasableCommodityData();
            dicePropertyDataList[index] = data;

            this.SendEvent<RefreshSellingCommodity>(new RefreshSellingCommodity(index, data));
        }
        public void InitCommodity()
        {
            for (int i = 0; i < 3; ++i)
            {
                DicePropertyData data = GetOnePurchasableCommodityData();
                dicePropertyDataList[i] = data;

                this.SendEvent<RefreshSellingCommodity>(new RefreshSellingCommodity(i, data));
            }
        }

        public void PlayerSellCommodity()
        {

        }
    }
    public struct BuyCommodityEvent
    {
        public DicePropertyData dicePropertyData;
        public BuyCommodityEvent(DicePropertyData data)
        {
            dicePropertyData = data;
        }
    }
    public struct RefreshSellingCommodity
    {
        public int index;
        public DicePropertyData newData;

        public RefreshSellingCommodity(int index, DicePropertyData newData)
        {
            this.index = index;
            this.newData = newData;
        }
    }
    public class PurchasedPropertyData
    {
        public string spritePath;
        public int sellingPrice;
        public string description;
    }
}