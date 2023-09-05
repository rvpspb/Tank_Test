using UnityEngine;
using System;
using tank.config;

namespace tank.core
{
    public class BotAI : MonoBehaviour
    {
        
        private float _checkTime;
        private int _checkDirection;
        private int _lastMoveDirection;
        private float _proxy;
        private bool _ballWasNear;
        
        private Collider[] _colliders = new Collider[1];        
        
        public event Action<float> OnDirectionChange;

        public void Construct()
        {
            
            _proxy = 0.5f * transform.localScale.y;
            _lastMoveDirection = 0;
            _ballWasNear = false;
        }
        

        
    }
}
