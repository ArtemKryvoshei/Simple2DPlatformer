using UnityEngine;

namespace Content.Features.EnemyLogic.Scripts.CommonEntitiesScripts
{
    public struct CanMoveLeftEvent
    {
        public int ObjectId;
        public bool CanMoveLeft;
    }

    public struct CanMoveRightEvent
    {
        public int ObjectId;
        public bool CanMoveRight;
    }
    
    public struct OnEntitySeeTarget
    {
        public int ObserverId;
        public Vector2 TargetPosition; 
    }
}