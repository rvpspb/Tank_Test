using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimplePool;
using System;
using tank.config;

namespace tank.core
{
    public class Unit
    {
        //public T View { get; protected set; }

        public event Action OnDie;
        protected GameObject _gameObject;
        

        public void Die()
        {
            OnDie?.Invoke();
            KhtPool.ReturnObject(_gameObject);
        }
    }
}
