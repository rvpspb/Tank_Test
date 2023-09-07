using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using tank.config;
using System;
using SimplePool;

namespace tank.core
{
    public class UnitMover : MonoBehaviour, IDestroy<UnitMover>, IDamageable
    {
        private float _moveDirection;
        private float _rotateDirection;
        private Rigidbody _rigidbody;
        private float _moveSpeed;
        private float _rotateSpeed;
        private float _acceleration = 10f;
        private Vector3 _lookDirection;
        private bool _isAlive;

        public event Action<UnitMover> OnDestroyed;
        public event Action<float> OnTakeDamage;

        public void Construct(UnitConfig unitConfig)
        {
            _rigidbody = GetComponent<Rigidbody>();
            //_rigidbody.centerOfMass = Vector3.up;
            _moveSpeed = unitConfig.MoveSpeed;
            _rotateSpeed = unitConfig.RotationSpeed;

            SetAlive(true);
            //_lookDirection = transform.forward;
        }

        public void SetMoveDirection(float value)
        {
            _moveDirection = value;
        }

        public void SetRotateDirection(float value)
        {
            _rotateDirection = value;
        }

        private void FixedUpdate()
        {
            if (!_isAlive)
            {
                return;
            }

            SolveVelocity();
        }

        private void SolveVelocity()
        {
            Vector3 needVelocity = _moveDirection * _moveSpeed * transform.forward;
            needVelocity.y = _rigidbody.velocity.y;
            Vector3 needAngularVelocity = _rigidbody.angularVelocity;
            needAngularVelocity.y = _rotateSpeed * _rotateDirection * Mathf.Sign(_moveDirection);

            _rigidbody.velocity = Vector3.Lerp(_rigidbody.velocity, needVelocity, _acceleration * Time.fixedDeltaTime);
            _rigidbody.angularVelocity = Vector3.Lerp(_rigidbody.angularVelocity, needAngularVelocity, _acceleration * Time.fixedDeltaTime);
                        
        }    
        
        public void TakeDamage(float damage)
        {
            OnTakeDamage?.Invoke(damage);            
        }

        public void SetAlive(bool value)
        {
            _isAlive = value;
            _rigidbody.constraints = _isAlive ? RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ : RigidbodyConstraints.None;            
        }

        public void Jump()
        {
            _rigidbody.AddForce(10f * _rigidbody.mass * Vector3.up, ForceMode.Impulse);
        }

        public void Destroy()
        {
            OnDestroyed?.Invoke(this);
            KhtPool.ReturnObject(gameObject);
        }
    }
}
