
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace tank.core
{
    public class VFXProvider : MonoBehaviour
    {

        [SerializeField] private List<Effect> _effects;
        public static VFXProvider Instance { get; private set; }

        private void Awake()
        {
            Instance = this;
        }

        public void Emit(VFXType type, Vector3 position, Quaternion rotation, int count = 0, float scale = 1f)
        {
            Effect effect = _effects.FirstOrDefault(item => item.VFXType == type);            
            effect.ParticleSystem.transform.SetPositionAndRotation(position, rotation);
            effect.ParticleSystem.transform.localScale = scale * Vector3.one;

            if (count == 0)
            {
                count = effect.EmitCount;
            }

            effect.ParticleSystem.Emit(count);
        }

        [System.Serializable]
        public class Effect
        {
            public VFXType VFXType;
            public ParticleSystem ParticleSystem;
            public int EmitCount;
        }
    }
}

public enum VFXType
{
    BloodSplash,
    SparksSplash
}
