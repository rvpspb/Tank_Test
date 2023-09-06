using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using tank.core;
using tank.config;
using SimplePool;

namespace tank.factory
{
    public class WeaponFactory : MonoFactory<Weapon>
    {
        private WeaponConfigSet _weaponConfigSet;
        
        public WeaponFactory(WeaponConfigSet weaponConfigSet)
        {
            _weaponConfigSet = weaponConfigSet;
        }

        public Weapon GetNewInstance(int weaponNumber)
        {
            WeaponConfig weaponConfig = _weaponConfigSet.WeaponConfigs[weaponNumber];
            _prefab = weaponConfig.WeaponPrefab;

            GameObject gameObject = KhtPool.GetObject(_prefab.gameObject);
            gameObject.SetActive(true);
            Weapon inctance = gameObject.GetComponent<Weapon>();
            inctance.Construct(weaponConfig);
            return inctance;
        }
    }
}
