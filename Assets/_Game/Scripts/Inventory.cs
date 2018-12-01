#if UNITY_EDITOR
using UnityEditor;
#endif
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public sealed class Inventory : ScriptableObject {
	private readonly Dictionary<FoodItem, int> foodItems = new Dictionary<FoodItem, int>();

	public IReadOnlyDictionary<FoodItem, int> FoodItems => foodItems;

#if UNITY_EDITOR
	private void Awake() {
		EditorApplication.playModeStateChanged += _ => Clear();
	}
#endif

	private void AddItemIfMissing(FoodItem item) {
		if (!foodItems.ContainsKey(item)) {
			foodItems.Add(item, 0);
		}
	}

	public void AddItem(FoodItem item, int amount = 1) {
		Debug.Assert(amount > 0, $"Amount to add to FoodItem {item.name} should be larger than 0, but got {amount}");

		AddItemIfMissing(item);
		foodItems[item] += amount;
	}

	public void Clear() {
		foodItems.Clear();
	}

	public void RemoveItem(FoodItem item, int amount = 1) {
		Debug.Assert(amount > 0, $"Amount to subtract from FoodItem {item.name} should be larger than 0, but got {amount}");

		AddItemIfMissing(item);
		foodItems[item] -= amount;

		Debug.Assert(foodItems[item] >= 0, $"After subtacting {amount} from {item.name} has a negative amount {foodItems[item]} left");
	}
}
