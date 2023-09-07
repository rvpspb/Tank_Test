using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using tank.factory;
using tank.config;
using npg.bindlessdi;
using System;

namespace tank.core
{
    public class EnemySpawner : MonoSpawner<UnitMover>
    {
        [SerializeField] private BoxCollider _spawnVolume;

        private EnemyFactory _enemyFactory;
        private EnemyConfigSet _enemyConfigSet;
        //private int _currentWeaponNumber;
        //private int _maxEnemyNumber;
        //private Transform _weaponParent;
        //private float _lastTimeWeaponSwitch;
        //private readonly float _weaponSwitchDelay = 0.25f;
        private int _needCount;
        private bool _isActive;
        private Camera _camera;
        private Transform _playerTransform;

        public void Construct(int needCount)
        {
            base.Construct();

            //_weaponParent = weaponParent;

            Container container = Container.Initialize();

            _enemyFactory = container.Resolve<EnemyFactory>();
            _enemyConfigSet = container.Resolve<EnemyConfigSet>();

            _playerTransform = container.Resolve<Player>().UnitMover.transform;

            _needCount = needCount;

            _camera = Camera.main;

            _isActive = false;

            //_maxEnemyNumber = _enemyConfigSet.EnemiesCount;
            //_maxWeaponNumber = _weaponConfigSet.WeaponsCount - 1;
            //_currentWeaponNumber = 0;

            //_lastTimeWeaponSwitch = Time.time;
        }

        public void StartSpawn()
        {
            _isActive = true;
        }
        

        private void Update()
        {
            if (!_isActive)
            {
                return;
            }

            if (NeedSpawn())
            {
                TrySpawn(out Enemy enemy);
            }
        }

        private bool NeedSpawn()
        {           

            return _spawned.Count < _needCount;
        }

        private bool TrySpawn(out Enemy enemy)
        {
            enemy = null;

            Vector3 spawnPosition;

            spawnPosition.x = UnityEngine.Random.Range(_spawnVolume.bounds.min.x, _spawnVolume.bounds.max.x);
            spawnPosition.y = 0f;
            spawnPosition.z = UnityEngine.Random.Range(_spawnVolume.bounds.min.z, _spawnVolume.bounds.max.z);
            
            if (CheckVisible(spawnPosition))
            {
                return false;
            }

            int enemyNumber = UnityEngine.Random.Range(0, _enemyConfigSet.EnemiesCount);

            UnitMover unitMover = Spawn(enemyNumber, spawnPosition);

            enemy = new Enemy(_enemyConfigSet.EnemyConfigs[enemyNumber], unitMover, _playerTransform);

            return true;

            //Spawn(enemyNumber, spawnPosition);
        }



        public UnitMover Spawn(int enemyNumber, Vector3 position)
        {
            UnitMover enemyMover = _enemyFactory.GetNewInstance(enemyNumber);
            AddToSpawned(enemyMover);
            //enemyMover.transform.SetParent(_weaponParent);
            enemyMover.transform.SetPositionAndRotation(position, Quaternion.identity);
            return enemyMover;
        }

        private bool CheckVisible(Vector3 worldPosition)
        {
            Vector3 screenPosition = _camera.WorldToScreenPoint(worldPosition);

            return screenPosition.z > 0
                && screenPosition.x > 0
                && screenPosition.x < Screen.width
                && screenPosition.y > 0
                && screenPosition.y < Screen.height;
        }
    }
}
