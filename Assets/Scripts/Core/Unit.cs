
using UnityEngine;
using System;
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

            float destroyDelay = 3f;
            GameTimer gameTimer = new GameTimer(destroyDelay);
            gameTimer.Start(() => UnitMover.Destroy());
        }
    }
}
