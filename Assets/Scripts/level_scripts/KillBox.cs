using Unity.VisualScripting;
using UnityEngine;

public class KillBox : MonoBehaviour
{

	public GameObject phis;
	public GameObject skull;
	public GameObject respawnpoint;
	public Transform checka;
	public GameObject check;
	CharacterController cha;
	

    private void Start()
    {
		phis = GameObject.Find("Phiast_NLA");
		skull = GameObject.Find("StCuan (1)");
		respawnpoint = GameObject.Find("reset");
		check = GameObject.Find("Checkpoint");
		checka = check.transform;
		cha = phis.GetComponent<CharacterController>();
		
		
        


    }

	
    void OnTriggerEnter(Collider other)
{
	if(other.CompareTag("phiast"))
	{

		
		PhiastHealth health = other.GetComponent<PhiastHealth>();
		health.TakeDamage(transform.position);
		cha.enabled = false;
        GameManager.Instance.RespawnCheckpoint(other.gameObject);
		cha.enabled = true;







            //skull.transform.position = respawnpoint.transform.position;//
            Debug.Log("Got da phiastie");
	}
	if(other.CompareTag("skull"))
	{
            phis.transform.position = respawnpoint.transform.position;
            GameManager.Instance.RespawnCheckpoint(other.gameObject);
		
			
	}



}
}
