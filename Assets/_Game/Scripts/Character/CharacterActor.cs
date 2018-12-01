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

	public GameObject bloodParticleSystem;

    void Start() {
        healthComponent = GetComponent<HealthComponent>();
        foodComponent = GetComponent<FoodComponent>();
		navMeshComponent = GetComponent<NavMeshAgent>();

		healthComponent.OnDamageTaken += _ => OnDamage();
	}

	// TODO: Rotation

	public float theta;
	private Vector3 lastPos;

	public Vector3 Forward => Quaternion.Euler(0, -theta, 0) * Vector3.right;

	private void Update() {
		if ((lastPos - transform.position).sqrMagnitude < 0.01f) return;
		theta = -Vector3.SignedAngle(Vector3.right, transform.position - lastPos, Vector3.up);
		if (theta < 0) theta += 360.0f;
		lastPos = transform.position;
	}

	[Button]
	private void OnDamage() {
		GameObject blood = Instantiate(bloodParticleSystem, transform);
		Destroy(blood, 45.0f);
	}
}