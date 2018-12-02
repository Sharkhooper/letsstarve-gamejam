using DG.Tweening;
using NaughtyAttributes;
using UnityEngine;

public class PickUpFood : MonoBehaviour {
	[Required] [SerializeField] private Inventory inventory;
	[Required] [SerializeField] private FoodItem itemType;

	private new SpriteRenderer renderer;

	private void Awake() {
		renderer = GetComponentInChildren<SpriteRenderer>();
	}

	private void Start() {
		renderer.transform.DOLocalJump(Vector3.zero, 1, 3, 0.6f);

		if (itemType != null && itemType.graphic != null) {
			renderer.gameObject.SetActive(true);
			renderer.sprite = itemType.graphic;
		} else renderer.gameObject.SetActive(false);
	}

	void OnTriggerEnter(Collider other) {
		inventory.AddFood(itemType, 1);
		Destroy(this.gameObject);
	}
}
