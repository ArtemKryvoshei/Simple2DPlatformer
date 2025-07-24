using System.Collections.Generic;
using Content.Features.ConfigsSystem.Scripts;
using Core.EventBus;
using UnityEngine;
using Zenject;

namespace Content.Features.EnemyLogic.Scripts.CommonEntitiesScripts
{
    public class EntityTargetInRadiusDetection : MonoBehaviour
    {
        [Header("Detection Settings")]
        
        [SerializeField] private Transform raycastOrigin;

        private float detectionRadius = 5f;
        private LayerMask targetLayer;
        private LayerMask obstacleLayer;
        
        private int objectId;
        private IEventBus _eventBus;

        [Inject]
        public void Construct(IEventBus eventBus, EnemyConfig config)
        {
            _eventBus = eventBus;
            objectId = gameObject.GetInstanceID();
            if (config != null)
            {
                detectionRadius = config.detectionRadius;
                targetLayer = config.targetLayer;
                obstacleLayer = config.obstacleLayer;
            }
        }

        private void FixedUpdate()
        {
            Collider2D[] targets = Physics2D.OverlapCircleAll(transform.position, detectionRadius, targetLayer);
            if (targets.Length == 0)
                return;

            Transform target = targets[0].transform;

            Vector2 direction = (target.position - raycastOrigin.position).normalized;
            float distance = Vector2.Distance(raycastOrigin.position, target.position);

            RaycastHit2D hit = Physics2D.Raycast(raycastOrigin.position, direction, distance, obstacleLayer);
            var seeEvent = new OnEntitySeeTarget();
            seeEvent.ObserverId = objectId;
            if (hit.collider == null)
            {
                seeEvent.TargetPosition = target.position;
            }
            else
            {
                seeEvent.TargetPosition = Vector2.zero;
            }
            _eventBus.Publish(seeEvent);
        }

#if UNITY_EDITOR
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, detectionRadius);

            if (raycastOrigin != null)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawLine(raycastOrigin.position, raycastOrigin.position + Vector3.right * detectionRadius);
                Gizmos.DrawLine(raycastOrigin.position, raycastOrigin.position + Vector3.left * detectionRadius);
            }
        }
#endif
    }
}