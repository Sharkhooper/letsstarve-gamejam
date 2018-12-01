using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Inventory))]
public class InventoryEditor : Editor {
	public override void OnInspectorGUI() {
		Inventory i = target as Inventory;
		if (i == null) return;

		IReadOnlyDictionary<FoodItem, int> items = i.FoodItems;
		if(items == null) return;
		foreach (KeyValuePair<FoodItem, int> item in items) {
			EditorGUILayout.LabelField($"{item.Value}x {item.Key.name}");
		}
	}
}
