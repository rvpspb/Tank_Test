
using UnityEngine;
using tank.config;
using npg.bindlessdi;
using System;

namespace tank.core
{
    public class Weapon : MonoBehaviour, IDestroy<Weapon>
    {
        [SerializeField] private Transform[] _shootPoints;

        private WeaponConfig _weaponConfig;
        private BulletSpawner _bulletSpawner;
        protected float _lastTimeShoot;        
        protected float _shootPeriod;
        private float _bulletDamage;

        public event Action<Weapon> OnDestroyed;

        public void Construct(WeaponConfig weaponConfig)
        {
            Container container = Container.Initialize();
            _bulletSpawner = container.Resolve<BulletSpawner>();
            _weaponConfig = weaponConfig;

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
                _bulletSpawner.Spawn(_weaponConfig, _shootPoints[i].position, _shootPoints[i].rotation, _bulletDamage);
            }

            _lastTimeShoot = Time.time;
        }               
    }
}
