using System;
using System.Collections;
using Model;
using QFramework;
using ScriptableObjectData;
using UnityEngine;
using UnityEngine.AddressableAssets;
namespace ViewController
{
    public class EnemyManager : AbstractViewController
    {
        #region  Model
        private IEnemyRefreshModel waveInformationModel;
        private IEnemyDataConfigurationModel enemyDataConfigurationModel;
        //在这里获取了IPlayerModel提供的所有数据 但实际上我们只会用到PlayerPosition 考虑用Query优化？
        //Command或者Query多次创建是否会带来性能问题  充血模型 贫血模型
        //事件系统能否改良 对无参事件的支持？ 对单例模式的支持？
        private IPlayerModel playerModel;
        #endregion
        private void Awake()
        {
            waveInformationModel = this.GetModel<IEnemyRefreshModel>();
            enemyDataConfigurationModel = this.GetModel<IEnemyDataConfigurationModel>();
            playerModel = this.GetModel<IPlayerModel>();
            waveInformationModel.WaveCount.Register(OnWaveCountUpdate).UnRegisterWhenGameObjectDestroyed(gameObject);
        }
        private void Start()
        {
            StartCoroutine(WaveUpdateCoroutine());
        }
        IEnumerator WaveUpdateCoroutine()
        {
            while (true)
            {
                waveInformationModel.WaveCount.Value += 1;
                EnemyWaveConfigurationInformation info = waveInformationModel.GetEnemyWaveInformation();
                Debug.Log("敌人波数更新" + info.waveCount.ToString());
                yield return new WaitForSeconds(info.duration);
            }
        }
        private void OnWaveCountUpdate(int waveCount)
        {
            StartCoroutine(UpdateEnemyCoroutine());
        }
        IEnumerator UpdateEnemyCoroutine()
        {
            EnemyWaveConfigurationInformation info = waveInformationModel.GetEnemyWaveInformation();
            string waveEnemy = info.waveBeginningEnemy;
            string slowlyRefreshWaveEnemy = info.slowlyRefreshWaveEnemy;
            float waveWeight = info.waveWeight;
            float interval = (float)info.duration / waveWeight;

            //生成在每波开始时就要生成的敌人
            string[] waveEnemyKindAndAmount = waveEnemy.Split('\n');
            for (int i = 0; i < waveEnemyKindAndAmount.Length; ++i)
            {
                string[] kindAndAmount = waveEnemyKindAndAmount[i].Split('*');
                Type type = Type.GetType(kindAndAmount[0]);
                int amount = int.Parse(kindAndAmount[1]);

                WaitForSeconds wait = new WaitForSeconds(interval / amount);

                for (int j = 0; j < amount; ++j)
                {
                    EnemyBase newEnemy = ObjectPoolManager.Instance.Get<EnemyBase>(kindAndAmount[0]);
                    newEnemy.transform.position = GetRandomEnemyStartPosition();
                    newEnemy.Init(enemyDataConfigurationModel.GetEnemyData(kindAndAmount[0]), waveInformationModel.WaveCount.Value);
                    yield return wait;
                }
            }

            //生成每波之后缓慢刷新的敌人
            string[] slowlyRefreshEnemyKind = slowlyRefreshWaveEnemy.Split(',');
            WaitForSeconds slowlyRefreshWait = new WaitForSeconds(interval);
            while (waveWeight > 0)
            {
                int i = UnityEngine.Random.Range(0, slowlyRefreshEnemyKind.Length);
                string enemyName = slowlyRefreshEnemyKind[i];

                EnemyBase newEnemy = ObjectPoolManager.Instance.Get<EnemyBase>(enemyName);
                EnemyData enemyData = enemyDataConfigurationModel.GetEnemyData(enemyName);

                newEnemy.transform.position = GetRandomEnemyStartPosition();
                newEnemy.Init(enemyData, waveInformationModel.WaveCount.Value);

                waveWeight -= enemyData.weight;

                yield return slowlyRefreshWait;
            }
        }
        private Vector2 GetRandomEnemyStartPosition()
        {
            //暂时这么处理
            float range = 30;

            Vector2 direction = UnityEngine.Random.insideUnitCircle.normalized;
            float radius = Vector2.Distance((Vector2)Camera.main.ViewportToWorldPoint(new Vector3(1, 1, 0)), playerModel.PlayerPosition);

            float a = UnityEngine.Random.Range(0, 1);

            Vector2 result = direction * (radius + range * a);
            return result;
        }
    }
}