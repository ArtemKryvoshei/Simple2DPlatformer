using Content.Features.EnemyLogic.Scripts.States;
using UnityEngine;

namespace Content.Features.EnemyLogic.Scripts
{
    public class Enemy : MonoBehaviour
    {
        [Header("States")]
        [SerializeField] private EnemyState idleState;
        [SerializeField] private EnemyState patrolState;
        [SerializeField] private EnemyState attackState;

        private EnemyStateMachine _stateMachine;

        private void Awake()
        {
            _stateMachine = GetComponent<EnemyStateMachine>();

            idleState.Initialize(this, _stateMachine);
            patrolState.Initialize(this, _stateMachine);
            attackState.Initialize(this, _stateMachine);
        }

        private void Start()
        {
            _stateMachine.SetState(idleState);
        }

        public void ToIdle() => _stateMachine.SetState(idleState);
        public void ToPatrol() => _stateMachine.SetState(patrolState);
        public void ToAttack() => _stateMachine.SetState(attackState);

        // Публичные данные врага: скорость, таймеры и т.п.
    }

}