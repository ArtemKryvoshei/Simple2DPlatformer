using UnityEngine;

namespace Content.Features.ConfigsSystem.Scripts
{
    [CreateAssetMenu(menuName = "Configs/EnemyConfig")]
    public class EnemyConfig : ScriptableObject
    {
        [Header("Idle")]
        public float idleDurationMax = 3f;
        public float idleDurationMin = 1f;
        [Header("Movement")]
        public float moveSpeed = 5f;
        public float chaseSpeed = 7f;
        
        [Header("System")]
        [Space(10)]
        [Header("Patroling State")]
        public float edgeCheckRadius = 0.1f;
        public float wallRayLength = 0.5f;
        public LayerMask groundLayer;
        [Space(10)]
        [Header("Chase State")]
        public float detectionRadius = 5f;
        public LayerMask targetLayer;
        public LayerMask obstacleLayer;
        
    }
}