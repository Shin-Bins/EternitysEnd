using UnityEngine;

public class AttackState : State
{
	protected StateMachine stateMachine;
	private BossStats boss;
	private bool hasAttacked = false;

  	private void Awake()
	{
		stateMachine = GetComponent<StateMachine>();
		boss = GetComponent<BossStats>();
	}

	public override void Enter()
	{
		Debug.Log("attack state");
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
			stateMachine.ChangeState<DecideState>();
		}
	}

	public override void Exit()
	{
		
	}
}
