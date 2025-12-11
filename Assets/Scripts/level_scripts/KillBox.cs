using UnityEngine;

public class KillBox : MonoBehaviour
{
void OnTriggerEnter(Collider other)
{
	if(other.CompareTag("phiast"))
	{
		PhiastHealth health = other.GetComponent<PhiastHealth>();
		health.TakeDamage();
		GameManager.Instance.RespawnCheckpoint(other.gameObject);
	}
	if(other.CompareTag("skull"))
	{
		GameManager.Instance.RespawnCheckpoint(other.gameObject);
	}
}
}
