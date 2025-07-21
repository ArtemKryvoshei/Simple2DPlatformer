using UnityEngine;
using Content.Features.PlayerInput.Scripts;
using Core.EventBus;
using Zenject;

namespace Content.Features.PlayerActions.Scripts
{
    public class PlayerMovementController : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _rigidbody;
        [SerializeField] private float _moveSpeed = 5f;

        private IEventBus _eventBus;
        private float _currentInputDirection = 0f;

        [Inject]
        public void Construct(IEventBus eventBus)
        {
            _eventBus = eventBus;

            _eventBus.Subscribe<PlayerMoveLeftInputEvent>(OnMoveLeftPressed);
            _eventBus.Subscribe<PlayerMoveRightInputEvent>(OnMoveRightPressed);
            _eventBus.Subscribe<PlayerMoveLeftInputReleasedEvent>(OnMoveLeftReleased);
            _eventBus.Subscribe<PlayerMoveRightInputReleasedEvent>(OnMoveRightReleased);
        }

        private void FixedUpdate()
        {
            Vector2 velocity = _rigidbody.velocity;
            velocity.x = _currentInputDirection * _moveSpeed;
            _rigidbody.velocity = velocity;
        }

        private void OnMoveLeftPressed(PlayerMoveLeftInputEvent _)
        {
            _currentInputDirection = -1f;
        }

        private void OnMoveRightPressed(PlayerMoveRightInputEvent _)
        {
            _currentInputDirection = 1f;
        }

        private void OnMoveLeftReleased(PlayerMoveLeftInputReleasedEvent _)
        {
            // Если отпущено влево, но зажато вправо — продолжаем двигать вправо
            _currentInputDirection = Input.GetKey(KeyCode.D) ? 1f : 0f;
        }

        private void OnMoveRightReleased(PlayerMoveRightInputReleasedEvent _)
        {
            // Если отпущено вправо, но зажато влево — продолжаем двигать влево
            _currentInputDirection = Input.GetKey(KeyCode.A) ? -1f : 0f;
        }

        private void OnDestroy()
        {
            _eventBus.Unsubscribe<PlayerMoveLeftInputEvent>(OnMoveLeftPressed);
            _eventBus.Unsubscribe<PlayerMoveRightInputEvent>(OnMoveRightPressed);
            _eventBus.Unsubscribe<PlayerMoveLeftInputReleasedEvent>(OnMoveLeftReleased);
            _eventBus.Unsubscribe<PlayerMoveRightInputReleasedEvent>(OnMoveRightReleased);
        }
    }
}