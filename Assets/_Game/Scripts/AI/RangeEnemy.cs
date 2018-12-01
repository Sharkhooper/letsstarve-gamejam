using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(NavMeshAgent))]
public class RangeEnemy : MonoBehaviour
{
    [SerializeField] private float detectionRange = 10f;
    [SerializeField] private float attackRange = 8f;
    [SerializeField] private float attackSpeed = 1f;
    [SerializeField] private int damage = 2;

    public MeshRenderer re;

    private NavMeshAgent agent;
    private float cooldown;
    private const int playerLayer = 0;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        cooldown = attackSpeed;
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
                Attack(detected.gameObject);
            }
            else
            {
                MoveTo(detected.transform);
            }
        }

        cooldown -= Time.deltaTime;
    }

    private void MoveTo(Transform target)
    {
        agent.destination = target.position;
    }

    private void Attack(GameObject target)
    {
        if (cooldown <= 0)
        {
            re.material.color = Color.blue;
            ExecuteEvents.ExecuteHierarchy<IHitTarget>(target, null, (x, y) => x.Damage(damage));
            cooldown = attackSpeed;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, detectionRange);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}