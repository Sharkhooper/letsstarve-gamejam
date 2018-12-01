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

	private CharacterActor actor;

	private void Awake() {
		actor = GetComponent<CharacterActor>();
	}

	private void Update() {
		timer += Time.deltaTime;

		if (timer >= 1.0f / attackSpeed) {
			timer -= 1.0f / attackSpeed;

			float nearestSqrDistance = float.PositiveInfinity;
			GameObject nearestTarget = null;

			float nearestUnavailableSqrDistance = float.PositiveInfinity;
			GameObject nearestUnavailableTarget = null;

			for (int i = 0; i < targets.Count; ++i) {
				float sqrDistance = (targets[i].transform.position - transform.position).sqrMagnitude;
				if (sqrDistance > range * range) continue;
				if (Vector3.Dot(targets[i].transform.position - transform.position, actor.Forward) < 0) {
					if (sqrDistance < nearestUnavailableSqrDistance) {
						nearestUnavailableSqrDistance = sqrDistance;
						nearestUnavailableTarget = targets[i];
					}
					continue;
				}

				if (sqrDistance < nearestSqrDistance) {
					nearestSqrDistance = sqrDistance;
					nearestTarget = targets[i];
				}

				ExecuteEvents.Execute<IHitTarget>(targets[i], null, (x, y) => x.Damage(damage));
			}

			if (nearestTarget != null) {
				GameObject effect = Instantiate(hitEffect, transform);
				effect.transform.rotation = Quaternion.FromToRotation(Vector3.forward, nearestTarget.transform.position - transform.position);
			} else if (nearestUnavailableTarget != null) {
				actor.theta = -Vector3.SignedAngle(Vector3.right, nearestUnavailableTarget.transform.position - transform.position, Vector3.up);
				timer = 1.0f / attackSpeed;
			} else {
				timer = 1.0f / attackSpeed;
			}
		}
	}
}
