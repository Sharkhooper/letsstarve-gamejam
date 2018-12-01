using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class RangeEnemy : MonoBehaviour
{
    [SerializeField] private float detectionRange = 10f;
    [SerializeField] private float attackRange = 8f;

    public MeshRenderer re;

    private NavMeshAgent agent;
    private const int playerLayer = 0;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        re.material.color = Color.red;
        Collider detected = Physics.OverlapSphere(transform.position, detectionRange, GameLayer.PlayerMask)
            .OrderBy(x => Vector3.Distance(x.transform.position, transform.position)).FirstOrDefault();

        if (detected != null)
        {
            if (Vector3.Distance(transform.position, detected.transform.position) <= attackRange)
            {
                Attack(detected.transform);
            }
            else
            {
                MoveTo(detected.transform);
            }
        }
    }

    private void MoveTo(Transform target)
    {
        agent.destination = target.position;
    }

    private void Attack(Transform target)
    {
        re.material.color = Color.blue;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, detectionRange);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}