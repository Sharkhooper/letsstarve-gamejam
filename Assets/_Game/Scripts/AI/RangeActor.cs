using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RangeActor : StupidActor
{
    protected override void Death(int damage)
    {
        Destroy(gameObject);
    }

    protected override void Attack(GameObject target)
    {
        renderer.material.color = Color.magenta;
        ExecuteEvents.ExecuteHierarchy<IHitTarget>(target, null, (x, y) => x.Damage(damage));
    }

    protected override void DamageTaken(int damage)
    {
        renderer.material.color = Color.black;
    }

    protected override void Move()
    {
        renderer.material.color = Color.blue;
    }

    protected override void Idle()
    {
        renderer.material.color = Color.yellow;
    }
}
