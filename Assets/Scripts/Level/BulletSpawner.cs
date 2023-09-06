using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using tank.factory;
using tank.config;
using npg.bindlessdi;

namespace tank.core
{
    public class BulletSpawner : MonoSpawner<Bullet>
    {
        //private Weapo

        private BulletFactory _bulletFactory;
        

        public void Construct(BulletFactory bulletFactory)
        {
            base.Construct();
            _bulletFactory = bulletFactory;
            //_spawned = new List<Bullet>();
        }

        public Bullet Spawn(WeaponConfig weaponConfig, Vector3 position, Quaternion rotation)
        {
            Bullet instance = _bulletFactory.GetNewInstance(weaponConfig);
            AddToSpawned(instance);
            instance.transform.localScale = weaponConfig.BulletScale * Vector3.one;
            instance.transform.SetPositionAndRotation(position, rotation);            
            //_spawned.Add(instance);
            //instance.OnDestroyed += RemoveFromSpawned;
            return instance;
        }
    }
}
