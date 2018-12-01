using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;


public delegate void AiAction();

[RequireComponent(typeof(NavMeshAgent))]
public class StupidBehaviour : MonoBehaviour
{
    [SerializeField] private float detectionRange = 10f;
    [SerializeField] private float attackRange = 8f;
    [SerializeField] private float attackSpeed = 1f;
    [SerializeField] private int damage = 2;

    public event AiAction OnAttack;
    public event AiAction OnMove;
    public event AiAction OnWait;

    private NavMeshAgent agent;
    private float cooldown;
    
    //Debug bool
    private bool attacking;
    private bool moveing;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        cooldown = attackSpeed;
    }

    void Update()
    {
        attacking = false;
        moveing = false;
        
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
        moveing = true;
        agent.destination = target.position;
    }

    private void Attack(GameObject target)
    {
        if (cooldown <= 0)
        {
            OnAttack?.Invoke();
            ExecuteEvents.ExecuteHierarchy<IHitTarget>(target, null, (x, y) => x.Damage(damage));
            cooldown = attackSpeed;
            attacking = true;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, detectionRange);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        
        if(moveing) Gizmos.color = Color.blue;
        else if(attacking) Gizmos.color = Color.red;
        else Gizmos.color = Color.yellow;
        
        Gizmos.DrawSphere(transform.position,0.3f);
    }
}