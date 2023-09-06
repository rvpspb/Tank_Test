using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMover : MonoBehaviour
{
    private Transform _target;
    private bool _hasTarget;
    private float _moveSpeed = 10;

    public void SetTarget(Transform target)
    {
        _target = target;
        _hasTarget = true;
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
