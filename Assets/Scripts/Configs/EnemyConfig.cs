using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace tank.config
{
    [CreateAssetMenu(fileName = "EnemyConfig", menuName = "Configs/EnemyConfig", order = 0)]
    public class EnemyConfig : UnitConfig
    {
        [field: SerializeField] public float Damage { get; private set; }
        [field: SerializeField] public float ReactionTime { get; private set; }
    }
}
