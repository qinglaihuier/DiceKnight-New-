using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ScriptableObjectData
{
    [CreateAssetMenu(menuName = "Data/ShopData")]
    public class ShopData : ScriptableObject
    {
        public int initialMoney;
        public int refreshCost;
        public string moneyIsShort;
        public string purchasedTwo;
        public string maximumQuantity;
    }
}

