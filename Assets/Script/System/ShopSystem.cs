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
        public BindableProperty<string> CommodityDescription { get; }

        public DicePropertyData UpdateOneCommodity(int index);
    }
    public class ShopSystem : AbstractSystem, IShopSystem
    {
        public BindableProperty<int> Money { get; private set; } = new BindableProperty<int>();
        public BindableProperty<int> RefreshCost { get; private set; } = new BindableProperty<int>();
        public List<PurchasedPropertyData> purchasedPropertyDataList = new List<PurchasedPropertyData>(6);
        public List<DicePropertyData> dicePropertyDataList = new List<DicePropertyData>(3);

        public BindableProperty<string> CommodityDescription { get; private set; } = new BindableProperty<string>("");

        #region  Model
        IDicePropertyDataConfigurationModel dicePropertyDataConfigurationModel;
        #endregion

        protected override void OnInit()
        {
            var data = Addressables.LoadAssetAsync<ShopData>("ShopData");
            data.WaitForCompletion();
            Money.Value = data.Result.initialMoney;
            RefreshCost.Value = data.Result.refreshCost;

            dicePropertyDataConfigurationModel = this.GetModel<IDicePropertyDataConfigurationModel>();
        }

        public DicePropertyData UpdateOneCommodity(int index)
        {
            if (index >= 3 || index < 0)
            {
#if UNITY_EDITOR
                Debug.LogError("商品索引错误");
#endif          
                return null;
            }
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
            dicePropertyDataList[index] = dicePropertyDataConfigurationModel.DataList[randomNum];
            return dicePropertyDataList[index];
        }
    }
    public class PurchasedPropertyData
    {
        public string spritePath;
        public int sellingPrice;
        public string description;
    }
}