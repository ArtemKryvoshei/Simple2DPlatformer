using Content.Features.ConfigsSystem.Scripts;
using Core.EventBus;
using UnityEngine;
using Zenject;

namespace Content.Features.EnemyLogic.Scripts.CommonEntitiesScripts
{
    public class EntityDirectionScanner : MonoBehaviour
    {
        [SerializeField] private Transform leftEdgeCheck;
        [SerializeField] private Transform rightEdgeCheck;
        [SerializeField] private Transform leftWallRayOrigin;
        [SerializeField] private Transform rightWallRayOrigin;
        
        
        private float edgeCheckRadius = 0.1f;
        private float wallRayLength = 0.5f;
        private LayerMask groundLayer;
        
        private IEventBus _eventBus;
        private int _id;
        
        
        [Inject]
        public void Construct(IEventBus eventBus, EnemyConfig config)
        {
            _eventBus = eventBus;
            _id = gameObject.GetInstanceID();
            if (config != null)
            {
                edgeCheckRadius = config.edgeCheckRadius;
                wallRayLength = config.wallRayLength;
                groundLayer = config.groundLayer;
            }
        }

        private void FixedUpdate()
        {
            CheckLeft();
            CheckRight();
        }
        
        private void CheckLeft()
        {
            bool isGround = Physics2D.OverlapCircle(leftEdgeCheck.position, edgeCheckRadius, groundLayer);
            bool isWall = Physics2D.Raycast(leftWallRayOrigin.position, Vector2.left, wallRayLength, groundLayer);

            bool canMove = isGround && !isWall;

            _eventBus.Publish(new CanMoveLeftEvent
            {
                ObjectId = _id,
                CanMoveLeft = canMove
            });
        }

        private void CheckRight()
        {
            bool isGround = Physics2D.OverlapCircle(rightEdgeCheck.position, edgeCheckRadius, groundLayer);
            bool isWall = Physics2D.Raycast(rightWallRayOrigin.position, Vector2.right, wallRayLength, groundLayer);

            bool canMove = isGround && !isWall;
            
            _eventBus.Publish(new CanMoveRightEvent
            {
                ObjectId = _id,
                CanMoveRight = canMove
            });
        }
        
        private void OnDrawGizmosSelected()
        {
            if (leftEdgeCheck != null)
                Gizmos.DrawWireSphere(leftEdgeCheck.position, edgeCheckRadius);
            if (rightEdgeCheck != null)
                Gizmos.DrawWireSphere(rightEdgeCheck.position, edgeCheckRadius);

            if (leftWallRayOrigin != null)
                Gizmos.DrawLine(leftWallRayOrigin.position, leftWallRayOrigin.position + Vector3.left * wallRayLength);
            if (rightWallRayOrigin != null)
                Gizmos.DrawLine(rightWallRayOrigin.position, rightWallRayOrigin.position + Vector3.right * wallRayLength);
        }
        
    }
}