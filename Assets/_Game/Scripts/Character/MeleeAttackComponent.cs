using UnityAtoms;
using UnityEngine;
using UnityEngine.EventSystems;

public class MeleeAttackComponent : MonoBehaviour {
	public GameObjectList targets;
	public float range;
	public float attackSpeed;
	public int damage;

	private float timer;

	private void Update() {
		timer += Time.deltaTime;

		if (timer >= 1.0f / attackSpeed) {
			foreach (GameObject o in targets.List) {
				if ((o.transform.position - transform.position).sqrMagnitude > range * range) continue;
				if (Vector3.Dot(o.transform.position - transform.position, transform.forward) < 0) continue;
				ExecuteEvents.Execute<IHitTarget>(o, null, (x, y) => x.Damage(damage));
			}

			timer -= 1.0f / attackSpeed;
		}
	}
}
