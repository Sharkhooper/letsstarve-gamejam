using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    void OnTriggerEntered(Collider other)
    {
        this.gameObject.SetActive(false);
    }
}
