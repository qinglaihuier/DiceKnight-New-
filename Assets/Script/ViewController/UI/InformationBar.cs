using System;
using System.Collections;
using System.Collections.Generic;
using QFramework;
using TMPro;
using UnityEngine;
using ViewController;

namespace ViewController
{
    public class InformationBar : AbstractViewController
    {
        #region  System
        private IShopSystem shopSystem;
        #endregion

        [SerializeField] private TextMeshProUGUI descriptionUI;
        
        private void Start()
        {
            shopSystem = this.GetSystem<IShopSystem>();

            shopSystem.InformationBoxDescription.Register(UpdateDescription).UnRegisterWhenGameObjectDestroyed(gameObject);
        }
        private void UpdateDescription(string s)
        {
            descriptionUI.text = s;
        }
    }
}
