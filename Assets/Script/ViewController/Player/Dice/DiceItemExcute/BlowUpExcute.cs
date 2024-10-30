using Model;
using ScriptableObjectData.PropertyExcuteData;
using UnityEngine;
namespace ViewController
{
    public class BlowUpExcute : IDiceItemExcute
    {
        public DiceItemType DiceItemType { get { return DiceItemType.BlowUp; } }
        public BlowUpExcute()
        {

        }
        public BlowUpExcute(BlowUpExcuteData data, int level)
        {
            
        }
        public void OnExcute()
        {
            Debug.Log("爆炸");
        }
    }
}