using System.Collections;
using System.Collections.Generic;
using Model;
using UnityEngine;

namespace ViewController
{
    public class SpikeExcute : IDiceItemExcute
    {
        public DiceItemType DiceItemType => DiceItemType.Spike;

        public void OnExcute()
        {
            Debug.Log("尖刺");
        }
    }

}
