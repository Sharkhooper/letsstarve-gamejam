using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent),typeof(StupidBehaviour),typeof(HealthComponent))]
public abstract class StupidActor : MonoBehaviour
{
    [SerializeField] protected int damage = 50;

    protected StupidBehaviour behaviour;
    protected MeshRenderer renderer;
    protected HealthComponent health;

    protected void Awake()
    {
        behaviour = GetComponent<StupidBehaviour>();
        renderer = transform.GetComponentInChildren<MeshRenderer>();
        health = transform.GetComponent<HealthComponent>();

        health.OnDeath += Death;
        health.OnDamageTaken += DamageTaken;

        behaviour.OnAttack += Attack;
        behaviour.OnMove += Move;
        behaviour.OnWait += Idle;
    }

    protected abstract void Death(int damage);
    
    protected abstract void Attack(GameObject target);

    protected abstract void DamageTaken(int damage);

    protected abstract void Move();

    protected abstract void Idle();
}