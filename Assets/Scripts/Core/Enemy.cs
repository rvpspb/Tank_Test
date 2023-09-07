using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimplePool;
using System;
using tank.config;
using tank.input;
using tank.factory;

namespace tank.core
{
    public class Enemy : Unit
    {
       
        //private readonly UnitMover _unitMover;
        private float _damage;
        private BotAI _botAI;

        public Enemy(EnemyConfig enemyConfig, UnitMover enemyMover, Transform target)
        {
            Health = enemyConfig.Health;
            Armor = enemyConfig.Armor;
            _damage = enemyConfig.Damage;

            UnitMover = enemyMover;
            UnitMover.OnTakeDamage += TakeDamage;
            _gameObject = enemyMover.gameObject;

            _botAI = _gameObject.GetComponent<BotAI>();
            _botAI.Construct(target);
            _botAI.OnRotateDirectionChange += SetMoverRotation;

            UnitMover.SetMoveDirection(1f);

            SetAlive(true);

            //_gameObject = KhtPool.GetObject(enemyConfig.Prefab);
            //_gameObject.transform.position = position;

            //_unitMover = _gameObject.GetComponent<UnitMover>();
            //_unitMover.Construct(enemyConfig);

        }
               
        private void SetMoverRotation(float direction)
        {
            UnitMover.SetRotateDirection(direction);
        }

        public override void Die()
        {
            UnitMover.OnTakeDamage -= TakeDamage;
            _botAI.OnRotateDirectionChange -= SetMoverRotation;
            base.Die();
        }

        //~Player()
        //{
        //    _input.OnUpdate -= GetInput;
        //}
    }
}
