using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SimplePool
{
    public class ObjectPool : MonoBehaviour
    {
        // Хак из-за отсутствия поддержки отображения Dictionary в инспекторе
        // Словарь с необходимыми объектами будет создаваться из листа с описанием префабов
        [Serializable]
        public class PrefabData
        {
            public string name;
            public GameObject prefab;
        }
        [SerializeField] private List<PrefabData> _prefabDatas = null;

        // Префабы для создания новых объектов
        private Dictionary<string, GameObject> _prefabs = new Dictionary<string, GameObject>();
        // Пулы для свободных объектов
        private Dictionary<string, Queue<GameObject>> _pools = new Dictionary<string, Queue<GameObject>>();

        private void Awake()
        {
            // Перекладываем информацию о пулах из нашей структуры в Dictionary
            foreach (var prefabData in _prefabDatas)
            {
                _prefabs.Add(prefabData.name, prefabData.prefab);
                _pools.Add(prefabData.name, new Queue<GameObject>());
            }
            _prefabDatas = null;
        }

        public GameObject GetObject(string poolName)
        {
            // При наличии свободного объекта в пуле возвращаем его
            if (_pools[poolName].Count > 0)
            {
                return _pools[poolName].Dequeue();
            }

            // Если пул пуст, то создаём новый объект
            return Instantiate(_prefabs[poolName]);
        }

        public void ReturnObject(string poolName, GameObject poolObject)
        {
            // Возвращаем объект в пул
            _pools[poolName].Enqueue(poolObject);
        }
    }
}
