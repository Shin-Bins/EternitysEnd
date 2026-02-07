using UnityEngine;

public class EndOneState : State
{
    protected StateMachine stateMachine;
	private BossStats boss;
	public GameObject currentTarget;
	private float holdTimer;
	private float rotationSpeed = 10f;
	private bool facingTarget = false;

	private Animator bossAnim;
	//the name of this is sketch but also I don't wanna redo everything xddd'
	private void Awake()
	{
		stateMachine = GetComponent<StateMachine>();
		boss = GetComponent<BossStats>();
		bossAnim = GetComponent<Animator>();
		
	}

	public override void Enter()
	{
		Debug.Log("readying state");
		holdTimer = 0f;
		facingTarget = false;
	}

	public void SetTarget(GameObject target)
	{
		currentTarget = target;
	}

	public override void Tick()
	{
		if(currentTarget != null)
        {
            Vector3 direction = currentTarget.transform.position - transform.position;
            direction.y = 0;
        
            if(direction != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);

                float angle = Quaternion.Angle(transform.rotation, targetRotation);
                if(angle < 5f && !facingTarget)
                {
                    facingTarget = true;
                    Debug.Log("Rotated to target! Attacking now!");
                    bossAnim.SetTrigger("SectionEnd");
                }
            }
		}
	}

	public override void Exit()
	{
		bossAnim.SetBool("PlayerFound", false);
		Debug.Log("Welcome to the true mans world");
	}
}
