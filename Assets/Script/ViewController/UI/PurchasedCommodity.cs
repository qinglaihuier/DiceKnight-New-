using System;
using Model;
using QFramework;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.AddressableAssets;

namespace ViewController
{
    public class PurchasedCommodity : AbstractViewController, IPointerEnterHandler, IPointerClickHandler, IPointerExitHandler
    {
        #region 数据缓存 
        private string descirption;
        private int sellingPrice;
        #endregion

        #region System
        private IShopSystem shopSystem;
        #endregion

        #region 依赖组件 
        private Image image;
        private TextMeshProUGUI sellingPriceText;
        #endregion

        private void Awake()
        {
            image = GetComponent<Image>();
            sellingPriceText = GetComponentInChildren<TextMeshProUGUI>();
        }
        private void Start()
        {
            shopSystem = this.GetSystem<IShopSystem>();
        }
        public void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Right)
            {
                Debug.Log("出售");
            }
        }
        public void OnPointerEnter(PointerEventData eventData)
        {
            shopSystem.InformationBoxDescription.Value = descirption;
        }
        public void Init(DiceItemData data)
        {
            image.sprite = Addressables.LoadAssetAsync<Sprite>(data.spriteName).WaitForCompletion();
            Color c = image.color;
            c.a = 1;
            image.color = c;

            descirption = data.description;
            sellingPrice = data.sellingPrice;
            sellingPriceText.text = sellingPrice.ToString();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            shopSystem.InformationBoxDescription.Value = "";
        }
    }
}