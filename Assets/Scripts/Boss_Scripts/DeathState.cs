using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathState : State
{
    protected StateMachine stateMachine;
	private BossStats boss;

    private void Awake()
	{
		stateMachine = GetComponent<StateMachine>();
		boss = GetComponent<BossStats>();
	}

	public override void Enter()
	{
		Destroy(gameObject);
	}

	public override void Tick()
	{
	
	}

	public override void Exit()
	{
		
	}
}
