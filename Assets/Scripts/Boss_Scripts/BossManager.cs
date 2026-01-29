using UnityEngine;

public class BossManager : MonoBehaviour
{
	private BossStats daBoss;

	public GameObject sectionOne;
	public GameObject sectionTwo;
	public GameObject sectionThree;

	void Awake()
	{
		daBoss = FindFirstObjectByType<BossStats>();
		sectionOne.SetActive(false);
		sectionTwo.SetActive(false);
		sectionThree.SetActive(false);
	}

	void Update()
	{
		if(daBoss.currentHealth == 3)
		{
			PhaseTwo();
		}
	}

	void PhaseTwo()
	{
		if(sectionOne != null)
		{
			Debug.Log("PhaseTwo baby");
			sectionOne.SetActive(true);
		}
	}

	void PhaseThree()
	{
		if(sectionTwo != null)
		{
			sectionTwo.SetActive(true);
		}
	}

	void PhaseFour()
	{
		if(sectionThree != null)
		{
			sectionThree.SetActive(true);
		}
	}
}
