using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SuicideActor : MonoBehaviour
{
    [SerializeField] private int damage = 50;

    private StupidBehaviour behaviour;
    private MeshRenderer renderer;
    private HealthComponent health;

    private void Awake()
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

    private void Death(int damage)
    {
        Destroy(gameObject);
    }

    private void Attack(GameObject target)
    {
        renderer.material.color = Color.red;
        ExecuteEvents.ExecuteHierarchy<IHitTarget>(target, null, (x, y) => x.Damage(damage));
        Destroy(gameObject);
    }

    private void DamageTaken(int damage)
    {
        renderer.material.color = Color.black;
    }

    private void Move()
    {
        renderer.material.color = Color.blue;
    }

    private void Idle()
    {
        renderer.material.color = Color.yellow;
    }
}