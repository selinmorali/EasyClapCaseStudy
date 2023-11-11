using System.Collections.Generic;
using UnityEngine;

namespace _GAME.Scripts.Pool
{
    public class ObjectPooler : MonoBehaviour
    {
        [System.Serializable]
        public class Pool
        {
            public string tag;
            public GameObject prefab;
            public int size;
        }
        
        public static ObjectPooler objectPoolerInstance;
        public List<Pool> pools;
        public Dictionary<string, List<GameObject>> poolDictionary;
        
        
        private void Awake()
        {
            objectPoolerInstance = this;
        }

        private void Start()
        {
            poolDictionary = new Dictionary<string, List<GameObject>>();
            foreach (Pool pool in pools)
            {
                List<GameObject> objectPool = new();
                for (int i = 0; i < pool.size; i++)
                {
                    GameObject obj = Instantiate(pool.prefab);
                    obj.SetActive(false);
                    objectPool.Add(obj);
                }

                poolDictionary.Add(pool.tag, objectPool);
            }
        }

        public GameObject SpawnFromPool(string tag, Vector3 position, Quaternion rotation)
        {
            if (!poolDictionary.ContainsKey(tag))
            {
                return null;
            }

            foreach (var obj in poolDictionary[tag])
            {
                if (obj != null && !obj.activeInHierarchy)
                {
                    obj.transform.SetPositionAndRotation(position, rotation);
                    obj.SetActive(true);
                    return obj;
                }
            }

            for (int i = 0; i < pools.Count; i++)
            {
                if (pools[i].tag == tag)
                {
                    GameObject obj = Instantiate(pools[i].prefab);
                    obj.transform.SetPositionAndRotation(position, rotation);
                    poolDictionary[tag].Add(obj);
                    return obj;
                }
            }

            return null;
        }
    }
}