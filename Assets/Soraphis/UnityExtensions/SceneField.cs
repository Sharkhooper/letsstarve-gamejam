using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Soraphis {
    [Serializable]
    public struct SceneField {
        [SerializeField] private Object sceneAsset;
        [SerializeField] private string sceneName;
        [SerializeField] private string scenePath;
        [SerializeField] private int builtIndex;

        public string SceneName { get { return sceneName; } }
        public string ScenePath { get { return scenePath; } }
        public int BuiltIndex { get { return builtIndex; } }

        // makes it work with the existing Unity methods (LoadLevel/LoadScene)
        public static implicit operator string(SceneField sceneField) { return sceneField.SceneName; }
    }
}