using UnityEngine;

namespace DefaultNamespace {
    public static class TransformExtension {
        
        public static bool inRange(this Transform a, Vector3 b, float range) {    return Vector3.SqrMagnitude(b - a.position) < range*range;    }
        public static bool inRange(this Transform a, Transform b, float range) {    return Vector3.SqrMagnitude(b.position - a.position) < range*range;    }
        
    }
}