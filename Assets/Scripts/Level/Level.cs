using UnityEngine;
using tank.factory;
using tank.config;
using tank.input;
using npg.bindlessdi;
using System.Collections;
using System.Collections.Generic;

namespace tank.core
{
    public class Level : MonoBehaviour
    {
        [field: SerializeField] public Transform PlayerStartPoint { get; private set; }
        [SerializeField] private BulletSpawner _bulletSpawner;
        [SerializeField] private EnemySpawner _enemySpawner;
        [SerializeField] private int _maxEnemiesCount;

        private Player _player;
        private List<Enemy> _enemies;
        //[field: SerializeField] public BulletSpawner BulletSpawner { get; private set; }

        public void Construct()
        {
            Container container = Container.Initialize();
            BulletFactory bulletFactory = container.Resolve<BulletFactory>();

            _bulletSpawner.Construct(bulletFactory);
            container.BindInstance(_bulletSpawner);
            _enemySpawner.Construct(_maxEnemiesCount);
            //container.BindInstance(this);

            _player = container.Resolve<Player>();
            _enemies = new List<Enemy>();
            //Player player = new Player()
        }

        public void StartSpawn()
        {
            //Container.Initialize().Resolve<Player>();
            _enemySpawner.StartSpawn();
        }

        public void StopSpawn()
        {
            //Container.Initialize().Resolve<Player>();

        }

        public void ClearSpawn()
        {
            _player.Die();
            _bulletSpawner.ClearSpawned();
            _enemySpawner.ClearSpawned();

        }
    }
}
