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
			for(int i = 0; i < targets.Count; ++i) {
				if ((targets[i].transform.position - transform.position).sqrMagnitude > range * range) continue;
				if (Vector3.Dot(targets[i].transform.position - transform.position, transform.forward) < 0) continue;
				ExecuteEvents.Execute<IHitTarget>(targets[i], null, (x, y) => x.Damage(damage));
			}

			timer -= 1.0f / attackSpeed;
		}
	}
}
