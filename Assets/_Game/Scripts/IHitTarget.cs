using UnityEngine.EventSystems;

public interface IHitTarget : IEventSystemHandler
{
    void Damage(int damage);
}