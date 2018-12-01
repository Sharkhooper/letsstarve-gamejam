#if UNITY_EDITOR
using UnityEditor;
#endif
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public sealed class Inventory : ScriptableObject
{
    private readonly Dictionary<FoodItem, int> foodItems = new Dictionary<FoodItem, int>();

    private readonly Dictionary<CharacterActor, GearItem> equippedItems = new Dictionary<CharacterActor, GearItem>();

    public IReadOnlyDictionary<FoodItem, int> FoodItems => foodItems;

    public IReadOnlyDictionary<CharacterActor, GearItem> EquippedItems => equippedItems;

#if UNITY_EDITOR
    private void Awake()
    {
        EditorApplication.playModeStateChanged += _ => Clear();
    }
#endif

    private void AddFoodIfMissing(FoodItem item)
    {
        if (!foodItems.ContainsKey(item))
        {
            foodItems.Add(item, 0);
        }
    }

    public void AddFood(FoodItem item, int amount = 1)
    {
        Debug.Assert(amount > 0, $"Amount to add to FoodItem {item.name} should be larger than 0, but got {amount}");
        Debug.Assert(amount < int.MaxValue, $"Amount to add to FoodItem {item.name} should be larger than 0, but got {amount}");

        AddFoodIfMissing(item);
        foodItems[item] += amount;
    }

    public void Clear()
    {
        foodItems.Clear();
        equippedItems.Clear();
    }

    public bool RemoveFood(FoodItem item, int amount = 1)
    {
        AddFoodIfMissing(item);
        if (foodItems[item] < amount) return false;

        Debug.Assert(foodItems[item] > amount, $"After subtracting {amount} from {item.name} would have a negative amount {foodItems[item]} left");

        foodItems[item] = Mathf.Max(foodItems[item] - amount, 0);

        return true;
    }

    public void equipGear(CharacterActor character, GearItem item)
    {
        if (equippedItems.ContainsKey(character))
        {
            equippedItems[character] = item;
        }
        else
        {
            equippedItems.Add(character, item);
        }
    }
}
