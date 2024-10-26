using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace ViewController
{
    public interface IDiceInteraction
    {
        public void TriggerDiceInteraction(int value);
    }
    public interface IDiceItem
    {
        public void OnExcute();
    }
    public class Dice : AbstractViewController
    {
        //
        #region 依赖组件
        new private Rigidbody2D rigidbody2D;
        new private Collider2D collider2D;
        #endregion

        #region 数据缓存
        private int pAtk;

        private Vector2 targetPos;

        private int playerLayer;
        private int diceLayer;
        private float speedSize;
        #endregion

        #region 状态
        private bool move;
        #endregion

        private IDiceItem[] diceItems;
        private void Awake()
        {
            rigidbody2D = GetComponent<Rigidbody2D>();
            collider2D = GetComponent<Collider2D>();

            playerLayer = LayerMask.NameToLayer("Player");
            diceLayer = LayerMask.NameToLayer("Dice");

            collider2D.enabled = false;

            diceItems = new IDiceItem[6];
        }

        public void Throw(int pAtk, Vector2 targetPos, float speedSize)
        {
            collider2D.enabled = true;

            this.pAtk = pAtk;
            this.targetPos = targetPos;
            this.speedSize = speedSize;

            Physics2D.IgnoreLayerCollision(playerLayer, diceLayer, true);

            move = true;
            transform.parent = null;
        }

        private void FixedUpdate()
        {
            if (move)
            {
                if (Vector2.Distance(transform.position, targetPos) > Mathf.Epsilon)
                {
                    Vector2 nextPos = Vector2.MoveTowards(transform.position, targetPos, speedSize);
                    rigidbody2D.MovePosition(nextPos);
                }
                else
                {
                    //TODO : 移动停止
                    move = false;
                    Physics2D.IgnoreLayerCollision(playerLayer, diceLayer, false);
                }
            }
        }
        public void BePickedUp(Transform parentTf, Vector3 localPosition)
        {
            transform.parent = parentTf;
            transform.localPosition = localPosition;
            collider2D.enabled = false;
        }
        private void OnTriggerEnter2D(Collider2D other)
        {
            if(move && other.TryGetComponent<IDiceInteraction>(out var i))
            {
                i.TriggerDiceInteraction(pAtk);
            }
        }
    }
}

