using Content.Features.BulletsPool;
using Content.Features.PlayerInput.Scripts;
using Core.EventBus;
using UnityEngine;
using Zenject;

namespace Content.Features.PlayerAnimator.Scripts
{
    public class CharacterOrientation : MonoBehaviour
    {
        [SerializeField] private Transform objectToRotate;
        [SerializeField] private float rotationRight = 0;
        [SerializeField] private float rotationLeft = 180;

        private IEventBus _eventBus;
        private bool isfacingRight;
        
        [Inject]
        public void Construct(IEventBus eventBus)
        {
            _eventBus = eventBus;
            _eventBus.Subscribe<PlayerMoveRightInputEvent>(HandleMoveRight);
            _eventBus.Subscribe<PlayerMoveLeftInputEvent>(HandleMoveLeft);
        }
        
        private void HandleMoveLeft(PlayerMoveLeftInputEvent _)
        {
            isfacingRight = false;
            transform.rotation = Quaternion.Euler(0, rotationLeft, 0);
        }

        private void HandleMoveRight(PlayerMoveRightInputEvent _)
        {
            isfacingRight = true;
            transform.rotation = Quaternion.Euler(0, rotationRight, 0);
        }
    }
}