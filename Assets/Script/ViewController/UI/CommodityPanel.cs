using System;
using QFramework;
using UnityEngine;

namespace ViewController
{
    public class CommodityPanel : AbstractViewController
    {
        private void Start()
        {
           for(int i = 0; i < transform.childCount; ++i)
           {
                transform.GetChild(i).GetComponent<Commodity>().index = i;
           }
        }
       
    }
}