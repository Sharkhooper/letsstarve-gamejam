using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using NaughtyAttributes;
using Soraphis;
using UnityEngine;

public class PickUpGear : MonoBehaviour{
    
    // [SerializeField] private Inventory inventory;

    [SerializeField] private GearItem itemType;
    private new SpriteRenderer renderer;

    void Awake() {
        renderer = GetComponentInChildren<SpriteRenderer>();
    }
    
    [Button()] private void Start() {
        renderer.transform.DOLocalJump(Vector3.zero, 1, 3, 0.6f);

        if (itemType != null && itemType.graphic != null) {
            renderer.gameObject.SetActive(true);
            renderer.sprite = itemType.graphic;
        }
        else renderer.gameObject.SetActive(false);
    }
    
    void OnTriggerEnter(Collider other){
        // collision mask checks that we only collide with "player"s 

        var c = other.attachedRigidbody.GetComponent<CharacterActor>();

        var e = c.attackController.GetGear(itemType);
        
        if (e == null) {
            this.gameObject.SetActive(false);
        } else {
            this.itemType = e;
            Start();
        }

    }
    
}
