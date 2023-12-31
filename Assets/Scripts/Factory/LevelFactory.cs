using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using tank.core;

namespace tank.factory
{
    public class LevelFactory : MonoFactory<Level>
    {
        public LevelFactory(Level levelPrefab)
        {
            _prefab = levelPrefab;
        }
    }
}
