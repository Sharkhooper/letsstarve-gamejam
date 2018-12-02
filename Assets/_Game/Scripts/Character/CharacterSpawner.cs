using System.Threading;
using UnityAtoms;
using UnityEngine;

public class CharacterSpawner : VoidListener {
    
    public GameObject characterPrefab;
    
    public void Spawn() {
        Instantiate(characterPrefab, this.transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }
}