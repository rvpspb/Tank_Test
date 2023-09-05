using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using tank.config;
using tank.core;
using SimplePool;

namespace tank.factory
{
    public class MonoFactory<TType> where TType : MonoBehaviour
    {

        private TType _prefab;
        private List<TType> _spawned;


        public MonoFactory()
        {
            _spawned = new List<TType>();
        }

        public TType Spawn()
        {
            GameObject gameObject = KhtPool.GetObject(_prefab.gameObject);
            return gameObject.GetComponent<TType>();
        }

        public void ClearSpawned()
        {
            for (int i = 0; i < _spawned.Count; i++)
            {
                KhtPool.ReturnObject(_spawned[i].gameObject);
            }

            _spawned.Clear();
        }
    }
}
