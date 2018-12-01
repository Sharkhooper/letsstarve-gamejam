using UnityEngine;

[CreateAssetMenu]
public class FoodItem : ScriptableObject {
	[SerializeField] private int value;

	public int Value => value;
}
