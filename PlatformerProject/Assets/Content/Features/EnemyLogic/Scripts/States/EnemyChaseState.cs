using System;
using Content.Features.EnemyLogic.Scripts.CommonEntitiesScripts;
using UnityEngine;

namespace Content.Features.EnemyLogic.Scripts.States
{
    public class EnemyChaseState : EnemyState
    {
        [SerializeField] private Rigidbody2D rb;

        private float chaseSpeed;
        
        private bool _canMoveLeft = true;
        private bool _canMoveRight = true;
        private int _objectId;
        private Vector2 _targetPosition = Vector2.zero;

        public override void EnterState()
        {
            chaseSpeed = enemyConfig.chaseSpeed;
            _eventBus.Subscribe<CanMoveLeftEvent>(OnCanMoveLeft);
            _eventBus.Subscribe<CanMoveRightEvent>(OnCanMoveRight);
        }

        public override void UpdateState()
        {
            // Если цели нет — прекращаем погоню
            if (_targetPosition == Vector2.zero)
            {
                rb.velocity = Vector2.zero;
                Enemy.ToIdle();
                return;
            }

            Vector2 currentPosition = Enemy.transform.position;
            float direction = Mathf.Sign(_targetPosition.x - currentPosition.x);

            if (direction < 0 && _canMoveLeft)
            {
                rb.velocity = new Vector2(-chaseSpeed, rb.velocity.y);
            }
            else if (direction > 0 && _canMoveRight)
            {
                rb.velocity = new Vector2(chaseSpeed, rb.velocity.y);
            }
            else
            {
                rb.velocity = Vector2.zero;
                //Enemy.ToIdle(); // В тупике — прекращаем погоню
            }
        }

        public override void ExitState()
        {
            _eventBus.Unsubscribe<CanMoveLeftEvent>(OnCanMoveLeft);
            _eventBus.Unsubscribe<CanMoveRightEvent>(OnCanMoveRight);

            rb.velocity = Vector2.zero;
            _targetPosition = Vector2.zero;
        }

        public override void OnZenjectConstruct()
        {
            _eventBus.Subscribe<OnEntitySeeTarget>(OnSeeTarget);
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

        private void OnSeeTarget(OnEntitySeeTarget evt)
        {
            if (Enemy == null) return; 
            _objectId = Enemy.gameObject.GetInstanceID();
            if (evt.ObserverId == _objectId)
            {
                if (evt.TargetPosition != Vector2.zero)
                {
                    Enemy.ToAttack();
                }
                _targetPosition = evt.TargetPosition;
            }
        }

        private void OnDestroy()
        {
            _eventBus.Unsubscribe<CanMoveLeftEvent>(OnCanMoveLeft);
            _eventBus.Unsubscribe<CanMoveRightEvent>(OnCanMoveRight);
            _eventBus.Unsubscribe<OnEntitySeeTarget>(OnSeeTarget);
        }
    }
}