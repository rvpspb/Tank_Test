
using UnityEngine;
using tank.factory;
using tank.config;
using npg.bindlessdi;

namespace tank.core
{
    public class WeaponSpawner : MonoSpawner<Weapon>
    {
        
        private WeaponFactory _weaponFactory;
        private WeaponConfigSet _weaponConfigSet;        
        private int _currentWeaponNumber;
        private int _maxWeaponNumber;
        private Transform _weaponParent;
        private float _lastTimeWeaponSwitch;
        private readonly float _weaponSwitchDelay = 0.25f;

        public void Construct(Transform weaponParent)
        {
            base.Construct();

            _weaponParent = weaponParent;

            Container container = Container.Initialize();

            _weaponFactory = container.Resolve<WeaponFactory>();
            _weaponConfigSet = container.Resolve<WeaponConfigSet>();

            _maxWeaponNumber = _weaponConfigSet.WeaponsCount - 1;
            _currentWeaponNumber = 0;

            _lastTimeWeaponSwitch = Time.time;
        }

        public bool SwitchWeapon(float change)
        {
            if (_lastTimeWeaponSwitch + _weaponSwitchDelay > Time.time || change == 0f)
            {
                return false;
            }

            if (change > 0)
            {
                _currentWeaponNumber++;

                if (_currentWeaponNumber > _maxWeaponNumber)
                {
                    _currentWeaponNumber = 0;
                }
            }
            else
            {
                _currentWeaponNumber--;

                if (_currentWeaponNumber < 0)
                {
                    _currentWeaponNumber = _maxWeaponNumber;
                }
            }

            _lastTimeWeaponSwitch = Time.time;

            return true;
        }

        public Weapon Spawn()
        {            
            Weapon weapon = _weaponFactory.GetNewInstance(_currentWeaponNumber);
            AddToSpawned(weapon);
            weapon.transform.SetParent(_weaponParent);
            weapon.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);            
            return weapon;
        }
    }
}
