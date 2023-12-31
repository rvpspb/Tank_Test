
using UnityEngine;
using tank.config;
using tank.core;
using SimplePool;

namespace tank.factory
{
    public class BulletFactory : MonoFactory<Bullet>
    {        
        public Bullet GetNewInstance(WeaponConfig weaponConfig)
        {
            GameObject gameObject = KhtPool.GetObject(weaponConfig.BulletPrefab.gameObject);
            gameObject.SetActive(true);
            Bullet inctance = gameObject.GetComponent<Bullet>();
            return inctance;
        }
    }
}
