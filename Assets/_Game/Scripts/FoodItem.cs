using UnityEngine;

[CreateAssetMenu]
[System.Serializable]
public class FoodItem : ScriptableObject {
	[SerializeField] private int value;

	public Sprite graphic;

	public int Value => value;
}
