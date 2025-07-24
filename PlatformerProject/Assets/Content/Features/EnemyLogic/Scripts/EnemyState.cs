using Content.Features.ConfigsSystem.Scripts;
using Content.Features.GameState.Scripts;
using Core.EventBus;
using UnityEngine;
using Zenject;

namespace Content.Features.EnemyLogic.Scripts
{
    public abstract class EnemyState : MonoBehaviour
    {
        protected EnemyStateMachine StateMachine;
        protected Enemy Enemy;
        protected IEventBus _eventBus;
        protected EnemyConfig enemyConfig;
        
        [Inject]
        public void Construct(IEventBus eventBus, EnemyConfig config)
        {
            _eventBus = eventBus;
            enemyConfig = config;
            OnZenjectConstruct();
        }
        
        public virtual void Initialize(Enemy enemy, EnemyStateMachine machine)
        {
            Enemy = enemy;
            StateMachine = machine;
        }

        public abstract void EnterState();
        public abstract void UpdateState();
        public abstract void ExitState();

        public abstract void OnZenjectConstruct();
    }
}