using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using tank.core;

namespace tank.config
{
    [CreateAssetMenu(fileName = "WeaponConfig", menuName = "Configs/WeaponConfig", order = 1)]
    public class WeaponConfig : ScriptableObject
    {
        [field: SerializeField] public WeaponType WeaponType { get; private set; }
        [field: SerializeField] public Weapon WeaponPrefab { get; private set; }
        [field: SerializeField] public float Damage { get; private set; }
        [field: SerializeField] public float FireRate { get; private set; }
        [field: SerializeField] public Bullet BulletPrefab { get; private set; }
        [field: SerializeField] public float BulletSpeed { get; private set; }
        [field: SerializeField] public float BulletScale { get; private set; }
    }
}