using UnityEngine;
using System;

namespace tank.core
{
    public class BotAI : MonoBehaviour
    {
        private float _damage;
        private readonly float _attackDelay = 0.5f;
        private float _lastAttackTime;
        private Transform _target;
        private bool _hasTarget;
        private float _reactionTime;
        private float _lastLookTime;

        public event Action<float> OnRotateDirectionChange;
        public event Action OnLostTarget;

        public void Construct(Transform target, float damage, float reactionTime)
        {
            _target = target;
            _hasTarget = true;
            _damage = damage;
            _reactionTime = reactionTime;
            _lastLookTime = Time.time;
            _lastAttackTime = Time.time;
        }

        private void FixedUpdate()
        {
            if (!_hasTarget)
            {
                return;
            }
            else if (_hasTarget && !_target.gameObject.activeInHierarchy)
            {
                _hasTarget = false;
                OnLostTarget?.Invoke();
                return;
            }

            if (_lastLookTime + _reactionTime < Time.time)
            {
                SolveRotateDirection();
                _lastLookTime = Time.time;
            }
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

        private void OnCollisionStay(Collision collision)
        {
            if (_lastAttackTime + _attackDelay > Time.time || collision.collider.gameObject.layer == gameObject.layer)
            {
                return;
            }

            Rigidbody rigidbody = collision.collider.attachedRigidbody;

            if (!rigidbody)
            {
                return;
            }

            if (rigidbody.TryGetComponent(out IDamageable damageable))
            {
                damageable.TakeDamage(_damage);
                _lastAttackTime = Time.time;
            }
        }
    }
}
