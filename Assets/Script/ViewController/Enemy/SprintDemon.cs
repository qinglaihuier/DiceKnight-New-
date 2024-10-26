using System.Collections;
using ScriptableObjectData;
using UnityEngine;
namespace ViewController
{
    public class SprintDemon : EnemyBase
    {
        #region 冲刺配置数据
        [SerializeField] private float sprintDistance = 15; //触发冲刺的距离
        [SerializeField] private float storageTime = 2; //冲刺前蓄力时间
        [SerializeField] private float sprintSpeed = 10;
        [SerializeField] private float sprintTime = 5;
        [SerializeField] private float sprintCD = 20;
        #endregion

        #region 状态
        private bool canSprint = true;
        #endregion

        #region Test
        public EnemyData enemyData;
        #endregion
        private void Awake()
        {
            Init(enemyData, 1);
        }
        public override void Init(EnemyData enemyData, int waveCount)
        {
            base.Init(enemyData, waveCount);
            canSprint = true;
        }
        protected override void FixedUpdate()
        {
            base.FixedUpdate();
            if (canSprint && Vector2.Distance(playerModel.PlayerPosition, transform.position) <= sprintDistance)
            {
                moveTowardPlayer = false;
                StartCoroutine(SprintCoroutine());
            }
        }
        private IEnumerator SprintCoroutine()
        {
            canSprint = false;
            rigidbody2D.velocity = Vector2.zero;
            
            yield return new WaitForSeconds(storageTime);
            Vector2 moveDirection = ((Vector2)playerModel.PlayerPosition - (Vector2)transform.position).normalized;
            rigidbody2D.velocity = moveDirection * sprintSpeed;

            yield return new WaitForSeconds(sprintTime);
            rigidbody2D.velocity = Vector2.zero;
            moveTowardPlayer = true;

            yield return new WaitForSeconds(sprintCD);
            canSprint = true;
        }
    }
}