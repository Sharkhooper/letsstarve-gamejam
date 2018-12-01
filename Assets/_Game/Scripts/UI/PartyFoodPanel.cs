using UnityAtoms;
using UnityEngine;

public class PartyFoodPanel : MonoBehaviour {

    public GameObject prefabSegment;
    public GameObjectList partyList;

    void Start() {
        for (var i = 0; i < partyList.Count; i++) {
            var go = transform.childCount <= i ? Instantiate(prefabSegment, this.transform) : transform.GetChild(i).gameObject;
            
            var segment = go.GetComponent<PartyFoodUISegment>();
            segment.partyMember = partyList[i].GetComponent<CharacterActor>();    
        }
    }
    
}