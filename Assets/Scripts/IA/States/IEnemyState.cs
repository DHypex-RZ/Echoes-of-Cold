using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

namespace IA.States
{
    public interface IEnemyState
    {
        public void HandleBehavior(NavMeshAgent agent, EnemyController brain);
    }
}