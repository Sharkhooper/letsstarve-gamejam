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
		Debug.Assert(amount < int.MaxValue, $"Amount to add to FoodItem {item.name} should be larger than 0, but got {amount}");

		AddItemIfMissing(item);
		foodItems[item] += amount;
	}

	public void Clear() {
		foodItems.Clear();
	}

	public bool RemoveItem(FoodItem item, int amount = 1) {
		AddItemIfMissing(item);
		if (foodItems[item] < amount) return false;
		
		Debug.Assert(foodItems[item] > amount, $"After subtracting {amount} from {item.name} would have a negative amount {foodItems[item]} left");
		
		foodItems[item] = Mathf.Max(foodItems[item] - amount, 0);

		return true;
	}
}
