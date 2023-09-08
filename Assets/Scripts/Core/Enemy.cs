
using UnityEngine;
using tank.config;

namespace tank.core
{
    public class Enemy : Unit
    {       
        private BotAI _botAI;

        public Enemy(EnemyConfig enemyConfig, UnitMover enemyMover, Transform target)
        {
            Health = enemyConfig.Health;
            Armor = enemyConfig.Armor;            

            UnitMover = enemyMover;
            UnitMover.OnTakeDamage += TakeDamage;
            _gameObject = enemyMover.gameObject;

            _botAI = _gameObject.GetComponent<BotAI>();
            _botAI.Construct(target, enemyConfig.Damage, enemyConfig.ReactionTime);
            _botAI.OnRotateDirectionChange += SetMoverRotation;
            _botAI.OnLostTarget += StopMover;

            UnitMover.SetMoveDirection(1f);
            SetAlive(true);
        }
               
        private void SetMoverRotation(float direction)
        {
            UnitMover.SetRotateDirection(direction);
        }

        private void StopMover()
        {
            UnitMover.SetRotateDirection(0f);
            UnitMover.SetMoveDirection(0f);
        }

        public override void Die()
        {
            UnitMover.OnTakeDamage -= TakeDamage;
            _botAI.OnRotateDirectionChange -= SetMoverRotation;
            _botAI.OnLostTarget -= StopMover;
            base.Die();
        }
    }
}
