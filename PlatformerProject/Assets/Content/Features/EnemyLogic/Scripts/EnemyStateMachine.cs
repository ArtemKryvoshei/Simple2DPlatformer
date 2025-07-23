using UnityEngine;

namespace Content.Features.EnemyLogic.Scripts
{
    public class EnemyStateMachine : MonoBehaviour
    {
        private EnemyState _currentState;

        public void SetState(EnemyState newState)
        {
            if (_currentState == newState)
                return;

            _currentState?.ExitState();
            _currentState = newState;
            _currentState?.EnterState();
        }

        private void Update()
        {
            _currentState?.UpdateState();
        }
    }

}