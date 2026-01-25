using UnityEngine;

public class punchlogic : MonoBehaviour
{
  void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            EnemyPatrol enemy = other.GetComponent<EnemyPatrol>();
            if(enemy != null)
            {
                enemy.SkullDisaster();
            }
            Debug.Log("hitem");
           // other.transform.parent.gameObject.SetActive(false);
        }
    }
}
