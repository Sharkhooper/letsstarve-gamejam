using System;
using System.Linq;
using Soraphis;
using UnityEditor;
using UnityEditor.Build.Content;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;


[CustomPropertyDrawer(typeof(SceneField))]
public class SceneFieldDrawer : PropertyDrawer {

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
        EditorGUI.BeginProperty(position, label, property);
        OnPropertyGUI(position, property, label);
        EditorGUI.EndProperty( );
    }
    
    public override float GetPropertyHeight(SerializedProperty property, GUIContent label) {
        var scene = property.FindPropertyRelative("sceneAsset")?.objectReferenceValue;
        SceneAsset sceneAsset = scene as SceneAsset;
        if(sceneAsset == null) base.GetPropertyHeight(property, label);
        int buildIndex = SceneUtility.GetBuildIndexByScenePath(property.FindPropertyRelative("scenePath").stringValue);
        if (buildIndex != -1) return base.GetPropertyHeight(property, label);
        return base.GetPropertyHeight(property, label) * 2 + 5;
    }

    private void OnPropertyGUI(Rect position, SerializedProperty property, GUIContent label){
        Rect lower;
        position = position.SnipRectV(EditorGUIUtility.singleLineHeight, out lower);
        
        var scene = property.FindPropertyRelative("sceneAsset")?.objectReferenceValue;
        SceneAsset sceneAsset = scene as SceneAsset;
        
        int buildIndex = SceneUtility.GetBuildIndexByScenePath(property.FindPropertyRelative("scenePath").stringValue);
        Rect buttonRect = new Rect();
        if (buildIndex == -1) position = position.SnipRectH(position.width - 70, out buttonRect);
        
        // RENDER LABEL:
        position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);
        
        // RENDER OBJECT FIELD:
        EditorGUI.BeginChangeCheck();
        scene = EditorGUI.ObjectField(position, scene, typeof(SceneAsset), false);
        if (EditorGUI.EndChangeCheck()) {
            property.FindPropertyRelative("sceneAsset").objectReferenceValue = scene;
            sceneAsset = scene as SceneAsset;
            if (sceneAsset != null) {
                property.FindPropertyRelative("sceneName").stringValue = sceneAsset.name;
                var scenePath = AssetDatabase.GetAssetPath(scene);
                property.FindPropertyRelative("scenePath").stringValue = scenePath;
                // var assetsIndex = scenePath.IndexOf("Assets", StringComparison.Ordinal) + 7;
                // var extensionIndex = scenePath.LastIndexOf(".unity", StringComparison.Ordinal);
                // scenePath = scenePath.Substring(assetsIndex, extensionIndex - assetsIndex);

            }
        }

        buildIndex = SceneUtility.GetBuildIndexByScenePath(property.FindPropertyRelative("scenePath").stringValue);
        property.FindPropertyRelative("builtIndex").intValue = buildIndex;
        if (buildIndex != -1) return;
        
        EditorGUI.HelpBox(lower, "Scene is not added in the build settings", MessageType.Warning);
        
        // OPTIONAL: RENDER BUTTON 
        if (scene != null && scene is SceneAsset) {
            
            if (GUI.Button(buttonRect, "Fix Now")) {
                AddSceneToBuildSettings(sceneAsset);
            }
        }
            
        
        
        
    }
    
    private void AddSceneToBuildSettings(SceneAsset sceneAsset) {
        var scenePath = AssetDatabase.GetAssetPath(sceneAsset);
        // var assetsIndex = scenePath.IndexOf("Assets", StringComparison.Ordinal) + 7;
        // var extensionIndex = scenePath.LastIndexOf(".unity", StringComparison.Ordinal);
        // scenePath = scenePath.Substring(assetsIndex, extensionIndex - assetsIndex);
        
        var scenes = EditorBuildSettings.scenes.ToList();
        scenes.Add(new EditorBuildSettingsScene(scenePath, true));

        EditorBuildSettings.scenes = scenes.ToArray();
    }

}
