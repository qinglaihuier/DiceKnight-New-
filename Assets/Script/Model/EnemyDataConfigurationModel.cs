using System.Collections.Generic;
using QFramework;
using UnityEngine.AddressableAssets;
using ScriptableObjectData;
namespace Model
{
    public interface IEnemyDataConfigurationModel : IModel
    {
        public EnemyData GetEnemyData(string key);
    }
    public class EnemyDataConfigurationModel : AbstractModel, IEnemyDataConfigurationModel
    {
        private Dictionary<string, EnemyData> enemyDataDic = new Dictionary<string, EnemyData>();
        protected override void OnInit()
        {
            var handle = Addressables.LoadAssetsAsync<EnemyData>("EnemyData", (data) => 
            {
                enemyDataDic.Add(data.name, data);
            });
            handle.WaitForCompletion();
        }
        public EnemyData GetEnemyData(string key)
        {
            if(enemyDataDic.TryGetValue(key, out EnemyData data))
            {
                return data;
            }
            return null;
        }
    }
}