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
        protected TType _prefab;

        public virtual TType GetNewInstance()
        {
            GameObject gameObject = KhtPool.GetObject(_prefab.gameObject);
            gameObject.SetActive(true);
            TType inctance = gameObject.GetComponent<TType>();
            return inctance;
        }     
    }
}
