using System.Collections.Generic;
using QFramework;
using ScriptableObjectData.PropertyExcuteData;
using UnityEngine;
using UnityEngine.AddressableAssets;
namespace Model
{
    public enum PropertyType
    {
        BlowUp,
        Spike,
        Stun
    }
    public interface IPropertyExcuteDataConfigurationModel : IModel
    {
        public T GetPropertyExcuteData<T>(PropertyType propertyType) where T :class, IPropertyExcuteData; 
    }
    public class PropertyExcuteDataConfigurationModel : AbstractModel, IPropertyExcuteDataConfigurationModel
    {
        private Dictionary<PropertyType, IPropertyExcuteData> dic;

        public T GetPropertyExcuteData<T>(PropertyType propertyType) where T : class, IPropertyExcuteData
        {
            T data = dic[propertyType] as T;
            return data;
        }

        protected override void OnInit()
        {
            dic = new Dictionary<PropertyType, IPropertyExcuteData>();

            var handle = Addressables.LoadAssetsAsync<IPropertyExcuteData>("PropertyExcuteData", (data) =>
            {
                dic.Add(data.PropertyType, data);
            });
            handle.WaitForCompletion();
        }
    }
}