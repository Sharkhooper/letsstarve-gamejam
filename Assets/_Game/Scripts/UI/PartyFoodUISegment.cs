using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityAtoms;
using UnityEngine;
using UnityEngine.UI;

public class PartyFoodUISegment : MonoBehaviour {
    public CharacterActor partyMember;

    [BoxGroup("UI Elements")] [SerializeField]
    private Image characterIcon, healthBar, foodBar; 
    
    // Start is called before the first frame update
    void Start() {
        // todo: characterIcon ... 
    }

    // Update is called once per frame
    void Update() {
        healthBar.fillAmount = partyMember.healthComponent.HealthPercentage;
        foodBar.fillAmount = partyMember.foodComponent.SaturationPercent;
    }
}