using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class MovementController : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;
    private Plane p = new Plane(Vector3.up, Vector3.zero);

    // Start is called before the first frame update
    void Start() {
        navMeshAgent = GetComponent<NavMeshAgent>(); 
    }

    // Update is called once per frame
    void Update() {
        if(! Input.GetMouseButton(0)) return;
        
        
        if(EventSystem.current.IsPointerOverGameObject()) return;
        
            
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        p.Raycast(ray, out float f);

        navMeshAgent.SetDestination(ray.GetPoint(f));
    }

    private void OnDrawGizmos() {
        if(! Application.isPlaying) return;
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(navMeshAgent.destination, 0.25f);
    }
}
