using UnityEngine;

public class FoodComponent : MonoBehaviour{
    
    [SerializeField] public int saturation;

    [SerializeField] private int maxSaturation;

    [SerializeField] private int saturationDecay;

    public int MaxSaturation => maxSaturation;

    public float SaturationPercent => (float) saturation / MaxSaturation;

    public int SaturationDecay => saturationDecay;

    private float timer;

    private void Update() {
        timer += Time.deltaTime;
        if (timer > 1.0f) {
            saturation -= saturationDecay;
            timer -= 1.0f;
        }
    }
    
}