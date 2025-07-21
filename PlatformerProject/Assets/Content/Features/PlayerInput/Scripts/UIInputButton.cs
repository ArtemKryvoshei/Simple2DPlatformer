using Core.EventBus;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Zenject;

namespace Content.Features.PlayerInput.Scripts
{
    public class UIInputButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField] private UIInputType _inputType;

        private IEventBus _eventBus;

        [Inject]
        public void Construct(IEventBus eventBus)
        {
            _eventBus = eventBus;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            switch (_inputType)
            {
                case UIInputType.MoveLeft:
                    _eventBus.Publish(new PlayerMoveLeftInputEvent());
                    break;
                case UIInputType.MoveRight:
                    _eventBus.Publish(new PlayerMoveRightInputEvent());
                    break;
                case UIInputType.Shoot:
                    _eventBus.Publish(new PlayerShootInputEvent());
                    break;
                case UIInputType.Jump:
                    _eventBus.Publish(new PlayerJumpInputEvent());
                    break;
                
            }
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            switch (_inputType)
            {
                case UIInputType.MoveLeft:
                    _eventBus.Publish(new PlayerMoveLeftInputReleasedEvent());
                    break;
                case UIInputType.MoveRight:
                    _eventBus.Publish(new PlayerMoveRightInputReleasedEvent());
                    break;
                case UIInputType.Shoot:
                    _eventBus.Publish(new PlayerShootInputReleasedEvent());
                    break;
            }
        }
    }
}