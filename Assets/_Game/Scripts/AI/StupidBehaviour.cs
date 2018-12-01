using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;


public delegate void AiAction();
public delegate void AttackAction(GameObject target);

[RequireComponent(typeof(NavMeshAgent))]
public class StupidBehaviour : MonoBehaviour
{
    [SerializeField] private float detectionRange = 10f;
    [SerializeField] private float attackRange = 8f;
    [SerializeField] private float attackSpeed = 1f;

    public event AttackAction OnAttack;
    public event AiAction OnMove;
    public event AiAction OnWait;

    private NavMeshAgent agent;
    private float cooldown;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        cooldown = attackSpeed;
    }

    void Update()
    {
        
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
                OnMove?.Invoke();
                MoveTo(detected.transform);
            }
        }
        else
        {
            OnWait?.Invoke();
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
            OnAttack?.Invoke(target);
            cooldown = attackSpeed;
        }
        else
        {
            OnWait?.Invoke();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, detectionRange);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}