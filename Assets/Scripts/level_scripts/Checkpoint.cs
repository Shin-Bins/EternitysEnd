using UnityEngine;

public class Checkpoint : MonoBehaviour
{
	public KillBox killbox;


     void Start()
    {
		killbox = GameObject.FindGameObjectWithTag("killbox").GetComponent<KillBox>();
    }
    void OnTriggerEnter(Collider other)
	{
		if(other.CompareTag("phiast") || other.CompareTag("skull"))
		{
			GameManager.Instance.SetCheckpoint(transform.position);
            killbox.ac();

        }
	}
}
