using System;
using Content.Features.GameState.Scripts;
using Content.Features.PlayerInput.Scripts;
using Core.EventBus;
using UnityEngine;
using Zenject;

namespace Content.Features.PlayerAnimator.Scripts
{
    public class PlayerAnimator : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private string groundedBoolName = "isJumping";
        [SerializeField] private string takeoffTriggerName = "takeOff";
        [SerializeField] private string runningBoolName = "isRunning";
        
        IEventBus _eventBus;

        [Inject]
        public void Construct(IEventBus eventBus)
        {
            _eventBus = eventBus;
            _eventBus.Subscribe<OnJumpedAnimatorEvent>(OnJumpAnimation);
            _eventBus.Subscribe<GroundedAnimatorEvent>(SetGroundedAnim);
            _eventBus.Subscribe<PlayerMoveLeftInputEvent>(OnRunningLeft);
            _eventBus.Subscribe<PlayerMoveRightInputEvent>(OnRunningRight);
            _eventBus.Subscribe<PlayerMoveLeftInputReleasedEvent>(OnStopRunningLeft);
            _eventBus.Subscribe<PlayerMoveRightInputReleasedEvent>(OnStopRunningRight);
            
            _eventBus.Subscribe<OnLevelCompleteEvent>(@event => StopPlayRunning());
            _eventBus.Subscribe<ReloadLevelGameEvent>(@event => StopPlayRunning());
            _eventBus.Subscribe<OnGameOverEvent>(@event => StopPlayRunning());
        }

        private void OnStopRunningRight(PlayerMoveRightInputReleasedEvent obj)
        {
            StopPlayRunning();
        }

        private void OnStopRunningLeft(PlayerMoveLeftInputReleasedEvent obj)
        {
            StopPlayRunning();
        }

        private void OnRunningRight(PlayerMoveRightInputEvent obj)
        {
            PlayRunning();
        }

        private void OnRunningLeft(PlayerMoveLeftInputEvent obj)
        {
            PlayRunning();
        }

        private void PlayRunning()
        {
            _animator.SetBool(runningBoolName, true);
        }

        private void StopPlayRunning()
        {
            _animator.SetBool(runningBoolName, false);
        }

        private void SetGroundedAnim(GroundedAnimatorEvent obj)
        {
            _animator.SetBool(groundedBoolName, !obj._isGrounded);
        }

        private void OnJumpAnimation(OnJumpedAnimatorEvent obj)
        {
            _animator.SetTrigger(takeoffTriggerName);
        }

        private void OnDestroy()
        {
            _eventBus.Unsubscribe<OnJumpedAnimatorEvent>(OnJumpAnimation);
            _eventBus.Unsubscribe<GroundedAnimatorEvent>(SetGroundedAnim);
            _eventBus.Unsubscribe<PlayerMoveLeftInputEvent>(OnRunningLeft);
            _eventBus.Unsubscribe<PlayerMoveRightInputEvent>(OnRunningRight);
            _eventBus.Unsubscribe<PlayerMoveLeftInputReleasedEvent>(OnStopRunningLeft);
            _eventBus.Unsubscribe<PlayerMoveRightInputReleasedEvent>(OnStopRunningRight);
        }
    }
}