using UnityEngine;
using UnityEngine.EventSystems;

public class MeleeActor : StupidActor
{
    protected override void Death(int damage)
    {
        Destroy(gameObject);
    }

    protected override void Attack(GameObject target)
    {
        anim.SetTrigger("attack");
        ExecuteEvents.ExecuteHierarchy<IHitTarget>(target, null, (x, y) => x.Damage(damage));
    }

    protected override void DamageTaken(int damage)
    {
    }

    protected override void Move()
    {
        anim.SetBool("walking",true);
    }

    protected override void Idle()
    {
        Debug.Log("Idel");
        anim.SetBool("walking",false);
    }
}