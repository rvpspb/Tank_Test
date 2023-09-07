using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace tank.config
{
    [CreateAssetMenu(fileName = "WeaponConfigSet", menuName = "Configs/WeaponConfigSet", order = 1)]
    public class WeaponConfigSet : ScriptableObject
    {
        [field: SerializeField] public List<WeaponConfig> WeaponConfigs { get; private set; }
        public int WeaponsCount => WeaponConfigs.Count;
        
        public WeaponConfig GetWeaponConfig(WeaponType weaponType)
        {
            return WeaponConfigs.First(item => item.WeaponType == weaponType);
        }
    }
}