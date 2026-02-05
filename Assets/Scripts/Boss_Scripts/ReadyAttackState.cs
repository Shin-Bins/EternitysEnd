using UnityEngine;

public class ReadyAttackState : State
{
    protected StateMachine stateMachine;
	private BossStats boss;
	private Transform phiastLocal;
	private float rotationSpeed = 10f;
    private float holdTimer;
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
		Debug.Log("readying state");
		holdTimer = 0f;
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

			holdTimer += Time.deltaTime;
			if(holdTimer >= boss.holdAttack)
			{
				bossAnim.SetBool("Attack", true);
				stateMachine.ChangeState<AttackState>();
			}
	}

	public override void Exit()
	{
		bossAnim.SetBool("PlayerFound", false);
		Debug.Log("Welcome to the true mans world");
	}
}
