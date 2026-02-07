using UnityEngine;

public class BossDamage : MonoBehaviour
{
	protected StateMachine stateMachine;

	void Start()
	{
		stateMachine = GetComponent<StateMachine>();
	}

	void OnTriggerEnter(Collider other)
	{
		if(other.CompareTag("Enemy"))
		{
			EnemyPatrol enemy = other.GetComponent<EnemyPatrol>();
			if(enemy != null)
			{
				enemy.SkullDisaster();
			}
		}
		if(other.CompareTag("Destruct"))
		{
			Destructible dest = other.GetComponent<Destructible>();
			if(dest != null)
			{
				dest.DestructObject();
			}
		}
	}
}
