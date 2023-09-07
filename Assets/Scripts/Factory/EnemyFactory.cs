using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using tank.core;
using tank.config;
using SimplePool;

namespace tank.factory
{
    public class EnemyFactory : MonoFactory<UnitMover>
    {
        private readonly EnemyConfigSet _enemyConfigSet;

        public EnemyFactory(EnemyConfigSet enemyConfigSet)
        {
            _enemyConfigSet = enemyConfigSet;
        }

        public UnitMover GetNewInstance(int enemyNumber)
        {
            EnemyConfig enemyConfig = _enemyConfigSet.EnemyConfigs[enemyNumber];
            
            GameObject gameObject = KhtPool.GetObject(enemyConfig.Prefab);

            gameObject.SetActive(true);
            UnitMover instance = gameObject.GetComponent<UnitMover>();
            instance.Construct(enemyConfig);
            return instance;
        }
    }
}
