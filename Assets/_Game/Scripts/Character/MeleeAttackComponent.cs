using UnityAtoms;
using UnityEngine;
using UnityEngine.EventSystems;

public class MeleeAttackComponent : MonoBehaviour {
	public GameObjectList targets;
	
	public float attackSpeed;

	private int damage;
    private float range;

    public float rangedStat = 0.5f;

	public GameObject hitEffect;

	private float timer;

	private CharacterActor actor;

    private float nearestSqrDistance;
    private float nearestUnavailableSqrDistance;

    private GameObject nearestTarget;
    private GameObject nearestUnavailableTarget;

	private void Awake() {
		actor = GetComponent<CharacterActor>();
	}

	private void Update() {
		timer += Time.deltaTime;

		if (timer >= 1.0f / attackSpeed) {
			timer -= 1.0f / attackSpeed;

			nearestSqrDistance = float.PositiveInfinity;
			nearestTarget = null;

			nearestUnavailableSqrDistance = float.PositiveInfinity;
			nearestUnavailableTarget = null;

		    if (actor.inventory.EquippedItems[actor].IsRangedValue)
		    {
                attackRanged();
		    }
		    else
		    {
		        attackMelee();
		    }

			if (nearestTarget != null) {
				GameObject effect = Instantiate(hitEffect, transform);
				effect.transform.rotation = Quaternion.FromToRotation(Vector3.forward, nearestTarget.transform.position - transform.position);
			    actor.Theta = -Vector3.SignedAngle(Vector3.right, nearestUnavailableTarget.transform.position - transform.position, Vector3.up);
            } else if (nearestUnavailableTarget != null) {
				actor.Theta = -Vector3.SignedAngle(Vector3.right, nearestUnavailableTarget.transform.position - transform.position, Vector3.up);
				timer = 1.0f / attackSpeed;
			} else {
				timer = 1.0f / attackSpeed;
			}
		}
	}

    void attackMelee()
    {
        for (int i = 0; i < targets.Count; ++i)
        {
            float sqrDistance = (targets[i].transform.position - transform.position).sqrMagnitude;
            if (sqrDistance > range * range) continue;
            if (Vector3.Dot(targets[i].transform.position - transform.position, actor.Forward) < 0)
            {
                if (sqrDistance < nearestUnavailableSqrDistance)
                {
                    nearestUnavailableSqrDistance = sqrDistance;
                    nearestUnavailableTarget = targets[i];
                }
                continue;
            }

            if (sqrDistance < nearestSqrDistance)
            {
                nearestSqrDistance = sqrDistance;
                nearestTarget = targets[i];
            }

            damage = (int)(actor.inventory.EquippedItems[actor].DamageValue * (1 + rangedStat));
            ExecuteEvents.Execute<IHitTarget>(targets[i], null, (x, y) => x.Damage(damage));
        }
    }
    void attackRanged()
    {
        for (int i = 0; i < targets.Count; ++i)
        {
            float sqrDistance = (targets[i].transform.position - transform.position).sqrMagnitude;
            if (sqrDistance > range * range) continue;
            if (Vector3.Dot(targets[i].transform.position - transform.position, actor.Forward) < 0)
            {
                if (sqrDistance < nearestUnavailableSqrDistance)
                {
                    nearestUnavailableSqrDistance = sqrDistance;
                    nearestUnavailableTarget = targets[i];
                }
                continue;
            }

            if (sqrDistance < nearestSqrDistance)
            {
                nearestSqrDistance = sqrDistance;
                nearestTarget = targets[i];
            }

            if (nearestTarget != null)
            {
                damage = (int)(actor.inventory.EquippedItems[actor].DamageValue * (1 + rangedStat));
                ExecuteEvents.Execute<IHitTarget>(targets[i], null, (x, y) => x.Damage(damage));
            }
        }
    }
}
