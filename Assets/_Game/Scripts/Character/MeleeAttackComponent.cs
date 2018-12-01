using UnityAtoms;
using UnityEngine;
using UnityEngine.EventSystems;

public class MeleeAttackComponent : MonoBehaviour {
	public GameObjectList targets;
	public float range;
	public float attackSpeed;
	public int damage;

	public GameObject hitEffect;

	private float timer;

	private void Update() {
		timer += Time.deltaTime;

		if (timer >= 1.0f / attackSpeed) {
			timer -= 1.0f / attackSpeed;

			float nearestSqrDistance = float.PositiveInfinity;
			GameObject nearestTarget = null;

			for (int i = 0; i < targets.Count; ++i) {
				float sqrDistance = (targets[i].transform.position - transform.position).sqrMagnitude;
				if (sqrDistance > range * range) continue;
				if (Vector3.Dot(targets[i].transform.position - transform.position, transform.forward) < 0) continue;

				if (sqrDistance < nearestSqrDistance) {
					nearestSqrDistance = sqrDistance;
					nearestTarget = targets[i];
				}

				ExecuteEvents.Execute<IHitTarget>(targets[i], null, (x, y) => x.Damage(damage));
			}

			if (nearestTarget != null) {
				GameObject effect = Instantiate(hitEffect, transform);
				effect.transform.rotation = Quaternion.FromToRotation(transform.forward, nearestTarget.transform.position - transform.position);
			} else {
				timer = 1.0f / attackSpeed;
			}
		}
	}
}
