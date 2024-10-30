using System;
using System.Collections;
using System.Collections.Generic;
using QFramework;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using ViewController;
namespace ViewController
{
    public class ShopManager : AbstractViewController
    {
        [SerializeField] private TextMeshProUGUI moneyText;
        [SerializeField] private Button goBackButton;

        #region System
        private IShopSystem shopSystem;
        #endregion
        private void Awake()
        {
            shopSystem = this.GetSystem<IShopSystem>();
            goBackButton.onClick.AddListener(OnGoBackButtonClick);
        }
        private void OnEnable()
        {
            moneyText.text = shopSystem.Money.Value.ToString();
        }
        private void OnGoBackButtonClick()
        {
            gameObject.SetActive(false);
        }
    }
}
