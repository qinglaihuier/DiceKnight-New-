using System;
using System.Collections.Generic;
using Model;
using UnityEngine;

namespace ScriptableObjectData.PropertyExcuteData
{
    public interface IPropertyExcuteData
    {
        public PropertyType PropertyType { get; }

    }
    [CreateAssetMenu(menuName = "Data/PropertyData/BlowUp")]
    public class BlowUpExcuteData : ScriptableObject, IPropertyExcuteData
    {
        [SerializeField] private List<BlowUpExcuteDataEachLevel> blowUpExcuteDataList;

        [SerializeField] private PropertyType propertyType;
        public PropertyType PropertyType
        {
            get
            {
                return propertyType;
            }
        }
        public BlowUpExcuteDataEachLevel GetBlowUpExcuteData(int level)
        {
            if (level <= blowUpExcuteDataList.Count && level >= 1)
            {
                return blowUpExcuteDataList[level - 1];
            }
            return default;
        }
    }

    [Serializable]
    public class BlowUpExcuteDataEachLevel
    {
        public float damageMultiple;
        public float damageRange;
    }
}

