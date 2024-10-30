using Model;
using ScriptableObjectData.PropertyExcuteData;
using UnityEngine;
namespace ViewController
{
    public class BlowUpExcute : IDicePropertyExcute
    {
        public PropertyType PropertyType { get { return PropertyType.BlowUp; } }
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