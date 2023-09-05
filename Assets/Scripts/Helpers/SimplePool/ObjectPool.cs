using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SimplePool
{
    public class ObjectPool : MonoBehaviour
    {
        // ��� ��-�� ���������� ��������� ����������� Dictionary � ����������
        // ������� � ������������ ��������� ����� ����������� �� ����� � ��������� ��������
        [Serializable]
        public class PrefabData
        {
            public string name;
            public GameObject prefab;
        }
        [SerializeField] private List<PrefabData> _prefabDatas = null;

        // ������� ��� �������� ����� ��������
        private Dictionary<string, GameObject> _prefabs = new Dictionary<string, GameObject>();
        // ���� ��� ��������� ��������
        private Dictionary<string, Queue<GameObject>> _pools = new Dictionary<string, Queue<GameObject>>();

        private void Awake()
        {
            // ������������� ���������� � ����� �� ����� ��������� � Dictionary
            foreach (var prefabData in _prefabDatas)
            {
                _prefabs.Add(prefabData.name, prefabData.prefab);
                _pools.Add(prefabData.name, new Queue<GameObject>());
            }
            _prefabDatas = null;
        }

        public GameObject GetObject(string poolName)
        {
            // ��� ������� ���������� ������� � ���� ���������� ���
            if (_pools[poolName].Count > 0)
            {
                return _pools[poolName].Dequeue();
            }

            // ���� ��� ����, �� ������ ����� ������
            return Instantiate(_prefabs[poolName]);
        }

        public void ReturnObject(string poolName, GameObject poolObject)
        {
            // ���������� ������ � ���
            _pools[poolName].Enqueue(poolObject);
        }
    }
}
