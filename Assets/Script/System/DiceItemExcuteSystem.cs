using Model;
using QFramework;
using ScriptableObjectData.PropertyExcuteData;
using UnityEngine;
using ViewController;
namespace System
{
    public interface IDiceItemExcuteSystem : ISystem
    {
        public IDiceItemExcute GetDiceItemExcuteObject(string name, int level);
    }
    
    public class DiceItemExcuteSystem : AbstractSystem, IDiceItemExcuteSystem
    {
        #region  Model
        private IDiceItemExcuteDataConfigurationModel diceItemExcuteDataConfigurationModel;
        #endregion

        protected override void OnInit()
        {
            diceItemExcuteDataConfigurationModel = this.GetModel<IDiceItemExcuteDataConfigurationModel>();
        }
        public IDiceItemExcute GetDiceItemExcuteObject(string name, int level)
        {
            switch (name)
            {
                case "BlowUpExcute":
                    BlowUpExcuteData data = diceItemExcuteDataConfigurationModel.GetDiceItemExcuteData<BlowUpExcuteData>(DiceItemType.BlowUp);
                    return new BlowUpExcute(data, level);
                default:
                    return null;
            }
        }


    }
}