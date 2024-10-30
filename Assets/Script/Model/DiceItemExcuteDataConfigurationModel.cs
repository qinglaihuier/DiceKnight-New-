using System.Collections.Generic;
using QFramework;
using ScriptableObjectData.PropertyExcuteData;
using UnityEngine;
using UnityEngine.AddressableAssets;
namespace Model
{
    public enum DiceItemType
    {
        BlowUp,
        Spike,
        Stun
    }
    public interface IDiceItemExcuteDataConfigurationModel : IModel
    {
        public T GetDiceItemExcuteData<T>(DiceItemType propertyType) where T :class, IDiceItemExcuteData; 
    }
    /// <summary>
    /// 骰子道具实际执行效果相关配置数据
    /// </summary>
    public class DiceItemExcuteDataConfigurationModel : AbstractModel, IDiceItemExcuteDataConfigurationModel
    {
        private Dictionary<DiceItemType, IDiceItemExcuteData> dic;

        public T GetDiceItemExcuteData<T>(DiceItemType propertyType) where T : class, IDiceItemExcuteData
        {
            T data = dic[propertyType] as T;
            return data;
        }

        protected override void OnInit()
        {
            dic = new Dictionary<DiceItemType, IDiceItemExcuteData>();

            var handle = Addressables.LoadAssetsAsync<IDiceItemExcuteData>("PropertyExcuteData", (data) =>
            {
                dic.Add(data.PropertyType, data);
            });
            handle.WaitForCompletion();
        }
    }
}