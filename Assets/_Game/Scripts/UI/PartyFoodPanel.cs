using UnityAtoms;
using UnityEngine;

public class PartyFoodPanel : MonoBehaviour {

    public GameObject prefabSegment;
    public GameObjectList partyList;

    private void OnEnable() {
        int i = 0;
        for (; i < partyList.Count; ++i) {
            var go = transform.childCount <= i ? Instantiate(prefabSegment, this.transform) : transform.GetChild(i).gameObject;
            
            var segment = go.GetComponent<PartyFoodUISegment>();
            segment.partyMember = partyList[i].GetComponent<CharacterActor>();
            go.SetActive(true);
        }

        for (; i < transform.childCount; ++i) {
            transform.GetChild(i).gameObject.SetActive(false);
        }
    }
    
}