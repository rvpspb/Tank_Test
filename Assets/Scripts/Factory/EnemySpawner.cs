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
        private int _needCount;
        private bool _isActive;
        private Camera _camera;
        private Transform _playerTransform;

        public void Construct(Transform target)
        {
            base.Construct();

            Container container = Container.Initialize();

            _enemyFactory = container.Resolve<EnemyFactory>();
            _enemyConfigSet = container.Resolve<EnemyConfigSet>();
            GameConfig gameConfig = container.Resolve<GameConfig>();

            _playerTransform = target;
            _needCount = gameConfig.BotsCount;
            _camera = Camera.main;
            _isActive = false;
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
            Vector3 spawnPosition;

            spawnPosition.x = UnityEngine.Random.Range(_spawnVolume.bounds.min.x, _spawnVolume.bounds.max.x);
            spawnPosition.y = 0f;
            spawnPosition.z = UnityEngine.Random.Range(_spawnVolume.bounds.min.z, _spawnVolume.bounds.max.z);
            
            if (CheckVisible(spawnPosition))
            {
                enemy = null;
                return false;
            }

            int enemyNumber = UnityEngine.Random.Range(0, _enemyConfigSet.EnemiesCount);
            UnitMover unitMover = Spawn(enemyNumber, spawnPosition);

            enemy = new Enemy(_enemyConfigSet.EnemyConfigs[enemyNumber], unitMover, _playerTransform);            
            return true;
        }

        public UnitMover Spawn(int enemyNumber, Vector3 position)
        {
            UnitMover enemyMover = _enemyFactory.GetNewInstance(enemyNumber);
            AddToSpawned(enemyMover);            
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
