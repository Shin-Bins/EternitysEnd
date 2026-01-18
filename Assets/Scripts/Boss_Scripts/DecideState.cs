using UnityEngine;

public class DecideState : State
{
	protected StateMachine stateMachine;
	public Transform phiastLocal;
	float rotationSpeed = 10f;
	private void Awake()
	{
		stateMachine = GetComponent<StateMachine>();
	}

	public override void Enter()
	{
		Debug.Log("deciding state");
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
	}

	public override void Exit()
	{
		Debug.Log("exiting state");
	}
}
