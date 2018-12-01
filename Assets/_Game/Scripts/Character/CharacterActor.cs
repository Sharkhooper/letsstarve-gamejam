using NaughtyAttributes;
using UnityAtoms;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(HealthComponent))]
[RequireComponent(typeof(FoodComponent))]
[RequireComponent(typeof(NavMeshAgent))]
public class CharacterActor : MonoBehaviour {

    public GameObjectList partyList;

    private void OnEnable() { partyList.Add(gameObject); }
    private void OnDisable() { partyList.Remove(gameObject); }
    
    public HealthComponent healthComponent;
    public FoodComponent foodComponent;
	public NavMeshAgent navMeshComponent;
	public Animator animator;

	public GameObject bloodParticleSystem;

    void Start() {
        healthComponent = GetComponent<HealthComponent>();
        foodComponent = GetComponent<FoodComponent>();
		navMeshComponent = GetComponent<NavMeshAgent>();

		healthComponent.OnDamageTaken += _ => OnDamage();
	}

	// TODO: Rotation

	private float theta;
	public float Theta {
		get => theta;
		set {
			if (value < 0) {
				value += 360.0f;
			}

			theta = value;
		}
	}

	private Vector3 lastPos;

	public Vector3 Forward => Quaternion.Euler(0, -Theta, 0) * Vector3.right;

	private void Update() {
		int animationState = Mathf.FloorToInt((Theta + 405.0f) / 90.0f) - 4;
		animator.SetInteger("direction", animationState);
		animator.gameObject.transform.localScale = new Vector3(animationState == 2 ? -1 : 1, 1, 1);

		if ((lastPos - transform.position).sqrMagnitude < 0.01f) return;
		Theta = -Vector3.SignedAngle(Vector3.right, transform.position - lastPos, Vector3.up);
		lastPos = transform.position;
	}

	[Button]
	private void OnDamage() {
		GameObject blood = Instantiate(bloodParticleSystem, transform);
		Destroy(blood, 45.0f);
	}
}