using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using tank.config;

namespace tank.core
{
    public class UnitMover : MonoBehaviour
    {
        private float _moveDirection;
        private float _rotateDirection;
        private Rigidbody _rigidbody;
        private float _moveSpeed;
        private float _rotateSpeed;
        private float _acceleration = 0.2f;
        

        public void Construct(UnitConfig unitConfig)
        {
            _rigidbody = GetComponent<Rigidbody>();
            _moveSpeed = unitConfig.MoveSpeed;
            _rotateSpeed = unitConfig.RotationSpeed;
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
            Vector3 needVelocity = _moveDirection * _moveSpeed * transform.forward;
            Vector3 needAngularVelocity = new Vector3(0, _rotateSpeed * _rotateDirection * Mathf.Sign(_moveDirection), 0f);

            _rigidbody.velocity = Vector3.Lerp(_rigidbody.velocity, needVelocity, _acceleration);
            _rigidbody.angularVelocity = Vector3.Lerp(_rigidbody.angularVelocity, needAngularVelocity, _acceleration);

        }

        
    }
}
