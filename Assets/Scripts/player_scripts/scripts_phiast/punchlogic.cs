using UnityEngine;

public class punchlogic : MonoBehaviour
{
  void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            Debug.Log("hitem");
            other.transform.parent.gameObject.SetActive(false);
        }
            
        
    }
}
