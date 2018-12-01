using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        Debug.Log(other);
        this.gameObject.SetActive(false);
    }
}
