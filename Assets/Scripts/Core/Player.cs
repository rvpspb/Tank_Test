using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimplePool;
using System;
using tank.config;
using tank.input;
using tank.factory;

namespace tank.core
{
    public class Player : Unit
    {
        //public UnitView View { get; private set; }

        private IInput _input;

        private UnitMover _unitMover;
        private Weapon _weapon;
        //private WeaponFactory _weaponFactory;
        private WeaponSpawner _weaponSpawner;
        

        public Player(PlayerConfig playerConfig, WeaponFactory weaponFactory, WeaponConfigSet weaponConfigSet, IInput input, CameraMover cameraMover)
        { 
            _gameObject = KhtPool.GetObject(playerConfig.Prefab);
            
            _input = input;
            _input.OnUpdate += GetInput;
            
            _unitMover = _gameObject.GetComponent<UnitMover>();
            _unitMover.Construct(playerConfig);

            _weaponSpawner = _gameObject.GetComponent<WeaponSpawner>();
            _weaponSpawner.Construct(_gameObject.transform);

            cameraMover.SetTarget(_gameObject.transform);



            //_weaponFactory = weaponFactory;
            //_currentWeaponType = WeaponType.BaseGun;
            LoadWeapon();
        }

        private void GetInput()
        {
            _unitMover.SetMoveDirection(_input.Vertical);
            _unitMover.SetRotateDirection(_input.Horizontal);

            if (_weaponSpawner.SwitchWeapon(_input.WeaponWheel))
            {
                LoadWeapon();
            }

            if (_input.IsFireHold)
            {
                _weapon.Shot();                
            }
        }

        

        private void LoadWeapon()
        {
            _weaponSpawner.ClearSpawned();

            _weapon = _weaponSpawner.Spawn();

            //_weapon = _weaponFactory.GetNewInstance(_currentWeaponType);
            //_weapon.transform.SetParent(_gameObject.transform);
            //_weapon.transform.localPosition = Vector3.zero;
            //_weapon.transform.localRotation = Quaternion.identity;
        }

        protected override void Die()
        {
            _input.OnUpdate -= GetInput;

            base.Die();
        }

        ~Player()
        {
            _input.OnUpdate -= GetInput;
        }
    }
}
