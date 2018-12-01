using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EatFoodButton : MonoBehaviour, IPointerClickHandler {

    public Inventory inventory;
    public FoodItem item;
    public TMP_Text itemCountLabel;

    private Image background;
    
    private void Start() {
        if (item == null) this.gameObject.SetActive(false);
        background = GetComponent<Image>();
    }

    void Update() {
        itemCountLabel.text = ""+inventory.FoodItems[item];
        background.color = PartyFoodUISegment.SelectedPartyFoodSegment == null ? Color.gray : Color.white;
        
    }
    
    public void OnPointerClick(PointerEventData eventData) {
        if (PartyFoodUISegment.SelectedPartyFoodSegment == null) {
            // alert: select someone to make him eat food \o/
            return;
        }
        
        
        if(inventory.RemoveItem(item, 1)){
            PartyFoodUISegment.SelectedPartyFoodSegment.partyMember.foodComponent.saturation += item.Value;
            var seq = DOTween.Sequence();
            seq.Append(transform.DOScale(0.9f, 0.1f).SetEase(Ease.InCubic));
            seq.Append(transform.DOScale(1, 0.1f).SetEase(Ease.OutCubic));
        }else {
            this.transform.DOShakePosition(0.1f, 1f);
        }

    }
}