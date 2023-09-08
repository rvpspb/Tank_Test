
using UnityEngine;
using tank.config;
using System;
using SimplePool;

namespace tank.core
{
    public class Bullet : MonoBehaviour, IDestroy<Bullet>
    {
        [SerializeField] private LayerMask _checkLayers;

        private WeaponConfig _weaponConfig;
        private float _moveSpeed;
        private float _damage;
        private readonly float _lifeTime = 2f;
        private float _bornTime;
        private readonly float _pushForce = 0.5f;

        public event Action<Bullet> OnDestroyed;

        public void Construct(WeaponConfig weaponConfig, float bulletDamage)
        {
            _weaponConfig = weaponConfig;
            transform.localScale = _weaponConfig.BulletScale * Vector3.one;
            _moveSpeed = _weaponConfig.BulletSpeed;
            _damage = bulletDamage;
            _bornTime = Time.time;            
        }

        private void Update()
        {
            Move();
            CheckFront();

            if (_bornTime + _lifeTime < Time.time)
            {
                Die();
            }
        }

        private void Move()
        {
            transform.position += _moveSpeed * Time.deltaTime * transform.forward;
        }

        private void CheckFront()
        {
            float distance = 2f * _moveSpeed * Time.deltaTime;
            Vector3 end = transform.position;
            Vector3 start = end - transform.forward * distance;
            RaycastHit raycastHit;

            if (!Physics.Linecast(start, end, out raycastHit, _checkLayers))
            {
                return;
            }

            Rigidbody hitRigidbody = raycastHit.collider.attachedRigidbody;

            if (!hitRigidbody)
            {
                VFXProvider.Instance.Emit(VFXType.SparksSplash, raycastHit.point, Quaternion.LookRotation(raycastHit.normal, Vector3.up));
                Die();
                return;
            }

            if (hitRigidbody.TryGetComponent(out IDamageable damageable))
            {
                hitRigidbody.AddForce(_damage * _pushForce * transform.forward, ForceMode.Impulse);
                damageable.TakeDamage(_damage);
                VFXProvider.Instance.Emit(VFXType.BloodSplash, raycastHit.point, Quaternion.LookRotation(raycastHit.normal, Vector3.up));
                Die();                
            }
        }

        private void Die()
        {
            KhtPool.ReturnObject(gameObject);
            OnDestroyed?.Invoke(this);
        }
    }
}
