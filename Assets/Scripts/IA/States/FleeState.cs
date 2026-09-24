using Player;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

namespace IA.States
{
    public class FleeState: IEnemyState
    {
        readonly UnityEvent _actions;
        readonly float _fleeDistance;
        readonly float _sampleRadius;
        readonly int _maxChecks;
        readonly int _angleStep;

        public FleeState(UnityEvent actions, float fleeDistance, float sampleRadius, int maxChecks, int angleStep)
        {
            _actions = actions;
            _fleeDistance = fleeDistance;
            _sampleRadius = sampleRadius;
            _maxChecks = maxChecks;
            _angleStep = angleStep;
        }

        public void HandleBehavior(NavMeshAgent agent, EnemyController brain)
        {
            _actions.Invoke();
            
            Vector3 direction = (brain.transform.position - PlayerManager.GetPosition()).normalized;

            if (TryFindPoint(brain.transform.position, direction, out Vector3 result)) 
                agent.SetDestination(result);
        }
        
        bool TryFindPoint(Vector3 origin, Vector3 direction, out Vector3 result)
        {
            if (CheckDirection(origin, direction, out result)) return true;
            
            for (int i = 1; i <= _maxChecks; i++)
            {
                float ang = _angleStep * i;

                if (CheckDirection(origin, Quaternion.Euler(0, ang, 0) * direction, out result))
                    return true;

                if (CheckDirection(origin, Quaternion.Euler(0, -ang, 0) * direction, out result))
                    return true;
            }
            
            result = Vector3.zero;
            return false;
        }
        
        bool CheckDirection(Vector3 origin, Vector3 direction, out Vector3 result)
        {
            Vector3 target = origin + direction * _fleeDistance;

            if (NavMesh.SamplePosition(target, out NavMeshHit hit, _sampleRadius, NavMesh.AllAreas))
            {
                result = hit.position;
                return true;
            }
            
            result = Vector3.zero;
            return false;
        }
    }
}