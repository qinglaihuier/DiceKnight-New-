using QFramework;
using UnityEngine.AddressableAssets;
using UnityEngine;
using System.Collections.Generic;
using System;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Logical;
namespace ViewController
{
    public interface IObjectPoolManager
    {
        public T Get<T>(string name) where T : MonoBehaviour, IBelongToObjectPool;
        public GameObject Get(string name);
    }
    public interface IBelongToObjectPool
    {
        public Action<GameObject> PullBack { get; set; }
    }
    public class ObjectPoolManager : AbstractViewController, IObjectPoolManager
    {
        private static IObjectPoolManager instance;
        public static IObjectPoolManager Instance
        {
            get
            {
                //注意可能在未初始化时访问问题
                return instance;
            }
        }
        private Dictionary<string, Queue<GameObject>> poolDic = new Dictionary<string, Queue<GameObject>>();
        private void Awake()
        {
            if (instance != null)
            {
#if UNITY_EDITOR
                Debug.LogWarning("多个对象池单例");
#endif
                Destroy(gameObject);
                return;
            }
            instance = this;
        }
        public T Get<T>(string name) where T : MonoBehaviour, IBelongToObjectPool
        {
            return Get(name).GetComponent<T>();
        }

        private void PullBack(GameObject gameObject)
        {
            string name = gameObject.name;
            poolDic[name].Enqueue(gameObject);
            gameObject.SetActive(false);
        }

        public GameObject Get(string name)
        {
            if (poolDic.ContainsKey(name) == false)
            {
                poolDic.Add(name, new Queue<GameObject>());
            }

            var queue = poolDic[name];

            if (queue.Count == 0)
            {
                Transform parent = transform.Find(name + "Pool");
                if(parent == null)
                {
                    GameObject pool = new GameObject(name + "Pool");
                    pool.transform.parent = transform;
                    parent = pool.transform;
                }
                
                var handle = Addressables.LoadAssetAsync<GameObject>(name);
                handle.WaitForCompletion();
#if UNITY_EDITOR
                if (handle.Result == null)
                {
                    Debug.LogError("加载了不存在的对象： " + name);
                }
#endif
                GameObject obj = Instantiate<GameObject>(handle.Result);
                obj.name = name;
                obj.transform.parent = parent;
                obj.GetComponent<IBelongToObjectPool>().PullBack = PullBack;

                queue.Enqueue(obj);
            }

            GameObject result = queue.Dequeue();
            result.SetActive(true);
            return result;
        }
    }
}