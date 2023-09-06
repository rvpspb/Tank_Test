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
        
        public event Action OnDie;
        protected GameObject _gameObject;
        public bool IsAlive { get; private set; }

        public void SetAlive(bool value)
        {
            IsAlive = value;
        }

        protected virtual void Die()
        {
            SetAlive(false);
            OnDie?.Invoke();
            KhtPool.ReturnObject(_gameObject);
        }
    }
}
