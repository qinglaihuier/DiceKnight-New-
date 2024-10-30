using Model;
using QFramework;
using ScriptableObjectData.PropertyExcuteData;
using UnityEngine;
using ViewController;
namespace System
{
    public interface IPropertyExcuteSystem : ISystem
    {
        public IDicePropertyExcute GetPropertyExcuteObject(string name, int level);
    }
    public class PropertyExcuteSystem : AbstractSystem, IPropertyExcuteSystem
    {
        #region  Model
        private IPropertyExcuteDataConfigurationModel propertyExcuteDataConfigurationModel;
        #endregion

        protected override void OnInit()
        {
            propertyExcuteDataConfigurationModel = this.GetModel<IPropertyExcuteDataConfigurationModel>();
        }
        public IDicePropertyExcute GetPropertyExcuteObject(string name, int level)
        {
            switch (name)
            {
                case "BlowUpExcute":
                    BlowUpExcuteData data = propertyExcuteDataConfigurationModel.GetPropertyExcuteData<BlowUpExcuteData>(PropertyType.BlowUp);
                    return new BlowUpExcute(data, level);
                default:
                    return null;
            }
        }


    }
}