using Content.Features.PlayerInput.Scripts;
using Core.EventBus;
using UnityEngine;
using Zenject;

namespace Content.Features.PlayerActions.Scripts
{
    public class PlayerJumpController : MonoBehaviour
    {
        [Header("Jump Settings")]
        [SerializeField] private float jumpForce = 5f;
        [SerializeField] private LayerMask groundMask;
        [SerializeField] private Transform groundCheckPoint;
        [SerializeField] private float groundCheckDistance = 0.1f;

        private Rigidbody2D _rigidbody;
        private IEventBus _eventBus;

        [Inject]
        public void Construct(IEventBus eventBus)
        {
            _eventBus = eventBus;
            _eventBus.Subscribe<PlayerJumpInputEvent>(OnJumpPressed);
        }

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        private void OnJumpPressed(PlayerJumpInputEvent obj)
        {
            if (IsGrounded())
            {
                _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, jumpForce);
            }
        }

        private bool IsGrounded()
        {
            Vector2 origin = groundCheckPoint != null ? groundCheckPoint.position : transform.position;
            RaycastHit2D hit = Physics2D.Raycast(origin, Vector2.down, groundCheckDistance, groundMask);
            return hit.collider != null;
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