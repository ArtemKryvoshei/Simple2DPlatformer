using UnityEngine;

namespace Content.Features.SimpleCameraController.Scripts
{
    public class FollowPlayerWithDeadZone : MonoBehaviour
    {
        [SerializeField] private Transform target; 
        [SerializeField] private Vector2 deadZone = new Vector2(0f, 1.4f); 
        [SerializeField] private float smoothSpeed = 5f; 

        private void LateUpdate()
        {
            if (target == null)
                return;

            Vector3 currentPosition = transform.position;
            Vector3 targetPosition = target.position;
            
            float deltaY = targetPosition.y - currentPosition.y;

            if (Mathf.Abs(deltaY) > deadZone.y)
            {
                float direction = Mathf.Sign(deltaY);
                float desiredY = targetPosition.y - (deadZone.y * direction);
                currentPosition.y = Mathf.Lerp(currentPosition.y, desiredY, Time.deltaTime * smoothSpeed);
            }
            
            currentPosition.x = Mathf.Lerp(currentPosition.x, targetPosition.x, Time.deltaTime * smoothSpeed);

            transform.position = new Vector3(currentPosition.x, currentPosition.y, transform.position.z);
        }
    }
}