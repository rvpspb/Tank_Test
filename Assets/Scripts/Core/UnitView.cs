using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using tank.config;

namespace tank.core
{
    public class UnitView : MonoBehaviour
    {
        public event Action OnBulletHit;

        
        public void Destroy()
        {
            Destroy(gameObject);
        }
    }
}
