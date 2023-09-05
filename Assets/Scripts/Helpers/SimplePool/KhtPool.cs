using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SimplePool
{
    public class KhtPool : KhtSingleton<KhtPool>
    {
        // Хак из-за отсутствия поддержки отображения Dictionary в инспекторе
        // Словарь с необходимыми объектами будет создаваться из листа с описанием префабов
        [Serializable]
        public class PrefabData
        {
            public GameObject prefab;
            public int initPoolSize = 0;
        }
        [SerializeField] private List<PrefabData> prefabDatas = null;

        // Привязка пула к идентификатору префаба
        private readonly Dictionary<int, Queue<GameObject>> _pools = new Dictionary<int, Queue<GameObject>>();
        // Привязка объекта к пулу по его идентификатору
        private readonly Dictionary<int, int> _objectToPoolDict = new Dictionary<int, int>();

        private Dictionary<int, Transform> _poolParents = new Dictionary<int, Transform>();


        private new void Awake()
        {
            // Настройка синглетона
            base.Awake();

            // При необходимости предварительно наполняем пулы объектов
            foreach (var prefabData in prefabDatas)
            {
                int prefabId = prefabData.prefab.GetInstanceID();

                _pools.Add(prefabId, new Queue<GameObject>());

                _poolParents.Add(prefabId, new GameObject($"Pool Parent of {prefabData.prefab.name}").transform);

                for (int i = 0; i < prefabData.initPoolSize; i++)
                {
                    //GameObject retObject = Instantiate(prefabData.prefab, Instance.transform, true);

                    GameObject retObject = Instantiate(prefabData.prefab, _poolParents[prefabId], true);

                    Instance._objectToPoolDict.Add(retObject.GetInstanceID(), prefabId);
                    Instance._pools[prefabId].Enqueue(retObject);
                    retObject.SetActive(false);
                }
            }
            prefabDatas = null;
        }

        // Получение объекта из пула
        public static GameObject GetObject(GameObject prefab)
        {
            // В случае отсутствия синглетона просто создаём новый объект
            if (!Instance)
            {
                return Instantiate(prefab);
            }

            // Уникальный идентификатор префаба, по которому осуществляется привязка к пулу
            int prefabId = prefab.GetInstanceID();
            // Если пула для префаба не существует, то создаём новый
            if (!Instance._pools.ContainsKey(prefabId))
            {
                Instance._pools.Add(prefabId, new Queue<GameObject>());

                Instance._poolParents.Add(prefabId, new GameObject($"Pool Parent of {prefab.name}").transform);
            }

            // При наличии объекта в пуле возвращаем его
            if (Instance._pools[prefabId].Count > 0)
            {
                return Instance._pools[prefabId].Dequeue();
            }

            // В случае нехватки объектов создаём новый
            GameObject retObject = Instantiate(prefab);
            // Добавляем привязку объекта к пулу по его идентификатору
            Instance._objectToPoolDict.Add(retObject.GetInstanceID(), prefabId);

            //Debug.Log("Pool need more " + prefab.name);

            return retObject;
        }

        public static GameObject GetObject(GameObject prefab, Vector3 position, Quaternion rotation, Transform parent)
        {
            // В случае отсутствия синглетона просто создаём новый объект
            if (!Instance)
            {
                return Instantiate(prefab, position, rotation, parent);
            }

            // Уникальный идентификатор префаба, по которому осуществляется привязка к пулу
            int prefabId = prefab.GetInstanceID();
            // Если пула для префаба не существует, то создаём новый
            if (!Instance._pools.ContainsKey(prefabId))
            {
                Instance._pools.Add(prefabId, new Queue<GameObject>());

                Instance._poolParents.Add(prefabId, new GameObject($"Pool Parent of {prefab.name}").transform);
            }

            // При наличии объекта в пуле возвращаем его
            if (Instance._pools[prefabId].Count > 0)
            {
                GameObject temp = Instance._pools[prefabId].Dequeue();

                //if (temp.transform.parent != parent)
                    temp.transform.parent = parent;

                temp.transform.SetPositionAndRotation(position, rotation);

                //Debug.Log("return from pool");

                return temp;
            }

            // В случае нехватки объектов создаём новый
            GameObject retObject = Instantiate(prefab, position, rotation, parent);

            //Debug.Log("Pool need more " + prefab.name);

            // Добавляем привязку объекта к пулу по его идентификатору
            Instance._objectToPoolDict.Add(retObject.GetInstanceID(), prefabId);

            return retObject;
        }

        // Возврат объекта в пул
        public static void ReturnObject(GameObject poolObject, bool parentToPool = true)
        {
            // В случае отсутствия синглетона просто уничтожаем объект
            if (!Instance)
            {
                Destroy(poolObject);
                return;
            }

            // Идентификатор объекта для определения пула
            int objectId = poolObject.GetInstanceID();

            // В случае отсутствия привязки объекта к пулу просто его уничтожаем
            if (!Instance._objectToPoolDict.TryGetValue(objectId, out int poolId))
            {
                Destroy(poolObject);
                return;
            }

            // Возвращаем объект в пул
            Instance._pools[poolId].Enqueue(poolObject);
            //if (parentToPool)
                poolObject.transform.SetParent(Instance._poolParents[poolId]);
            poolObject.SetActive(false);
        }
    }
}
