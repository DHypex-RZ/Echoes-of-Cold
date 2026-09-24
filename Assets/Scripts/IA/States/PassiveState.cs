using UnityEngine.AI;
using UnityEngine.Events;

namespace IA.States
{
    public class PassiveState: IEnemyState
    {
        readonly UnityEvent _actions;
        readonly float _stoppingDistance;

        public PassiveState(UnityEvent actions, float stoppingDistance)
        {
            _actions = actions;
            _stoppingDistance = stoppingDistance;
        }

        public void HandleBehavior(NavMeshAgent agent, EnemyController brain)
        {
            agent.stoppingDistance = _stoppingDistance;
            _actions.Invoke();
        }
    }
}