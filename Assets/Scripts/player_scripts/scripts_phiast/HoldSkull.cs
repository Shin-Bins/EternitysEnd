using UnityEngine;

public class HoldSkull : MonoBehaviour
{
	public static HoldSkull Instance{get; private set;}

	private void Awake()
	{
		if(Instance != null)
		{
			Instance = this;
		}
		else{
			Destroy(this);
		}
	}
}
