using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SimplePool
{
    public class KhtPool : KhtSingleton<KhtPool>
    {        
        [Serializable]
        public class PrefabData
        {
            public GameObject prefab;
            public int initPoolSize = 0;
        }
        [SerializeField] private List<PrefabData> prefabDatas = null;

        private readonly Dictionary<int, Queue<GameObject>> _pools = new Dictionary<int, Queue<GameObject>>();
        private readonly Dictionary<int, int> _objectToPoolDict = new Dictionary<int, int>();
        private Dictionary<int, Transform> _poolParents = new Dictionary<int, Transform>();

        private new void Awake()
        {
            base.Awake();

            foreach (var prefabData in prefabDatas)
            {
                int prefabId = prefabData.prefab.GetInstanceID();
                _pools.Add(prefabId, new Queue<GameObject>());
                _poolParents.Add(prefabId, new GameObject($"Pool Parent of {prefabData.prefab.name}").transform);

                for (int i = 0; i < prefabData.initPoolSize; i++)
                {
                    GameObject retObject = Instantiate(prefabData.prefab, _poolParents[prefabId], true);
                    Instance._objectToPoolDict.Add(retObject.GetInstanceID(), prefabId);
                    Instance._pools[prefabId].Enqueue(retObject);
                    retObject.SetActive(false);
                }
            }

            prefabDatas = null;
        }

        public static GameObject GetObject(GameObject prefab, Transform parent = null)
        {
            if (!Instance)
            {
                return Instantiate(prefab, parent);
            }

            int prefabId = prefab.GetInstanceID();

            if (!Instance._pools.ContainsKey(prefabId))
            {
                Instance._pools.Add(prefabId, new Queue<GameObject>());
                Instance._poolParents.Add(prefabId, new GameObject($"Pool Parent of {prefab.name}").transform);
            }

            if (Instance._pools[prefabId].Count > 0)
            {
                GameObject gameObject = Instance._pools[prefabId].Dequeue();
                gameObject.transform.parent = parent;
                return gameObject;
            }

            GameObject retObject = Instantiate(prefab, parent);
            Instance._objectToPoolDict.Add(retObject.GetInstanceID(), prefabId);
            return retObject;
        }

        public static GameObject GetObject(GameObject prefab, Vector3 position, Quaternion rotation, Transform parent)
        {
            if (!Instance)
            {
                return Instantiate(prefab, position, rotation, parent);
            }

            int prefabId = prefab.GetInstanceID();

            if (!Instance._pools.ContainsKey(prefabId))
            {
                Instance._pools.Add(prefabId, new Queue<GameObject>());
                Instance._poolParents.Add(prefabId, new GameObject($"Pool Parent of {prefab.name}").transform);
            }

            if (Instance._pools[prefabId].Count > 0)
            {
                GameObject temp = Instance._pools[prefabId].Dequeue();
                temp.transform.parent = parent;
                temp.transform.SetPositionAndRotation(position, rotation);
                return temp;
            }

            GameObject retObject = Instantiate(prefab, position, rotation, parent);
            Instance._objectToPoolDict.Add(retObject.GetInstanceID(), prefabId);
            return retObject;
        }

        public static void ReturnObject(GameObject poolObject)
        {
            if (!Instance)
            {
                Destroy(poolObject);
                return;
            }

            int objectId = poolObject.GetInstanceID();

            if (!Instance._objectToPoolDict.TryGetValue(objectId, out int poolId))
            {
                Destroy(poolObject);
                return;
            }

            Instance._pools[poolId].Enqueue(poolObject);
            poolObject.transform.SetParent(Instance._poolParents[poolId]);
            poolObject.SetActive(false);
        }
    }
}
