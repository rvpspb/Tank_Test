using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using tank.config;
using System;
using SimplePool;

namespace tank.core
{
    public class Bullet : MonoBehaviour, IDestroy<Bullet>
    {
        private WeaponConfig _weaponConfig;
        private float _moveSpeed;
        private float _damage;
        private float _lifeTime;
        private float _bornTime;

        public event Action<Bullet> OnDestroyed;

        public void Construct(WeaponConfig weaponConfig, float bulletDamage)
        {
            _weaponConfig = weaponConfig;
            transform.localScale = _weaponConfig.BulletScale * Vector3.one;
            _moveSpeed = _weaponConfig.BulletSpeed;
            _damage = bulletDamage;

            _bornTime = Time.time;
            _lifeTime = 2f;
        }

        private void Update()
        {
            transform.position += _moveSpeed * Time.deltaTime * transform.forward;

            if (_bornTime + _lifeTime < Time.time)
            {
                KhtPool.ReturnObject(gameObject);
                OnDestroyed?.Invoke(this);
            }
        }

        

        //private void OnDestroy()
        //{
        //    OnDestroyed?.Invoke(this);
        //}
    }
}
