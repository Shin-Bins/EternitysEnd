using UnityEngine;

public class PushBlock : MonoBehaviour
{
	void OnCollisionEnter(Collision col)
	{
		if(col.gameObject.CompareTag("phiast"))
		{
			panzermove movement = col.gameObject.GetComponentInParent<panzermove>();
			if(movement != null)
			{
				movement.isPushing = true;
			}
		}
	}
	void OnCollisionExit(Collision col)
	{
		if(col.gameObject.CompareTag("phiast"))
		{
			panzermove movement = col.gameObject.GetComponentInParent<panzermove>();
			if(movement != null)
			{
				movement.isPushing = false;
			}
		}
	}
}
