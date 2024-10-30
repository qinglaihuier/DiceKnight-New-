using System.Collections;
using System.Collections.Generic;
using Model;
using UnityEngine;

namespace ViewController
{
    public class StunExcute : IDicePropertyExcute
    {
        public PropertyType PropertyType => PropertyType.Stun;

        public void OnExcute()
        {
            Debug.Log("晕眩");
        }
    }
}

