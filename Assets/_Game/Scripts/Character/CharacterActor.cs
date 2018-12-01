using UnityAtoms;
using UnityEngine;


[RequireComponent(typeof(HealthComponent))]
[RequireComponent(typeof(FoodComponent))]
public class CharacterActor : MonoBehaviour {

    public GameObjectList partyList;

    private void OnEnable() { partyList.Add(gameObject); }
    private void OnDisable() { partyList.Remove(gameObject); }
    
    public HealthComponent healthComponent;
    public FoodComponent foodComponent;

    void Start() {
        healthComponent = GetComponent<HealthComponent>();
        foodComponent = GetComponent<FoodComponent>();
    }
}