using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using tank.config;
using tank.factory;
using npg.bindlessdi;
using System;

namespace tank.core
{
    public class Weapon : MonoBehaviour, IDestroy<Weapon>
    {
        //[SerializeField] protected BonusType _type;

        public event Action<Weapon> OnDestroyed;

        //private GameObject _bulletPrefab;

        //[SerializeField] private Transform _bulletSpawnPoint;

        //[SerializeField] protected ParticleSystem _shootFX;

        //[SerializeField] private WeaponBonusSO _bonus;

        //[SerializeField] protected Transform _gun;

        //[SerializeField] protected ShootPoints _shootPoints;

        [SerializeField] private Transform[] _shootPoints;

        //[SerializeField] protected float _gunOffset = 0.03f;

        //[SerializeField] protected float _minInterval = 0.02f;

        //[SerializeField] protected bool _autoShooting = true;

        //protected WeaponBonusSO _weaponBonus;

        private WeaponConfig _weaponConfig;
        private BulletSpawner _bulletSpawner;

        protected float _lastTimeShoot;
        //protected float _shootTime;
        protected float _shootPeriod;

        private float _bulletDamage;
        
        

        public void Construct(WeaponConfig weaponConfig)
        {
            Container container = Container.Initialize();
            _bulletSpawner = container.Resolve<BulletSpawner>();
            _weaponConfig = weaponConfig;

            //_weaponConfig = weaponConfig;
            //_bulletSpawner = bulletSpawner;

            _lastTimeShoot = Time.time;
            _bulletDamage = _weaponConfig.Damage / _shootPoints.Length;
            _shootPeriod = 1f / _weaponConfig.FireRate;
        }

        public void Shot()
        {
            if (_lastTimeShoot + _shootPeriod > Time.time)
            {
                return;
            }

            for (int i = 0; i < _shootPoints.Length; i++)
            {
                ShootSingle(_shootPoints[i], _bulletDamage);
            }

            _lastTimeShoot = Time.time;
        }
                
        protected virtual void ShootSingle(Transform point, float power)
        {

            Bullet newBullet = _bulletSpawner.Spawn(_weaponConfig, point.position, point.rotation);
            newBullet.Construct(_weaponConfig, power);            
        }
    }
}
