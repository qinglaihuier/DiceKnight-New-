using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ScriptableObjectData
{
    [CreateAssetMenu(menuName = "Data/PlayerData")]
    public class PlayerData : ScriptableObject
    {
        public int health;
        
        public int playerSpeedSize;

        public int pAtk;

        public int mAtk;

        public float diceSpeedSize;
        public float invincibleTime;
    }

}

