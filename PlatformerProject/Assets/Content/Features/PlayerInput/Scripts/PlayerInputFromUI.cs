using Core.EventBus;
using UnityEngine;
using Zenject;

namespace Content.Features.PlayerInput.Scripts
{
    public class PlayerInputFromUI : MonoBehaviour
    {
        private IEventBus _eventBus;

        public bool MoveLeft { get; private set; }
        public bool MoveRight { get; private set; }
        public bool Shoot { get; private set; }

        private bool _jumpRequested;
        public bool Jump => ConsumeJump();

        [Inject]
        public void Construct(IEventBus eventBus)
        {
            _eventBus = eventBus;

            _eventBus.Subscribe<PlayerMoveLeftInputEvent>(OnMoveLeft);
            _eventBus.Subscribe<PlayerMoveLeftInputReleasedEvent>(OnMoveLeftReleased);
            
            _eventBus.Subscribe<PlayerMoveRightInputEvent>(OnMoveRight);
            _eventBus.Subscribe<PlayerMoveRightInputReleasedEvent>(OnMoveRightReleased);
            
            _eventBus.Subscribe<PlayerShootInputEvent>(OnShoot);
            _eventBus.Subscribe<PlayerShootInputReleasedEvent>(OnShootReleased);

            _eventBus.Subscribe<PlayerJumpInputEvent>(OnJump);
        }

        private void OnMoveLeft(PlayerMoveLeftInputEvent _) => MoveLeft = true;
        private void OnMoveLeftReleased(PlayerMoveLeftInputReleasedEvent _) => MoveLeft = false;

        private void OnMoveRight(PlayerMoveRightInputEvent _) => MoveRight = true;
        private void OnMoveRightReleased(PlayerMoveRightInputReleasedEvent _) => MoveRight = false;

        private void OnShoot(PlayerShootInputEvent _) => Shoot = true;
        private void OnShootReleased(PlayerShootInputReleasedEvent _) => Shoot = false;

        private void OnJump(PlayerJumpInputEvent _) => _jumpRequested = true;

        private bool ConsumeJump()
        {
            if (_jumpRequested)
            {
                _jumpRequested = false;
                return true;
            }
            return false;
        }

        private void OnDestroy()
        {
            _eventBus.Unsubscribe<PlayerMoveLeftInputEvent>(OnMoveLeft);
            _eventBus.Unsubscribe<PlayerMoveLeftInputReleasedEvent>(OnMoveLeftReleased);
            
            _eventBus.Unsubscribe<PlayerMoveRightInputEvent>(OnMoveRight);
            _eventBus.Unsubscribe<PlayerMoveRightInputReleasedEvent>(OnMoveRightReleased);

            _eventBus.Unsubscribe<PlayerShootInputEvent>(OnShoot);
            _eventBus.Unsubscribe<PlayerShootInputReleasedEvent>(OnShootReleased);

            _eventBus.Unsubscribe<PlayerJumpInputEvent>(OnJump);
        }
    }
}