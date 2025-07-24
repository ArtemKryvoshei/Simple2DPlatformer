using UnityEngine;

namespace Content.Features.EnemyLogic.Scripts.States
{
    public class EnemyIdleState : EnemyState
    {
        private float timer;
        
        public override void EnterState()
        {
            timer = Random.Range(enemyConfig.idleDurationMin, enemyConfig.idleDurationMax);
        }

        public override void UpdateState()
        {
            timer -= Time.deltaTime;
            if (timer <= 0f)
            {
                Enemy.ToPatrol();
            }
        }

        public override void ExitState() { }
        public override void OnZenjectConstruct() { }
    }
}