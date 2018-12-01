using UnityAtoms;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent),typeof(StupidBehaviour),typeof(HealthComponent))]
public abstract class StupidActor : MonoBehaviour
{
    [SerializeField] protected int damage = 50;
	[SerializeField] private GameObjectList enemyList;

    protected StupidBehaviour behaviour;
    protected MeshRenderer renderer;
    protected HealthComponent health;
	protected Animator anim;

	protected void OnEnable() { enemyList.Add(gameObject); }
	protected void OnDisable() { enemyList.Remove(gameObject); }

	protected void Awake()
    {
		behaviour = GetComponent<StupidBehaviour>();
        renderer = transform.GetComponentInChildren<MeshRenderer>();
        health = transform.GetComponent<HealthComponent>();
	    anim = transform.GetComponentInChildren<Animator>();

        health.OnDeath += Death;
        health.OnDamageTaken += DamageTaken;

        behaviour.OnAttack += Attack;
        behaviour.OnMove += Move;
        behaviour.OnWait += Idle;
    }
	
	
	private float theta;
	public float Theta {
		get => theta;
		set {
			if (value < 0) {
				value += 360.0f;
			}

			theta = value;
		}
	}

	private Vector3 lastPos;

	public Vector3 Forward => Quaternion.Euler(0, -Theta, 0) * Vector3.right;

	private void Update() {

		if (anim != null)
		{
			int animationState = Mathf.FloorToInt((Theta + 405.0f) / 90.0f) - 4;
			anim.SetInteger("direction", animationState);
			anim.gameObject.transform.localScale = new Vector3(animationState == 2 ? -1 : 1, 1, 1);

			if ((lastPos - transform.position).sqrMagnitude < 0.01f) return;
			Theta = -Vector3.SignedAngle(Vector3.right, transform.position - lastPos, Vector3.up);
			lastPos = transform.position;
		}
	}

    protected abstract void Death(int damage);
    
    protected abstract void Attack(GameObject target);

    protected abstract void DamageTaken(int damage);

    protected abstract void Move();

    protected abstract void Idle();
}