using UnityAtoms;
using UnityEngine;

public class PartyMemberComponent : MonoBehaviour {

    public GameObjectList partyList;

    private void OnEnable() {
        partyList.Add(this.gameObject);
    }

    private void OnDisable() {
        partyList.Remove(this.gameObject);
    }


}