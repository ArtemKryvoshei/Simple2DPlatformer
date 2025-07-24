using UnityEngine;

namespace Content.Features.SimpleCameraController.Scripts
{
    public class ParallaxBackground : MonoBehaviour
    {
        [Tooltip("Скорость параллакса по X и Y. Меньше 1 — медленнее, больше 1 — быстрее (обычно < 1).")]
        [SerializeField] private Vector2 parallaxMultiplier = new Vector2(0.5f, 0.5f);
        [SerializeField] private Transform cameraTransform;
        
        private Vector3 lastCameraPosition;

        private void Start()
        {
            if (cameraTransform == null)
            {
                return;
            }
            lastCameraPosition = cameraTransform.position;
        }

        private void LateUpdate()
        {
            if (cameraTransform == null)
            {
                return;
            }

            Vector3 deltaMovement = cameraTransform.position - lastCameraPosition;
            transform.position += new Vector3(
                deltaMovement.x * parallaxMultiplier.x,
                deltaMovement.y * parallaxMultiplier.y,
                0f
            );

            lastCameraPosition = cameraTransform.position;
        }
    }
}