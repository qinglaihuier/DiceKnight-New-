using System;
using System.Collections;
using System.Collections.Generic;
using Command;
using Model;
using QFramework;
using ScriptableObjectData;
using UnityEngine;
using ViewController;

namespace ViewController
{


    public abstract class EnemyBase : AbstractViewController, IDiceInteraction, IBelongToObjectPool
    {
        #region  依赖组件
        new protected Rigidbody2D rigidbody2D;
        #endregion

        #region 状态
        [SerializeField] protected bool moveTowardPlayer = true;
        private bool init = false;
        #endregion

        #region 临时 缓存 逻辑数据
        private float hp;
        private float atk; //触碰攻击伤害
        private float satk; //特殊攻击伤害  冲刺 爆炸 魔法
        private float pdef;
        private float mdef;
        private float drop;
        private float speedSize;
        private int fre;
        #endregion

        #region  Model
        protected IPlayerModel playerModel;
        #endregion
        public Action<GameObject> PullBack{ get; set; }
        public virtual void Init(EnemyData enemyData, int waveCount)
        {
            //是否已经初始化过
            if (init == false)
            {
                init = true;
                rigidbody2D = GetComponent<Rigidbody2D>();
                playerModel = this.GetModel<IPlayerModel>();
            }

            //计算数值 LOG(W + a, b) * (c + d * W) * e
            hp = Mathf.Log(waveCount + enemyData.hp_a, enemyData.hp_b) * (enemyData.hp_c + enemyData.hp_d * waveCount) * enemyData.hp_e;
            atk = Mathf.Log(waveCount + enemyData.atk_a, enemyData.atk_b) * (enemyData.atk_c + enemyData.atk_d * waveCount)
             * enemyData.atk_e;
            satk = Mathf.Log(waveCount + enemyData.satk_a, enemyData.satk_b) * (enemyData.satk_c + enemyData.satk_d * waveCount)
             * enemyData.satk_e;
            pdef = Mathf.Log(waveCount + enemyData.pdef_a, enemyData.pdef_b) * (enemyData.pdef_c + enemyData.pdef_d * waveCount)
             * enemyData.pdef_e;
            mdef = Mathf.Log(waveCount + enemyData.mdef_a, enemyData.mdef_b) * (enemyData.mdef_c + enemyData.mdef_d * waveCount)
             * enemyData.mdef_e;
            drop = Mathf.Log(waveCount + enemyData.drop_a, enemyData.drop_b) * (enemyData.drop_c + enemyData.drop_d * waveCount)
             * enemyData.drop_e;
            speedSize = enemyData.speedSize;
            fre = enemyData.fre;
        }
        protected virtual void FixedUpdate()
        {
            if (moveTowardPlayer)
            {
                Vector2 moveDirection = ((Vector2)playerModel.PlayerPosition - (Vector2)transform.position).normalized;
                rigidbody2D.velocity = moveDirection * speedSize;
            }
        }
        public virtual void TriggerDiceInteraction(int value)
        {
            hp -= value;
            if(hp <= 0)
            {
                PullBack(gameObject); //返回对象池
            }
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            //伤害玩家
            if (other.CompareTag("Player") && playerModel.injured == false)
            {
                this.SendCommand<AttackPlayerCommand>(new AttackPlayerCommand(1));
            }
        }

    }
}
