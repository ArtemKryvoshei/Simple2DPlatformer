using System;
using Content.Features.ConfigsSystem.Scripts;
using Content.Features.PlayerAnimator.Scripts;
using Content.Features.PlayerInput.Scripts;
using Core.EventBus;
using UnityEngine;
using Zenject;

namespace Content.Features.PlayerActions.Scripts
{
    public class PlayerJumpController : MonoBehaviour
    {
        [Header("Jump Settings")]
        [SerializeField] private LayerMask groundMask;
        [SerializeField] private Transform groundCheckPoint;
        [SerializeField] private float groundCheckDistance = 0.1f;
        
        private Rigidbody2D _rigidbody;
        private IEventBus _eventBus;
        private PlayerConfig playerConfig;
        
        private float jumpForce = 0;

        [Inject]
        public void Construct(IEventBus eventBus, PlayerConfig playerC)
        {
            _eventBus = eventBus;
            playerConfig = playerC;
            _eventBus.Subscribe<PlayerJumpInputEvent>(OnJumpPressed);
            if (playerConfig != null)
            {
                jumpForce = playerConfig.JumpForce;
            }
        }

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            IsGrounded();
        }

        private void OnJumpPressed(PlayerJumpInputEvent obj)
        {
            if (IsGrounded())
            {
                _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, jumpForce);
                _eventBus.Publish(new OnJumpedAnimatorEvent());
            }
        }

        private bool IsGrounded()
        {
            Vector2 origin = groundCheckPoint != null ? groundCheckPoint.position : transform.position;
            RaycastHit2D hit = Physics2D.Raycast(origin, Vector2.down, groundCheckDistance, groundMask);
            bool result = hit.collider != null;
            GroundedAnimatorEvent groundedEvent = new GroundedAnimatorEvent();
            groundedEvent._isGrounded = result;
            _eventBus.Publish(groundedEvent);
            return result;
        }

        private void OnDestroy()
        {
            _eventBus.Unsubscribe<PlayerJumpInputEvent>(OnJumpPressed);
        }

        private void OnDrawGizmosSelected()
        {
            if (groundCheckPoint == null) return;

            Gizmos.color = Color.green;
            Gizmos.DrawLine(groundCheckPoint.position, groundCheckPoint.position + Vector3.down * groundCheckDistance);
        }
    }
}