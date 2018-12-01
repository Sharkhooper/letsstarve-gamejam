using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class EatFoodButton : MonoBehaviour, IPointerClickHandler {

    public Inventory inventory;
    public FoodItem item;
    public TMP_Text itemCountLabel;

    private void Start() {
        if (item == null) this.gameObject.SetActive(false);
    }

    void Update() {
        itemCountLabel.text = ""+inventory.FoodItems[item];
    }
    
    public void OnPointerClick(PointerEventData eventData) {
        if(PartyFoodUISegment.SelectedPartyFoodSegment == null) return;
            // alert: select someone to make him eat food \o/

        int i = inventory.FoodItems[item];
        
        if(inventory.RemoveItem(item, 1))
            PartyFoodUISegment.SelectedPartyFoodSegment.partyMember.foodComponent.saturation += item.Value;

    }
}