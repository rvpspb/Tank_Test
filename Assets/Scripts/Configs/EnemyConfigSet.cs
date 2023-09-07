using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace tank.config
{
    [CreateAssetMenu(fileName = "EnemyConfigSet", menuName = "Configs/EnemyConfigSet", order = 1)]
    public class EnemyConfigSet : ScriptableObject
    {
        [field: SerializeField] public List<EnemyConfig> EnemyConfigs { get; private set; }
        public int EnemiesCount => EnemyConfigs.Count;        
    }
}