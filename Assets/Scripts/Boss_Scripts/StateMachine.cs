using UnityEngine;

public class StateMachine : MonoBehaviour
{
	State currentState;
   public State CurrentState => currentState;
   protected bool inTransition;


   private void Start()
   {
	   ChangeState<DecideState>();
   }

   public void ChangeState<T>() where T: State
   {
	   T targetState = GetComponent<T>();
	   if(targetState == null)
	   {
		   Debug.Log("Nay state found pallie");
		   return;
	   }
	   StartNewState(targetState);
   }

   public void StartNewState(State targetState)
	{
		if(currentState != targetState && !inTransition)
		{
			CallNewState(targetState);
		}
	}

	public void CallNewState(State newState)
	{
		inTransition = true;
		//
		currentState?.Exit();
		currentState = newState;
		currentState?.Enter();
		//
		inTransition = false;
	}

	private void Update()
	{
		if(CurrentState != null && !inTransition)
		{
			CurrentState.Tick();
		}
	}
}
