using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace tank.config
{
    [CreateAssetMenu(fileName = "UnitConfig", menuName = "Configs/UnitConfig", order = 0)]
    public class UnitConfig : ScriptableObject
    {
        [field: SerializeField] public GameObject Prefab { get; private set; }
        [field: SerializeField] public float StartHealth { get; private set; }
        [field: SerializeField] public float MoveSpeed { get; private set; }
    }
}
