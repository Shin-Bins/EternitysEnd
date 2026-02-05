using UnityEngine;

public class DecideState : State
{
	protected StateMachine stateMachine;
	private BossStats boss;
	private Transform phiastLocal;
	private float rotationSpeed = 10f;
	private float followTimer;
	private Animator bossAnim;

	private void Awake()
	{
		stateMachine = GetComponent<StateMachine>();
		phiastLocal = GameObject.FindGameObjectWithTag("phiast").transform;
		boss = GetComponent<BossStats>();
		bossAnim = GetComponent<Animator>();
	}

	public override void Enter()
	{
		Debug.Log("deciding state");
		followTimer = 0f;
	}

	public override void Tick()
	{
		if(phiastLocal != null)
		{
			Vector3 direction = phiastLocal.position - transform.position;
			direction.y = 0;
        
			if(direction != Vector3.zero)
			{
				Quaternion targetRotation = Quaternion.LookRotation(direction);
				transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
			}
		}
		else{
				Debug.Log("Where's that guy");
			}

			followTimer += Time.deltaTime;
			if(followTimer >= boss.attackDelay)
			{
				bossAnim.SetBool("PlayerFound", true);
				stateMachine.ChangeState<ReadyAttackState>();
			}
	}

	public override void Exit()
	{
		Debug.Log("I'm not gonna sugarcoat it for ya");
	}
}
