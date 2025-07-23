using UnityEngine;

namespace Content.Features.EnemyLogic.Scripts.States
{
    public class EnemyIdleState : EnemyState
    {
        [SerializeField] private float idleDurationMax = 3f;
        [SerializeField] private float idleDurationMin = 1f;
        private float timer;

        public override void EnterState()
        {
            timer = Random.Range(idleDurationMin, idleDurationMax);
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
    }
}