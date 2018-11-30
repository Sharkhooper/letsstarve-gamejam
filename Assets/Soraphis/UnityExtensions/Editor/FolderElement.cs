using System.Linq;
using UnityEditor;
using UnityEngine;

[InitializeOnLoad]
public class FolderElement {

	[MenuItem("GameObject/FolderObject", false, 0)] 
	static void Create() {
		var go = new GameObject("Folder:");
		go.isStatic = true;
		go.transform.hideFlags = HideFlags.NotEditable;
		// go.hideFlags = HideFlags.NotEditable;
	}

	static FolderElement() {
		EditorApplication.hierarchyWindowItemOnGUI += OnHierarchyGUI;
		SceneView.onSceneGUIDelegate += OnScene;
		Selection.selectionChanged += OnSelectionChanged;
	}

	private static bool isFolder;
	
	private static void OnSelectionChanged() {
		isFolder = Selection.gameObjects.Any(go => (go.transform.hideFlags & HideFlags.NotEditable) != 0);
	}

	private static void DrawRect(Transform t, Vector3 dir, float size) {
		Handles.RectangleHandleCap(0, t.position + dir * size, t.rotation * Quaternion.LookRotation(dir), size, EventType.Repaint);
	}
	
	private static void DrawDot(Transform t, Vector3 dir, float size) {
		Handles.DotHandleCap(0, t.position + dir * size, t.rotation * Quaternion.LookRotation(dir), size * 0.0625f, EventType.Repaint);
	}
	
	private static void OnScene(SceneView sceneview) {
		Tools.hidden = false;
		if (!isFolder) return;
		
		const float size = 0.2f;
		for (var i = 0; i < Selection.gameObjects.Length; i++) {
			var go = Selection.gameObjects[i];
			var transform = go.transform;
			if (Event.current.type == EventType.Repaint)
			{
				Handles.color = Handles.xAxisColor;
				DrawRect(transform, Vector3.right, size);
				DrawDot(transform, Vector3.right, size);
				DrawDot(transform, -Vector3.right, size);
					
				Handles.color = Handles.yAxisColor;
				DrawRect(transform, Vector3.up, size);
				DrawDot(transform, Vector3.up, size);
				DrawDot(transform, -Vector3.up, size);
					
				Handles.color = Handles.zAxisColor;
				DrawRect(transform, Vector3.forward, size);
				DrawDot(transform, Vector3.forward, size);
				DrawDot(transform, -Vector3.forward, size);
			}
		}
		Tools.hidden = true;
	}


	static void OnHierarchyGUI(int instanceID, Rect selectionRect) {
		GameObject go = (GameObject)EditorUtility.InstanceIDToObject(instanceID);

		Rect rect = new Rect (selectionRect);
		rect.x += rect.width - 5;

		if (go != null && (go.transform.hideFlags & HideFlags.NotEditable) != 0) {
			var topline = new Rect(selectionRect);
			topline.height = 1;
			topline.width -= 5;
			EditorGUI.DrawRect(topline, Color.gray);

			rect.x -= 15;
			rect.width = 15;
			go.SetActive(GUI.Toggle(rect, go.activeInHierarchy, ""));
		}

	}
}
