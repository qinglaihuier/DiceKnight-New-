using System;
using System.Collections;
using System.Collections.Generic;
using QFramework;
using UnityEngine;

namespace ViewController
{
    public class PurchaseCommodityPanel : AbstractViewController
    {

        private void Start()
        {
            this.RegisterEvent<BuyCommodityEvent>(OnBuyCommodityEvent).UnRegisterWhenGameObjectDestroyed(gameObject);
        }
        private void OnBuyCommodityEvent(BuyCommodityEvent eventData)
        {
            for(int i = 0; i < transform.childCount; ++i)
            {
                if(transform.GetChild(i).GetComponent<PurchasedCommodity>() == null)
                {
                    var g = transform.GetChild(i).gameObject;
                    g.AddComponent<PurchasedCommodity>().Init(eventData.dicePropertyData);
                    return;
                }
            }
        }
    }
}
