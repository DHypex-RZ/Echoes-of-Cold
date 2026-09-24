using IA.States;
using player = Player.PlayerManager;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using Items.Pickable;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

namespace IA
{
    public class EnemyController : MonoBehaviour
    {
        [Header("Configuración Inicial")]
        [SerializeField] UnityEvent actions;

        [Header("Modo: Pasivo")]
        [SerializeField] float detectionDistance;
        [SerializeField] float stoppingDistanceInPassive;
        [SerializeField] UnityEvent actionsInPassive;

        [Header("Modo: Agresivo")]
        [SerializeField] float timeToGoAggressive;
        [SerializeField] float distanceToRelax;
        [SerializeField] float stoppingDistanceInAggressive;
        [SerializeField] UnityEvent actionsInAggressive;

        [Header("Modo: Huida")]
        [SerializeField] float saveDistanceToPassive;
        [SerializeField] float fleeDistance;
        [SerializeField] float sampleRadius;
        [SerializeField] int maxChecks;
        [SerializeField] int angleStep;
        [SerializeField] UnityEvent actionsInFlee;

        [Header("Modo: Final")]
        [SerializeField] UnityEvent actionsfinal;


        NavMeshAgent _agent;
        IEnemyState _state;
        PassiveState _passive;
        AggressiveState _aggressive;
        FleeState _flee;

        float _timer;
        bool _pacified;
        BoxCollider _collider;

        void Awake()
        {
            _passive = new(actionsInPassive, stoppingDistanceInPassive);
            _aggressive = new(actionsInAggressive, stoppingDistanceInAggressive);
            _flee = new(actionsInFlee, fleeDistance, sampleRadius, maxChecks, angleStep);
        }

        void Start()
        {
            _collider = GetComponent<BoxCollider>();
            _agent = GetComponent<NavMeshAgent>();
            actions?.Invoke();
            _state = _passive;
        }

        void Update()
        {
            _state = HandleState();
            _state.HandleBehavior(_agent, this);
            if (CollectionController.AllIsCollected && _pacified == false)
            {
                enabled = false;
                _pacified = true;
            }
        }

        IEnemyState HandleState()
        {
            float distance = Vector3.Distance(transform.position, player.GetPosition());

            IEnemyState state = _state;

            switch (_state)
            {
                case PassiveState:
                    state = distance < detectionDistance && HandleVision() ? _flee : _aggressive;
                    break;

                case FleeState:
                    if (distance > saveDistanceToPassive) state = _passive;
                    break;

                case AggressiveState:
                    if (distance > distanceToRelax || _pacified)
                    {
                        _agent.ResetPath();
                        state = _passive;
                    }
                    break;
            }

            return state;
        }

        bool HandleVision()
        {
            Debug.DrawRay(transform.position, (player.GetPosition() - transform.position).normalized * detectionDistance, Color.red);

            Vector3 direction = (player.GetPosition() - transform.position).normalized;
            float distance = Vector3.Distance(transform.position, player.GetPosition());

            if (!Physics.Raycast(transform.position, direction, out RaycastHit hit, distance)) return false;

            return hit.transform == player.GetTransform();
        }

        void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player")) return;

            if (_pacified) { SceneManager.LoadScene(4); }
        }
    }
}

