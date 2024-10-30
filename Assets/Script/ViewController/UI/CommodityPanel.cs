using System;
using System.Collections.Generic;
using Model;
using QFramework;
using UnityEngine;

namespace ViewController
{
    public class CommodityPanel : AbstractViewController
    {
        #region System
        private IShopSystem shopSystem;
        #endregion
        private List<Commodity> commodities = new List<Commodity>(3);
        private void Start()
        {
            shopSystem = this.GetSystem<IShopSystem>();
            for (int i = 0; i < transform.childCount; ++i)
            {
                Commodity c = transform.GetChild(i).GetComponent<Commodity>();
                c.index = i;
                commodities.Add(c);
            }

            this.RegisterEvent<RefreshSellingCommodity>(OnRefreshSellingCommodityEvent).UnRegisterWhenGameObjectDestroyed(gameObject);
            shopSystem.InitCommodity();
        }
        private void OnRefreshSellingCommodityEvent(RefreshSellingCommodity r)
        {
            commodities[r.index].Init(r.newData);
        }
    }
}