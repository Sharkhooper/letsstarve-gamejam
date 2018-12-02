using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public class PickUpFood : MonoBehaviour{

    [Required][SerializeField] private Inventory inventory;
    [Required][SerializeField] private FoodItem itemType;
    
    void OnTriggerEnter(Collider other){
        inventory.AddFood(itemType, 1);
        Destroy(this.gameObject);
    }
}
