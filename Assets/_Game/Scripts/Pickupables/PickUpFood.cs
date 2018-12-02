using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{

    [SerializeField] private Inventory inventory;

    [SerializeField] private FoodItem itemType;
    void OnTriggerEnter(Collider other)
    {
        Debug.Log(other);
        inventory.AddFood(itemType, 1);
        Destroy(this.gameObject);
    }
}
