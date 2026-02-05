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
                Debug.Log("hitem");
            }
            else
            {
                Debug.Log("Nay enemy");
            }

        }
    }
}
