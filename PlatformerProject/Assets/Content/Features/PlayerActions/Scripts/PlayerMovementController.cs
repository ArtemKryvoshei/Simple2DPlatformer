using Content.Features.ConfigsSystem.Scripts;
using Content.Features.GameState.Scripts;
using UnityEngine;
using Content.Features.PlayerInput.Scripts;
using Core.EventBus;
using Zenject;

namespace Content.Features.PlayerActions.Scripts
{
    public class PlayerMovementController : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _rigidbody;
        
        private PlayerConfig playerConfig;
        private IEventBus _eventBus;
        
        private float _moveSpeed = 0;
        private float _currentInputDirection = 0f;

        [Inject]
        public void Construct(IEventBus eventBus, PlayerConfig playerC)
        {
            _eventBus = eventBus;
            playerConfig = playerC;
            _eventBus.Subscribe<PlayerMoveLeftInputEvent>(OnMoveLeftPressed);
            _eventBus.Subscribe<PlayerMoveRightInputEvent>(OnMoveRightPressed);
            _eventBus.Subscribe<PlayerMoveLeftInputReleasedEvent>(OnMoveLeftReleased);
            _eventBus.Subscribe<PlayerMoveRightInputReleasedEvent>(OnMoveRightReleased);
            _eventBus.Subscribe<OnGameOverEvent>(StopOnGameOver);
            _eventBus.Subscribe<OnLevelCompleteEvent>(StopOnLevelComplete);
            _eventBus.Subscribe<StartNextLevelGameEvent>(OnStartNextLevelMove);
            _eventBus.Subscribe<ReloadLevelGameEvent>(OnReloadLevelMove);
            if (playerConfig != null)
            {
                _moveSpeed = playerConfig.MoveSpeed;
            }
        }

        private void OnReloadLevelMove(ReloadLevelGameEvent obj)
        {
            if (playerConfig != null)
            {
                _moveSpeed = playerConfig.MoveSpeed;
            }
        }

        private void OnStartNextLevelMove(StartNextLevelGameEvent obj)
        {
            if (playerConfig != null)
            {
                _moveSpeed = playerConfig.MoveSpeed;
            }
        }

        private void StopOnLevelComplete(OnLevelCompleteEvent obj)
        {
            ResetMovement();
        }

        private void StopOnGameOver(OnGameOverEvent obj)
        {
            ResetMovement();
        }

        private void FixedUpdate()
        {
            Vector2 velocity = _rigidbody.velocity;
            velocity.x = _currentInputDirection * _moveSpeed;
            _rigidbody.velocity = velocity;
        }

        private void ResetMovement()
        {
            _moveSpeed = 0;
            _currentInputDirection = 0;
            _rigidbody.velocity = Vector2.zero;
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
            _currentInputDirection = 0;
        }

        private void OnMoveRightReleased(PlayerMoveRightInputReleasedEvent _)
        {
            _currentInputDirection = 0;
        }

        private void OnDestroy()
        {
            _eventBus.Unsubscribe<PlayerMoveLeftInputEvent>(OnMoveLeftPressed);
            _eventBus.Unsubscribe<PlayerMoveRightInputEvent>(OnMoveRightPressed);
            _eventBus.Unsubscribe<PlayerMoveLeftInputReleasedEvent>(OnMoveLeftReleased);
            _eventBus.Unsubscribe<PlayerMoveRightInputReleasedEvent>(OnMoveRightReleased);
            _eventBus.Unsubscribe<OnGameOverEvent>(StopOnGameOver);
            _eventBus.Unsubscribe<OnLevelCompleteEvent>(StopOnLevelComplete);
            _eventBus.Unsubscribe<StartNextLevelGameEvent>(OnStartNextLevelMove);
            _eventBus.Unsubscribe<ReloadLevelGameEvent>(OnReloadLevelMove);
        }
    }
}