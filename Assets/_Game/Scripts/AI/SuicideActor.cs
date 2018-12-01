using UnityEngine;
using UnityEngine.EventSystems;

public class SuicideActor : StupidActor
{
    protected override void Death(int damage)
    {
        Destroy(gameObject);
    }

    protected override void DamageTaken(int damage)
    {
    }

    protected override void Attack(GameObject target)
    {
        ExecuteEvents.ExecuteHierarchy<IHitTarget>(target, null, (x, y) => x.Damage(damage));
        Destroy(gameObject);
    }

    protected override void Move()
    {
    }

    protected override void Idle()
    {
    }
}