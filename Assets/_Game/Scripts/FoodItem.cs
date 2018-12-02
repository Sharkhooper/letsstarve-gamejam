using UnityEngine;

[CreateAssetMenu]
[System.Serializable]
public class FoodItem : ScriptableObject {
	[SerializeField] private int value;

	public int Value => value;
}
