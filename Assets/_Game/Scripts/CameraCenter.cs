using System.Linq;
using UnityAtoms;
using UnityEngine;

namespace _Game.Scripts {
    public class CameraCenter : MonoBehaviour {
        public GameObjectList playerParty;

        private void Update() {
            if (playerParty.Count <= 0) {
                this.gameObject.SetActive(false);
                return;
            }
            
            this.transform.position = playerParty.List.Select(a => a.transform.position).Aggregate((a, b) => a + b);
            this.transform.position /= playerParty.Count;
        }
    }
}