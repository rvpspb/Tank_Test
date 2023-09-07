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

        //private Player _player;
        //private List<Enemy> _enemies;
        //[field: SerializeField] public BulletSpawner BulletSpawner { get; private set; }

        public void Construct()
        {
            Container container = Container.Initialize();

                 
            BulletFactory bulletFactory = container.Resolve<BulletFactory>();

            

            _bulletSpawner.Construct(bulletFactory);
            container.BindInstance(_bulletSpawner);
            Player player = container.Resolve<Player>(npg.bindlessdi.Instantiation.InstantiationPolicy.Transient);
            container.BindInstance(player);
            _enemySpawner.Construct(player.UnitMover.transform, _maxEnemiesCount);
            //container.BindInstance(this);
            
            
            
            
            //_enemies = new List<Enemy>();
            //Player player = new Player()
        }

        public void StartSpawn()
        {            
            _enemySpawner.StartSpawn();
        }        

        public void ClearSpawn()
        {           
            _bulletSpawner.ClearSpawned();
            _enemySpawner.ClearSpawned();
        }
    }
}
