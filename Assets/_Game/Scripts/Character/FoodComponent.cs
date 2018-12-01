using UnityEngine;

public class FoodComponent : MonoBehaviour{
    
    [SerializeField] private int saturation;

    [SerializeField] private int maxSaturation;

    [SerializeField] private int saturationDecay;

    public int Saturation => saturation;

    public int MaxSaturation => maxSaturation;

    public float SaturationPercent => (float) Saturation / MaxSaturation;

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