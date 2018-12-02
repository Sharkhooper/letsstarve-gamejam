using UnityEngine;

namespace Soraphis {
    public static class VectorExtensions {
        public static Vector2 xz(this Vector3 v) {    return new Vector2(v.x, v.z);    }
        public static Vector2Int xz(this Vector3Int v) {    return new Vector2Int(v.x, v.z);    }
        public static Vector2Int xy(this Vector3Int v) {    return new Vector2Int(v.x, v.y);    }
        
        public static Vector3 x_y(this Vector2 v) {    return new Vector3(v.x, 0, v.y);    }
        public static Vector3Int x_y(this Vector2Int v) {    return new Vector3Int(v.x, 0, v.y);    }
        
        public static Vector3 _xy(this Vector2 v) {    return new Vector3(0, v.x, v.y);    }
        public static Vector3Int _xy(this Vector2Int v) {    return new Vector3Int(0, v.x, v.y);    }
        
        public static Vector3Int xy_(this Vector2Int v) {    return new Vector3Int(v.x, v.y, 0);    }
     
        
        public static Vector3 WithY(this Vector3 v, float y) {    return new Vector3(v.x, y, v.z);    }
        
        public static bool inRange(this Vector3 a, Vector3 b, float range) {    return Vector3.SqrMagnitude(b - a) < range*range;    }
    }
}
