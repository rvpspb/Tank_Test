using UnityEngine;
using tank.factory;
using npg.bindlessdi;

namespace tank.core
{
    public class Level : MonoBehaviour
    {
        [field: SerializeField] public Transform PlayerStartPoint { get; private set; }
        [SerializeField] private BulletSpawner _bulletSpawner;
        [SerializeField] private EnemySpawner _enemySpawner;
        
        public void Construct()
        {
            Container container = Container.Initialize();                 
            BulletFactory bulletFactory = container.Resolve<BulletFactory>();           
            _bulletSpawner.Construct(bulletFactory);
            container.BindInstance(_bulletSpawner);
            Player player = container.Resolve<Player>(npg.bindlessdi.Instantiation.InstantiationPolicy.Transient);
            container.BindInstance(player);
            _enemySpawner.Construct(player.UnitMover.transform);            
        }

        public void StartSpawn()
        {            
            _enemySpawner.SetActive(true);
        }        

        public void ClearSpawn()
        {            
            _bulletSpawner.ClearSpawned();
            _enemySpawner.ClearEnemies();
            _enemySpawner.ClearSpawned();
        }
    }
}
