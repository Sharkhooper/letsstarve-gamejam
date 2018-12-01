using UnityEngine;

public class FoodInventoryUI : MonoBehaviour {
    public Inventory inventory;
    public GameObject prefabButton;

    private void OnEnable() {
        var foodItems = inventory.FoodItems;

        int i = 0; 
        foreach (var kv in foodItems) {
            var food = kv.Key;

            var go = transform.childCount <= i ? Instantiate(prefabButton, this.transform) : transform.GetChild(i).gameObject;
            var button = go.GetComponent<EatFoodButton>();
            button.item = food;
            
            go.SetActive(true);
            
            ++i;
        }
        
        for (; i < transform.childCount; ++i) {
            transform.GetChild(i).gameObject.SetActive(false);
        }
        
    }
    
}