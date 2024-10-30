using System.Collections;
using System.Collections.Generic;
using Model;
using UnityEngine;

namespace ViewController
{
    public class SpikeExcute : IDicePropertyExcute
    {
        public PropertyType PropertyType => PropertyType.Spike;

        public void OnExcute()
        {
            Debug.Log("尖刺");
        }
    }

}
