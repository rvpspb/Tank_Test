using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMover : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 10;

    private Transform _target;
    private bool _hasTarget;
    

    public void SetTarget(Transform target)
    {
        _target = target;
        _hasTarget = true;
        transform.position = _target.position;
    }

    private void LateUpdate()
    {
        FollowTarget();
    }

    private void FollowTarget()
    {
        if (_hasTarget && !_target)
        {
            _hasTarget = false;
            return;
        }

        if (!_hasTarget)
        {
            return;
        }

        transform.position = Vector3.Lerp(transform.position, _target.position, _moveSpeed * Time.deltaTime);
    }
}
