using System.Collections.Generic;
using System.Linq;
using UnityAtoms;
using UnityEngine;
using UnityEngine.EventSystems;

public class MovementController : MonoBehaviour {
	private Plane p = new Plane(Vector3.up, Vector3.zero);

	public GameObjectList partyList;

	public float formationPadding;

	void Update() {
		if (!Input.GetMouseButton(0)) return;

		if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject()) return;

		var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		p.Raycast(ray, out float f);

		Vector3 point = ray.GetPoint(f);
		Vector3 direction = transform.position - point;
		Quaternion rotation = Quaternion.FromToRotation(Vector3.forward, direction);

		// TODO: This is not optimal
		List<CharacterActor> list = partyList.List.Select(x => x.GetComponent<CharacterActor>()).OrderBy(x => x.healthComponent.Health).ToList();

		float sqrtCountf = Mathf.Ceil(Mathf.Sqrt(list.Count));
		int sqrtCount = (int) sqrtCountf;
		int numRows = Mathf.CeilToInt(list.Count / sqrtCountf);
		for (int i = 0; i < list.Count; ++i) {
			int row = i / sqrtCount;
			int column = i % sqrtCount;

			CharacterActor actor = list[i];

			Vector3 offsetFromPartyCenter = formationPadding * new Vector3(column, 0, row);

			// Align last row
			if (row == sqrtCount - 1 && (list.Count % sqrtCount) != 0) {
				offsetFromPartyCenter.x += 0.5f * (sqrtCount - (list.Count % sqrtCount));
			}

			offsetFromPartyCenter -= formationPadding * new Vector3(-0.5f + sqrtCountf / 2.0f, 0.0f, -0.5f + numRows / 2.0f);

			actor.navMeshComponent.SetDestination(point + rotation * offsetFromPartyCenter);
		}
	}
}
