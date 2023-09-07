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
        private readonly IInput _input;
        //public readonly UnitMover _unitMover;              
        private readonly WeaponSpawner _weaponSpawner;
        private Weapon _weapon;

        public Player(PlayerConfig playerConfig, Level level, IInput input, CameraMover cameraMover)
        {
            Health = playerConfig.Health;
            Armor = playerConfig.Armor;

            _gameObject = KhtPool.GetObject(playerConfig.Prefab);
            _gameObject.transform.position = level.PlayerStartPoint.position;
            _gameObject.transform.rotation = level.PlayerStartPoint.rotation;
            _gameObject.SetActive(true);

            _input = input;
            _input.OnUpdate += GetInput;
            
            UnitMover = _gameObject.GetComponent<UnitMover>();
            UnitMover.OnTakeDamage += TakeDamage;
            UnitMover.Construct(playerConfig);

            _weaponSpawner = _gameObject.GetComponent<WeaponSpawner>();
            _weaponSpawner.Construct(_gameObject.transform);
            LoadWeapon();

            cameraMover.SetTarget(_gameObject.transform);

            SetAlive(true);
        }

        private void GetInput()
        {
            UnitMover.SetMoveDirection(_input.Vertical);
            UnitMover.SetRotateDirection(_input.Horizontal);

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
        }

        public override void Die()
        {
            _weaponSpawner.ClearSpawned();
            _input.OnUpdate -= GetInput;            
            base.Die();
            UnitMover.Jump();
        }

        

        //~Player()
        //{
        //    _input.OnUpdate -= GetInput;
        //}
    }
}
