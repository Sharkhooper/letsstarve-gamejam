using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityAtoms;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PartyFoodUISegment : MonoBehaviour, IPointerClickHandler {

    public static PartyFoodUISegment SelectedPartyFoodSegment = null;
    
    public CharacterActor partyMember;

    [BoxGroup("UI Elements")] [SerializeField]
    private Image characterIcon, healthBar, foodBar;

    private Image background;
    
    // Start is called before the first frame update
    void Start() {
        background = GetComponent<Image>();

        // todo: characterIcon ... 
    }

    // Update is called once per frame
    void Update() {
        healthBar.fillAmount = partyMember.healthComponent.HealthPercentage;
        foodBar.fillAmount = partyMember.foodComponent.SaturationPercent;
        
        
    }

    public void OnPointerClick(PointerEventData eventData) {
        if(SelectedPartyFoodSegment != null) SelectedPartyFoodSegment.background.color = Color.white;
        SelectedPartyFoodSegment = this;
        SelectedPartyFoodSegment.background.color = Color.Lerp(Color.white, Color.blue, 0.1f); // hacky as fuck :D
    }
}