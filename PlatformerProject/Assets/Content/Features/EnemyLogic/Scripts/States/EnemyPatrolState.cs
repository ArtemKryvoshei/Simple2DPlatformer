using System;
using Content.Features.EnemyLogic.Scripts.CommonEntitiesScripts;
using UnityEngine;

namespace Content.Features.EnemyLogic.Scripts.States
{
    public class EnemyPatrolState : EnemyState
    {
        [SerializeField] private float patrolSpeed;
        [Tooltip("-1 = left, 1 = right")]
        [Range(-1, 1)]
        [SerializeField] private int _direction;
        [SerializeField] private Rigidbody2D rb;

        private bool _canMoveLeft = true;
        private bool _canMoveRight = true;
        private int _objectId;

        public override void EnterState()
        {
            _objectId = Enemy.gameObject.GetInstanceID();

            _eventBus.Subscribe<CanMoveLeftEvent>(OnCanMoveLeft);
            _eventBus.Subscribe<CanMoveRightEvent>(OnCanMoveRight);
        }

        public override void UpdateState()
        {
            if (!_canMoveLeft && !_canMoveRight)
            {
                ExitPatrol();
                return;
            }
            
            if (_direction == -1 && !_canMoveLeft)
            {
                ExitPatrol();
                return;
            }

            if (_direction == 1 && !_canMoveRight)
            {
                ExitPatrol();
                return;
            }
            
            rb.velocity = new Vector2(patrolSpeed * _direction, rb.velocity.y);
        }

        private void ExitPatrol()
        {
            rb.velocity = Vector2.zero;
            Enemy.ToIdle();
        }

        public override void ExitState()
        {
            _eventBus.Unsubscribe<CanMoveLeftEvent>(OnCanMoveLeft);
            _eventBus.Unsubscribe<CanMoveRightEvent>(OnCanMoveRight);
            rb.velocity = Vector2.zero;
            FlipDirection();
        }

        public override void OnZenjectConstruct() { }

        private void FlipDirection()
        {
            _direction *= -1;
        }

        private void OnCanMoveLeft(CanMoveLeftEvent evt)
        {
            if (evt.ObjectId == _objectId)
                _canMoveLeft = evt.CanMoveLeft;
        }

        private void OnCanMoveRight(CanMoveRightEvent evt)
        {
            if (evt.ObjectId == _objectId)
                _canMoveRight = evt.CanMoveRight;
        }

        private void OnDestroy()
        {
            _eventBus.Unsubscribe<CanMoveLeftEvent>(OnCanMoveLeft);
            _eventBus.Unsubscribe<CanMoveRightEvent>(OnCanMoveRight);
        }
    }

}