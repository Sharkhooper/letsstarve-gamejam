using UnityAtoms;
using UnityEngine;

public class PartyMemberComponent : MonoBehaviour {
    public GameObjectList partyList;

	[SerializeField] private int saturation;

	[SerializeField] private int maxSaturation;

	[SerializeField] private int saturationDecay;

	public int Saturation => saturation;

	public int MaxSaturation => maxSaturation;

	public float SaturationPercent => (float) Saturation / MaxSaturation;

	public int SaturationDecay => saturationDecay;

	private float timer;

    private void OnEnable() {
        partyList.Add(gameObject);
    }

    private void OnDisable() {
        partyList.Remove(gameObject);
    }

	private void Update() {
		timer += Time.deltaTime;
		if (timer > 1.0f) {
			saturation -= saturationDecay;
			timer -= 1.0f;
		}
	}
}