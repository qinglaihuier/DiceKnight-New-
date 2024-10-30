using System.Collections;
using System.Collections.Generic;
using Model;
using UnityEngine;

namespace ViewController
{
    public class StunExcute : IDiceItemExcute
    {
        public DiceItemType DiceItemType => DiceItemType.Stun;

        public void OnExcute()
        {
            Debug.Log("晕眩");
        }
    }
}

