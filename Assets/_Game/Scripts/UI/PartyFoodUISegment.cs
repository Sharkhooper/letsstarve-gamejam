using DG.Tweening;
using NaughtyAttributes;
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
        
        InvokeRepeating(nameof(pulseUI), 0, 1f);
    }

    private bool valueChanged(float a, float b, float tolerance = 0.03f) {
        return Mathf.Abs(a - b) > tolerance; 
    }

    private float lastSaturation, lastHealth;
    
    private void pulseUI() {
       // if(! this.gameObject.activeSelf) return; // maybe redundant...

        if (valueChanged(foodBar.fillAmount, lastSaturation)){
            
            foodBar.transform.parent.
                DOScale(0.9f, 0.05f)
                .SetEase(Ease.Linear)
                .From();
        
        lastSaturation = foodBar.fillAmount;
        }
        
        if (valueChanged(healthBar.fillAmount, lastHealth, 0.01f)){
            
            healthBar.transform.parent.
                DOScale(0.9f, 0.05f)
                .SetEase(Ease.Linear)
                .From();
        
            lastHealth = healthBar.fillAmount;
        }

    }
    
    // Update is called once per frame
    void Update() {
        healthBar.fillAmount = partyMember.healthComponent.HealthPercentage;
        foodBar.fillAmount = partyMember.foodComponent.SaturationPercent;
    }

    public void OnPointerClick(PointerEventData eventData) {
        if(SelectedPartyFoodSegment != null) SelectedPartyFoodSegment.background.color = Color.white;
        SelectedPartyFoodSegment = this;

        var seq = DOTween.Sequence();
        seq.Append(SelectedPartyFoodSegment.transform.DOScale(0.9f, 0.1f).SetEase(Ease.InCubic));
        seq.Append(SelectedPartyFoodSegment.transform.DOScale(1, 0.1f).SetEase(Ease.OutCubic));
        SelectedPartyFoodSegment.background.color = Color.Lerp(Color.white, Color.blue, 0.1f); // hacky as fuck :D
    }
}