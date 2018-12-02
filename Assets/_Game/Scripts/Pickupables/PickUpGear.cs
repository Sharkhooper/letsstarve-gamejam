using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpGear : MonoBehaviour{
    
    [SerializeField] private Inventory inventory;

    [SerializeField] private GearItem itemType;
    void OnTriggerEnter(Collider other)
    {
     
    }
    
}
