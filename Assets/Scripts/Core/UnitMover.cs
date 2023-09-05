using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace tank.core
{
    public class UnitMover : MonoBehaviour
    {
        private float _moveDirection;
        private float _rotateDirection;
        private Rigidbody _rigidbody;
        private float _moveSpeed;
        private float _rotateSpeed;

        public void Construct()
        {
            _rigidbody = GetComponent<Rigidbody>();

            _moveSpeed = 10;

            _rotateSpeed = 5;
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
            _rigidbody.velocity = _moveDirection * _moveSpeed * transform.forward;

            _rigidbody.angularVelocity = new Vector3(0, _rotateSpeed * _rotateDirection * Mathf.Sign(_moveDirection), 0f);

            //Quaternion rotationAdd = Quaternion.AngleAxis(_rotateSpeed * _rotateDirection * Time.fixedDeltaTime, Vector3.up);

            //_rigidbody.MoveRotation(rotationAdd * _rigidbody.rotation);
        }

        
    }
}
