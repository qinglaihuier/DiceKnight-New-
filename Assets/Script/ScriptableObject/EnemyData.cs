using UnityEngine;

namespace ScriptableObjectData
{
    [CreateAssetMenu(menuName = "Data/EnemyData")]
    public class EnemyData : ScriptableObject
    {
        [Header("W : 当前波次数")]
        [Header("HP计算参数 LOG(W + a, b) * (c + d * W) * e ")]
        public float hp_a;
        public float hp_b;
        public float hp_c;
        public float hp_d;
        public float hp_e;

        [Header("ATK计算参数 LOG(W + a, b) * (c + d * W) * e")]
        public float atk_a;
        public float atk_b;
        public float atk_c;
        public float atk_d;
        public float atk_e;

        [Header("SATK计算参数 LOG(W + a, b) * (c + d * W) * e")]
        public float satk_a;
        public float satk_b;
        public float satk_c;
        public float satk_d;
        public float satk_e;

        [Header("PDEF计算参数 LOG(W + a, b) * (c + d * W) * e")]
        public float pdef_a;
        public float pdef_b;
        public float pdef_c;
        public float pdef_d;
        public float pdef_e;

        [Header("MDEF计算参数 LOG(W + a, b) * (c + d * W) * e")]
        public float mdef_a;
        public float mdef_b;
        public float mdef_c;
        public float mdef_d;
        public float mdef_e;

        [Header("掉落计算参数 LOG(W + a, b) * (c + d * W) * e")]
        public float drop_a;
        public float drop_b;
        public float drop_c;
        public float drop_d;
        public float drop_e;

        [Space]

        public float speedSize;

        public float weight;

        public int fre;
    }

}
