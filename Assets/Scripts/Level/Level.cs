using UnityEngine;
using tank.factory;
using tank.config;
using tank.input;
using npg.bindlessdi;

namespace tank.core
{
    public class Level : MonoBehaviour
    {
        [field: SerializeField] public Transform PlayerStartPoint { get; private set; }
        [field: SerializeField] public BulletSpawner BulletSpawner { get; private set; }


        public void Construct()
        {
            Container container = Container.Initialize();

            BulletFactory bulletFactory = container.Resolve<BulletFactory>();

            BulletSpawner.Construct(bulletFactory);
            container.BindInstance(BulletSpawner);

            container.Resolve<Player>();
        }
                
    }
}
