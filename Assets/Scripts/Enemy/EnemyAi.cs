using System;
using Health;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.ProBuilder.Shapes;
using Random = UnityEngine.Random;

namespace Enemy
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class EnemyAi : MonoBehaviour
    {
        #region Veriable

        [SerializeField] private float viewingDistance = 2f;
        [SerializeField] private Vector3 containerBounds = new Vector3(0,0,0);
        [SerializeField] private float damage = 1f;
        
        private EnemyStates _states;
        private NavMeshAgent _navMeshAgent;
        private HealthBar _healthBar;
        private Vector3 _enemyPosition;
        private Vector3 _roamingPosition; 
        private Transform _player;

        #endregion
        private void Awake()
        {
            _navMeshAgent = GetComponent<NavMeshAgent>();
            _healthBar = GetComponentInChildren<HealthBar>();
            _states = EnemyStates.Wonder;
        }

        private void Start()
        {
            _enemyPosition = transform.position; 
            _roamingPosition = GetRoamingPosition();
            
//            if (Vector3.Distance(_roamingPosition, containerBounds) > 18f)
//            {
//                _roamingPosition = GetRoamingPosition();
//            }
            _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        }   
            
        private void Update()
        {
            switch (_states)
            {
                default:
                case EnemyStates.Wonder:
                    SetDestination(_roamingPosition);

                    float reachedPositionDistance = 1f;
                    if (Vector3.Distance(new Vector3(transform.position.x, 0f, transform.position.z), _roamingPosition) < reachedPositionDistance)
                    {
                        _roamingPosition = GetRoamingPosition();
                    }
                    
                    FindTarget();

                    break;
                
                case EnemyStates.Chase:

                    SetDestination(_player.position);
                    
                    float attackRange = 3f;

                    if (Vector3.Distance(transform.position, _player.position) < attackRange)
                    {
                        _navMeshAgent.isStopped = true;
                        
                        _healthBar.RemoveHealth(damage);
                        Debug.Log("Ouch!");
                    }

                    break;
                case EnemyStates.Attack:
                    break;
            }
        }

        public static Vector3 GetRandomDirection()
        {
            return new Vector3(Random.Range(-1f,1f), 0.0f, Random.Range(-1f,1f)).normalized;
        }

        private Vector3 GetRoamingPosition()
        {
            Vector3 addPos = GetRandomDirection();
            return _enemyPosition += (addPos * Random.Range(2f, 10f));
        }

        private bool SetDestination(Vector3 targetDestination)
        {
            NavMeshHit hit;
            if (NavMesh.SamplePosition(targetDestination, out hit, 1f, NavMesh.AllAreas))
            {
                _navMeshAgent.SetDestination(hit.position);
                return true;
            }
            return false;
        }

        private void FindTarget()
        {
            if (Vector3.Distance(transform.position, _player.position) < viewingDistance)
            {
                _states = EnemyStates.Chase; 
            }
        }

        /*private void OnDrawGizmos()
        {
            Gizmos.DrawSphere(containerBounds, 10f);
            Gizmos.color = Color.magenta;
        }*/
    }
    
}
