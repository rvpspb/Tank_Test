using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimplePool;
using System;
using tank.config;
using tank.helpers;

namespace tank.core
{
    public class Unit
    {
        public float Health { get; protected set; }
        public float Armor { get; protected set; }

        public event Action OnDie;
        protected GameObject _gameObject;
        public UnitMover UnitMover { get; protected set; }
        public bool IsAlive { get; private set; }

        public void SetAlive(bool value)
        {
            IsAlive = value;
        }

        protected virtual void TakeDamage(float damage)
        {
            if (!IsAlive)
            {
                return;
            }

            Health -= damage * (1f - Armor);

            if (Health <= 0f)
            {
                Die();
            }
        }

        public virtual void Die()
        {
            SetAlive(false);
            OnDie?.Invoke();

            UnitMover.SetAlive(false);

            GameTimer gameTimer = new GameTimer(3);
            gameTimer.Start(() => UnitMover.Destroy());

            //_unitMover.Destroy();

            //KhtPool.ReturnObject(_gameObject);
        }
    }
}
