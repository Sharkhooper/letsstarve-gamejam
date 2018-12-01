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

    void Start() {
        healthComponent = GetComponent<HealthComponent>();
        foodComponent = GetComponent<FoodComponent>();
		navMeshComponent = GetComponent<NavMeshAgent>();
	}
}