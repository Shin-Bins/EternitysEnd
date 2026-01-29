using UnityEngine;

public class DamagedState : State
{

    protected StateMachine stateMachine;
	private BossStats boss;
	private float damagedTimer;
	private AudioSource src;
	public AudioClip damagedAud;

    private void Awake()
	{
		stateMachine = GetComponent<StateMachine>();
		boss = GetComponent<BossStats>();
		src = GetComponent<AudioSource>();
	}

	public override void Enter()
	{
		boss.canBeHurt = false;
		damagedTimer = 0f;
		src.PlayOneShot(damagedAud);
	}

	public override void Tick()
	{
		damagedTimer += Time.deltaTime;

		if(damagedTimer >= boss.invinceFrames)
		{
			stateMachine.ChangeState<DecideState>();
		}
	}

	public override void Exit()
	{
		boss.canBeHurt = true;
	}
}
