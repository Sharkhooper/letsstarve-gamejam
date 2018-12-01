using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityAtoms;
using UnityEngine;
using UnityEngine.UI;

public class PartyFoodUISegment : MonoBehaviour {
    private int idx = -1;
    public GameObjectList partyList;
    private CharacterActor partyMember;

    [BoxGroup("UI Elements")] [SerializeField]
    private Image characterIcon, healthBar, foodBar; 
    
    // Start is called before the first frame update
    void Start() {
        idx = this.transform.GetSiblingIndex();
        partyMember = partyList[idx].GetComponent<CharacterActor>();
        
        // todo: characterIcon ... 
    }

    // Update is called once per frame
    void Update() {
        healthBar.fillAmount = partyMember.health;
        foodBar.fillAmount = partyMember.food;
    }
}