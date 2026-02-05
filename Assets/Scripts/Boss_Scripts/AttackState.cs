using UnityEngine;

public class AttackState : State
{
	protected StateMachine stateMachine;
	private BossStats boss;
	private bool hasAttacked = false;
	private Animator bossAnim;

  	private void Awake()
	{
		stateMachine = GetComponent<StateMachine>();
		boss = GetComponent<BossStats>();
		bossAnim = GetComponent<Animator>();
	}

	public override void Enter()
	{
		Debug.Log("attack state");
		bossAnim.SetBool("Attack", true);
		hasAttacked = false;
		SlamAttack();
	}

	private void SlamAttack()
	{
		Debug.Log("BAM");
		hasAttacked = true;
	}

	public override void Tick()
	{
		if(hasAttacked)
		{
			bossAnim.SetBool("AttackEnd", true);
			stateMachine.ChangeState<DecideState>();
		}
	}

	public override void Exit()
	{
		bossAnim.SetBool("Attack", false);
	}
}
