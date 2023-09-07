using UnityEngine;
using System;
using tank.config;

namespace tank.core
{
    public class BotAI : MonoBehaviour
    {                
        private Transform _target;        
        
        public event Action<float> OnRotateDirectionChange;

        public void Construct(Transform target)
        {
            _target = target;            
        }

        private void FixedUpdate()
        {
            if (!_target)
            {
                return;
            }

            SolveRotateDirection();
        }

        private void SolveRotateDirection()
        {
            float direction;
            Vector3 targetDirection = _target.position - transform.position;
            targetDirection.y = 0f;
            Quaternion delta = Quaternion.FromToRotation(transform.forward, targetDirection);
            direction = Mathf.Clamp( delta.y, -1f, 1f);
            OnRotateDirectionChange?.Invoke(direction);
        }
    }
}
