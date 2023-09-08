
using UnityEngine;
using tank.factory;
using tank.config;

namespace tank.core
{
    public class BulletSpawner : MonoSpawner<Bullet>
    {        
        private BulletFactory _bulletFactory;
        
        public void Construct(BulletFactory bulletFactory)
        {
            base.Construct();
            _bulletFactory = bulletFactory;            
        }

        public Bullet Spawn(WeaponConfig weaponConfig, Vector3 position, Quaternion rotation, float bulletDamage)
        {
            Bullet instance = _bulletFactory.GetNewInstance(weaponConfig);
            AddToSpawned(instance);
            instance.Construct(weaponConfig, bulletDamage);
            instance.transform.localScale = weaponConfig.BulletScale * Vector3.one;
            instance.transform.SetPositionAndRotation(position, rotation);            
            return instance;
        }
    }
}
