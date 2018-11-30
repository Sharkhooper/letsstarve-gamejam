using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTools : EditorWindow{

    [MenuItem("SceneTools/Toolbar")]
    public static void ShowExample()
    {
        var window = GetWindow<SceneTools>();
        window.minSize = new Vector2(10, 10);
        window.titleContent = new GUIContent("T");
        window.Show();
    }

    [MenuItem("Tools/CaptureScreenshot")]
    private static void Screenshot()
    {
        ScreenCapture.CaptureScreenshot("screenshot", 2);
    }
    
    private List<IEnumerator> coroutines = new List<IEnumerator>();

    public SceneTools() {
        Selection.selectionChanged += OnEnable; 
        SceneView.onSceneGUIDelegate -= OnSceneGUI;
        SceneView.onSceneGUIDelegate += OnSceneGUI;
    }

    private void OnDestroy() {
        SceneView.onSceneGUIDelegate -= OnSceneGUI;
    }

    private bool accordion_scenes;
    
    private void OnGUI(){

        if ((accordion_scenes = Soraphis.Editor.EditorUtils.Foldout("Scenes",accordion_scenes))){
        
        var count = SceneManager.sceneCountInBuildSettings;
        Rect line;
        for (int i = 0; i < count; ++i){
            line = EditorGUILayout.GetControlRect();
            // var scene = EditorSceneManager.GetSceneByBuildIndex(i);
            var path = SceneUtility.GetScenePathByBuildIndex(i);
            var sceneAsset = AssetDatabase.LoadAssetAtPath<SceneAsset>(path);
            if (sceneAsset == null){
                EditorGUI.LabelField(line.SplitRectH(4, 0, 3), path);
            }
            else EditorGUI.ObjectField(line.SplitRectH(4, 0, 3), sceneAsset, typeof(SceneAsset), false);

            if (GUI.Button(line.SplitRectH(4, 3, 1), "open")){
                EditorSceneManager.OpenScene(path);
            }
        }
        }
    }
    
    private void OnEnable() {
        
        /*
        var root = this.GetRootVisualContainer();
        root.Clear();


        Vector3[] rotations = new[] {Vector3.up, Vector3.right, Vector3.forward};
        string[] labels = new[] {"oY", "oX", "oZ"};
        
        var selected = Selection.transforms.Length > 0;
        Button button = null;

        for (int j = 0; j < 3; ++j) {
            int k = j;
            
            button = new Button(() => {
                Space relativeTo = Tools.pivotRotation == PivotRotation.Global ? Space.World : Space.Self;
                for (var i = 0; i < Selection.transforms.Length; i++) {
                    coroutines.Add(rotate(Selection.transforms[i], rotations[k], relativeTo));
                }
            }) {
                text = labels[k],
                style = {
                    alignSelf = Align.FlexStart,
                    maxWidth = 30, 
                    maxHeight = 30,
                }
            };
            button.SetEnabled(selected);
            root.Add(button);
        }
        */
    }

    private void OnSceneGUI(SceneView sceneview) {
        for (var index = coroutines.Count - 1; index >= 0; index--) {
            var coroutine = coroutines[index];
            if (!coroutine.MoveNext()) {
                coroutines.RemoveAt(index);
            }
        }
        
        Handles.BeginGUI();
        
        var bottomRow = new Rect(0, 0, Screen.width - 8, Screen.height-45).SnipRectV(-30);

        Space relativeTo = Tools.pivotRotation == PivotRotation.Global ? Space.World : Space.Self;
        Vector3[] rotations = new[] {Vector3.right, Vector3.up, Vector3.forward};

        Texture[] textures = new [] {
            AssetDatabase.LoadAssetAtPath<Texture>("Assets/Soraphis/Editor Resources/x_rotate.png"),
            AssetDatabase.LoadAssetAtPath<Texture>("Assets/Soraphis/Editor Resources/y_rotate.png"),
            AssetDatabase.LoadAssetAtPath<Texture>("Assets/Soraphis/Editor Resources/z_rotate.png"),
        };
        var contents = textures.Select(x => new GUIContent(x)).ToArray();
        var currentskin = GUI.skin;
        
        // AssetDatabase.CreateAsset(currentskin, "Assets/Soraphis/Editor Resources/editorskin.guiskin");
        
        //GUI.skin = style;
        GUI.enabled = Selection.transforms.Length > 0;
        for (int j = 3 - 1; j >= 0; --j) {
            if (GUI.Button(bottomRow.SnipRectH(-30, out bottomRow), contents[j])) {
                for (var i = 0; i < Selection.transforms.Length; i++) {
                    coroutines.Add(rotate(Selection.transforms[i], rotations[j], relativeTo));
                }
            }
        }
        GUI.enabled = true;
        GUI.skin = currentskin;
        Handles.EndGUI();
    }

    private IEnumerator rotate(Transform t, Vector3 v, Space s) {
        if (s == Space.Self) v = t.localToWorldMatrix * v;

        var start = t.rotation;
        var end = Quaternion.AngleAxis(90, v) * start;
        const int speed = 4;
        for (float f = 0; f < 1; f += speed/90f) {
            t.rotation = Quaternion.Lerp(start, end, f);
            SceneView.RepaintAll();
            yield return null;
        }
        t.rotation = Quaternion.Lerp(start, end, 1);
        
    } 
}
