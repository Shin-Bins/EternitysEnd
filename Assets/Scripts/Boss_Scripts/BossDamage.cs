using UnityEngine;

public class BossDamage : MonoBehaviour
{
	void OnTriggerEnter(Collider other)
	{
		if(other.CompareTag("Enemy"))
		{
			EnemyPatrol enemy = other.GetComponent<EnemyPatrol>();
			Destructible dest = other.GetComponent<Destructible>();
			if(enemy != null)
			{
				enemy.SkullDisaster();
			}

			if(dest != null)
			{
				dest.DestructObject();
			}
		}
	}
}
