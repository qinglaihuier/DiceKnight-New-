using System;
using System.Collections;
using System.Collections.Generic;
using Model;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using QFramework;
using UnityEngine.AddressableAssets;
using Unity.VisualScripting;
namespace ViewController
{
    public class Commodity : AbstractViewController, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
    {

        #region  System
        private IShopSystem shopSystem;
        #endregion

        #region 依赖组件
        private Image image;
        private TextMeshProUGUI priceText;
        #endregion

        #region  数据缓存
        private string descirption;
        [NonSerialized] public int index; //父物体的第几个子对象
        #endregion

        private void Awake()
        {
            image = GetComponent<Image>();
            priceText = GetComponentInChildren<TextMeshProUGUI>();
        }
        private void Start()
        {
            shopSystem = this.GetSystem<IShopSystem>();
            //TODO : 初始化
        }
        public void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Left)
            {
                shopSystem.PlayerBuyCommodity(index);
                //TODO : 触发购买逻辑，通知Dice等
                // DicePropertyData dicePropertyData = shopSystem.GetOnePurchasableCommodityData(index);



                // Sprite sprite = Addressables.LoadAssetAsync<Sprite>(dicePropertyData.spritePath).WaitForCompletion();
                // image.sprite = sprite;
                // priceText.text = dicePropertyData.purchasePrice.ToString();

                // descirption = dicePropertyData.description;
                // propertyExcuteName = dicePropertyData.excuteName;
            }
        }

        public void Init(DicePropertyData data)
        {
            image.sprite = Addressables.LoadAssetAsync<Sprite>(data.spritePath).WaitForCompletion();

            Color color = image.color;
            color.a = 1;
            image.color = color;

            priceText.text = data.purchasePrice.ToString();
            descirption = data.description;
        }
        public void OnPointerEnter(PointerEventData eventData)
        {
            shopSystem.InformationBoxDescription.Value = descirption;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            shopSystem.InformationBoxDescription.Value = "";
        }
    }
}
