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
        /// <summary>
        ///  已购买的骰子道具
        /// </summary>
        public List<DiceItemData> purchasedDiceItemDataList = new List<DiceItemData>(6) { null, null, null, null, null, null };
        /// <summary>
        /// 正在销售的骰子道具
        /// </summary>
        public List<DiceItemData> diceCommodityDataList = new List<DiceItemData>(3) { null, null, null };

        public BindableProperty<string> InformationBoxDescription { get; private set; } = new BindableProperty<string>("");

        #region  Model
        IDiceItemDataConfigurationModel diceItemDataConfigurationModel;
        #endregion

        #region  数据缓存
        private string moneyIsShort; //缺钱
        private string purchasedMax; //该道具达最大购买数量，无法再购买
        private string maximumQuantity; //购买的骰面道具数量达到最大，不能再继续购买
        #endregion
        protected override void OnInit()
        {
            var data = Addressables.LoadAssetAsync<ShopData>("ShopData");
            data.WaitForCompletion();
            Money.Value = data.Result.initialMoney;
            RefreshCost.Value = data.Result.refreshCost;

            moneyIsShort = data.Result.moneyIsShort;
            purchasedMax = data.Result.purchasedTwo;
            maximumQuantity = data.Result.maximumQuantity;

            diceItemDataConfigurationModel = this.GetModel<IDiceItemDataConfigurationModel>();
        }


        private DiceItemData GetOnePurchasableCommodityData()
        {
            int randomNum = UnityEngine.Random.Range(0, diceItemDataConfigurationModel.DataList.Count);
            int count = 0;
            while (diceItemDataConfigurationModel.DataList[randomNum].purchaseTimes <= 0)
            {
                randomNum = UnityEngine.Random.Range(0, diceItemDataConfigurationModel.DataList.Count);
                count++;
                if (count > 15)
                {
#if UNITY_EDITOR
                    Debug.LogError("获取可购买商品失败");
#endif
                    return null;
                }
            }
            return diceItemDataConfigurationModel.DataList[randomNum];
        }

        public void PlayerBuyCommodity(int index)
        {
            if (diceCommodityDataList[index].purchaseTimes <= 0)
            {
                InformationBoxDescription.Value = purchasedMax;
                return;
            }
            if (diceCommodityDataList[index].purchasePrice > Money.Value)
            {
                InformationBoxDescription.Value = moneyIsShort;
                return;
            }
            for (int i = 0; i < purchasedDiceItemDataList.Count; ++i)
            {
                if (purchasedDiceItemDataList[i] == null)
                {
                    purchasedDiceItemDataList[i] = diceCommodityDataList[index];
                    break;
                }
                if (i == purchasedDiceItemDataList.Count - 1)
                {
                    InformationBoxDescription.Value = maximumQuantity;
                    return;
                }
            }
            this.SendEvent<BuyCommodityEvent>(new BuyCommodityEvent(diceCommodityDataList[index]));
            diceCommodityDataList[index].purchaseTimes -= 1;
            Money.Value -= diceCommodityDataList[index].purchasePrice;



            DiceItemData data = GetOnePurchasableCommodityData();
            diceCommodityDataList[index] = data;

            this.SendEvent<RefreshSellingCommodity>(new RefreshSellingCommodity(index, data));
        }
        /// <summary>
        /// 游戏开始时初始化商品栏
        /// </summary>
        public void InitCommodity()
        {
            for (int i = 0; i < 3; ++i)
            {
                DiceItemData data = GetOnePurchasableCommodityData();
                diceCommodityDataList[i] = data;

                this.SendEvent<RefreshSellingCommodity>(new RefreshSellingCommodity(i, data));
            }
        }

        public void PlayerSellCommodity()
        {

        }
    }
    /// <summary>
    /// 购买商品事件
    /// </summary>
    public struct BuyCommodityEvent
    {
        public DiceItemData dicePropertyData;
        public BuyCommodityEvent(DiceItemData data)
        {
            dicePropertyData = data;
        }
    }
    /// <summary>
    /// 购买商品后，刷新商品栏商品事件
    /// </summary>
    public struct RefreshSellingCommodity
    {
        public int index;
        public DiceItemData newData;

        public RefreshSellingCommodity(int index, DiceItemData newData)
        {
            this.index = index;
            this.newData = newData;
        }
    }
}