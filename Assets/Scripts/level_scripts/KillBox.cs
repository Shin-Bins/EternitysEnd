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
		phis = GameObject.Find("Phiast-tank 1");
		skull = GameObject.Find("skull");
		respawnpoint = GameObject.Find("reset");
		check = GameObject.Find("Checkpoint");
		checka = check.transform;
		cha = phis.GetComponent<CharacterController>();
		phis.transform.position = checka.position;
		
        


    }

	public void pleasework()
	{
		cha.enabled = false;
		phis.transform.position = checka.position;
		cha.enabled = true;

	}
    void OnTriggerExit(Collider other)
{
	if(other.CompareTag("phiast"))
	{

		
		PhiastHealth health = other.GetComponent<PhiastHealth>();
		health.TakeDamage(transform.position);
			pleasework();
			

		
		
			
			
			
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
