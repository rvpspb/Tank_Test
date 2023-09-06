using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using tank.factory;
using SimplePool;

namespace tank.core
{
    public class MonoSpawner<TType> : MonoBehaviour where TType : MonoBehaviour, IDestroy<TType>
    {
        protected MonoFactory<TType> _monoFactory;
        protected List<TType> _spawned;

        public virtual void Construct()
        {            
            _spawned = new List<TType>();
        }

        //public TType Spawn()
        //{
        //    TType instance = _monoFactory.GetNewInstance();
        //    AddToSpawned(instance);
        //    return instance;
        //}

        protected void AddToSpawned(TType instance)
        {
            _spawned.Add(instance);
            instance.OnDestroyed += RemoveFromSpawned;
        }

        protected void RemoveFromSpawned(TType instance)
        {
            instance.OnDestroyed -= RemoveFromSpawned;
            _spawned.Remove(instance);
        }

        public void ClearSpawned()
        {
            for (int i = 0; i < _spawned.Count; i++)
            {
                KhtPool.ReturnObject(_spawned[i].gameObject);
                RemoveFromSpawned(_spawned[i]);
            }

            _spawned.Clear();
        }

        //private void OnDestroy()
        //{
        //    ClearSpawned();
        //}
    }
}
