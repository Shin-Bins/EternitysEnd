using UnityEngine;

public class BossManager : MonoBehaviour
{
	private BossStats daBoss;

	public GameObject sluaghOne;
	public GameObject sluaghTwo;
	public GameObject sluaghThree;

	void Awake()
	{
		daBoss = FindFirstObjectByType<BossStats>();
		sluaghOne.SetActive(false);
		sluaghTwo.SetActive(false);
		sluaghThree.SetActive(false);
	}

	void Update()
	{
		if(daBoss.currentHealth ==3)
		{
			PhaseTwo();
		}
	}

	void PhaseTwo()
	{
		if(sluaghOne != null)
		{
			Debug.Log("PhaseTwo baby");
			sluaghOne.SetActive(true);
		}
	}

	void PhaseThree()
	{
		if(sluaghTwo != null)
		{
			sluaghTwo.SetActive(true);
		}
	}

	void PhaseFour()
	{
		if(sluaghThree != null)
		{
			sluaghThree.SetActive(true);
		}
	}
}
