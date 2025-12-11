using UnityEngine;

public class Checkpoint : MonoBehaviour
{
	
	void OnTriggerEnter(Collider other)
	{
		if(other.CompareTag("phiast") || other.CompareTag("skull"))
		{
			GameManager.Instance.SetCheckpoint(transform.position);
		}
	}
}
