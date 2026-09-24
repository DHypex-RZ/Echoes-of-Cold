using UnityEngine.AI;
using UnityEngine.Events;
using player = Player.PlayerManager;

namespace IA.States
{
    public class AggressiveState: IEnemyState
    {
        readonly UnityEvent _actions;
        readonly float _stoppingDistance;

        public AggressiveState(UnityEvent actions, float stoppingDistance)
        {
            _actions = actions;
            _stoppingDistance = stoppingDistance;
        }

        public void HandleBehavior(NavMeshAgent agent, EnemyController brain)
        {
            agent.stoppingDistance = _stoppingDistance;
            _actions.Invoke();
            agent.SetDestination(player.GetPosition());
        }
    }
}