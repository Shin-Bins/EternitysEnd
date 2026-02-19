using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class KillBox : MonoBehaviour
{

	public GameObject phis;
	public GameObject skull;
	public GameObject par;
	public Transform respawnpoint;
	public Transform checkpointpos;
	CharacterController cha;
	bool chec = false;
	

    private void Start()
    {
		phis = GameObject.Find("Phiast_NLA");
		skull = GameObject.Find("StCuan (1)");
		par = GameObject.Find("Player_current");
		respawnpoint = GameObject.Find("reset").transform;
		cha = phis.GetComponent<CharacterController>();
    }

	
    void OnTriggerExit(Collider other)
{
	if(other.CompareTag("phiast"))
	{
		PhiastHealth health = other.GetComponent<PhiastHealth>();
		health.TakeDamage(transform.position);
			if (chec == false)
			{
				GameManager.Instance.Death();
			}
			else
			{
				GameManager.Instance.Death();
				Debug.Log("Got da phiastie");
			}
	}
	if(other.CompareTag("skull"))
	{
            phis.transform.localPosition = checkpointpos.transform.position;
            GameManager.Instance.RespawnCheckpoint(other.gameObject);
			Physics.SyncTransforms();
	}
}

    public void ac()
    {
		Debug.Log("check");
		chec = true;
		checkpointpos = GameObject.Find("Checkpoint").transform;
    }

	

	
	
}
