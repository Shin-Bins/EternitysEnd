using UnityEngine;

public class DamagedState : State
{

    protected StateMachine stateMachine;
	private BossStats boss;
	private float damagedTimer;
	private Animator bossAnim;

    private void Awake()
	{
		stateMachine = GetComponent<StateMachine>();
		boss = GetComponent<BossStats>();
		bossAnim = GetComponent<Animator>();
	}

	public override void Enter()
	{
		boss.canBeHurt = false;
		damagedTimer = 0f;
		bossAnim.SetBool("DamageDealt", true);
	}

	public override void Tick()
	{
		damagedTimer += Time.deltaTime;

		if(damagedTimer >= boss.invinceFrames)
		{
			stateMachine.ChangeState<DecideState>();
			boss.ChangeSection();
		}
	}

	public override void Exit()
	{
		boss.canBeHurt = true;
		bossAnim.SetBool("DamageDealt", false);
	}
}
