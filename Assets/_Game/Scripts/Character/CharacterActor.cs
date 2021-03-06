using NaughtyAttributes;
using UnityAtoms;
using UnityEngine;
using UnityEngine.AI;
using _Game.Scripts.Controlls;

[RequireComponent(typeof(HealthComponent))]
[RequireComponent(typeof(FoodComponent))]
[RequireComponent(typeof(NavMeshAgent))]
public class CharacterActor : MonoBehaviour {

    public GameObjectList partyList;

    private void OnEnable() { partyList.Add(gameObject); }
    private void OnDisable() { partyList.Remove(gameObject); }
    
    [HideInInspector] public HealthComponent healthComponent;
    [HideInInspector] public FoodComponent foodComponent;
	[HideInInspector] public NavMeshAgent navMeshComponent;
	[HideInInspector] public AttackController attackController;
	
	public Animator animator;
    [SerializeField] public Inventory inventory; // fixme: has nothing to do here ... 

	public GameObject bloodParticleSystem;

    void Start() {
        healthComponent = GetComponent<HealthComponent>();
        foodComponent = GetComponent<FoodComponent>();
		navMeshComponent = GetComponent<NavMeshAgent>();
	    attackController = GetComponentInChildren<AttackController>();
	    
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

	public Vector3 Forward {
		get => Quaternion.Euler(0, -Theta, 0) * Vector3.right;
		set {
			var forward = value.normalized;
			Theta = -Vector3.SignedAngle(Vector3.right, forward, Vector3.up);		
		}
	}

	private void Update() {
		int animationState = Mathf.FloorToInt((Theta + 405.0f) / 90.0f) - 4;
		animator.SetFloat("direction", animationState);
		animator.gameObject.transform.localScale = new Vector3(animationState == 2 ? -1 : 1, 1, 1);

		var walking = (navMeshComponent.destination - transform.position).sqrMagnitude < 0.01f;
		
		animator.SetBool("walking", !walking);
		if (walking) return;
		Theta = -Vector3.SignedAngle(Vector3.right, transform.position - lastPos, Vector3.up);
		lastPos = transform.position;
	}

	[Button]
	private void OnDamage() {
		GameObject blood = Instantiate(bloodParticleSystem, transform);
		Destroy(blood, 45.0f);
	}
}