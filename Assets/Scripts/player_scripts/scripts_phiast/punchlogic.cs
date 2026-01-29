using UnityEngine;

public class punchlogic : MonoBehaviour
{
  void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            EnemyPatrol enemy = other.GetComponent<EnemyPatrol>();
            if(enemy != null)
            {
                enemy.SkullDisaster();
            }
            else
            {
                Debug.Log("Nay enemy");
            }
                Debug.Log("hitem");
            other.transform.parent.gameObject.SetActive(false);
        }
    }
}
