using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BossStats : MonoBehaviour
{
    [Header("Health")]
    private int maxHealth = 4;
    public int currentHealth;
    public bool canBeHurt = true;
    public bool isHurt = false;
    public float invinceFrames = 3f;

    [Header("Attack")]
    public float attackDelay = 10f;//how long in between attacks he doesn't do anything'
    public float holdAttack = 3f;//how long he holds his attackDelay
    public GameObject damZone;

    [Header("Progression")]
	private EndOneState sectionEnd;

	public GameObject sectionOne;
	public GameObject sectionTwo;
	public GameObject sectionThree;

	public GameObject wallOne;
	public GameObject wallTwo;
	public GameObject wallThree;
	private Destructible wallOneDest;
	private Destructible wallTwoDest;
	private Destructible wallThreeDest;

    private AudioSource src;
	public AudioClip wallDestruct;//this is for the progression walls smashing
	public AudioClip damagedAud;//evil phiast being hurt

    private StateMachine stateMachine;

    void Start()
    {
        currentHealth = maxHealth;
        src = GetComponent<AudioSource>();
        stateMachine = GetComponent<StateMachine>();
        damZone.SetActive(false);

        sectionEnd = GetComponent<EndOneState>();
		sectionOne.SetActive(false);
		sectionTwo.SetActive(false);
		sectionThree.SetActive(false);
    }

    public void DoDamage()
    {
        damZone.SetActive(true);
    }

    public void DontDamage()
    {
        damZone.SetActive(false);
    }

   public void Damaged()
    {
        if(canBeHurt)
        {
            currentHealth --;
            src.PlayOneShot(damagedAud);
        }
        if(currentHealth <= 0)
        {
            stateMachine.ChangeState<DeathState>();
        }
        else{
            stateMachine.ChangeState<DamagedState>();
        }
    }

    public void ChangeSection()
	{
		if(currentHealth == 3)
		{
			PhaseTwo();
		}
		if(currentHealth == 2)
		{
			PhaseThree();
		}
		if(currentHealth == 1)
		{
			PhaseFour();
		}
	}

	void PhaseTwo()
	{
		if(sectionOne != null)
		{
			sectionOne.SetActive(true);
			wallOne.tag = "Destruct";
			Destructible dest = wallOne.AddComponent<Destructible>();
			dest.destAud = wallDestruct;
			sectionEnd.SetTarget(wallOne);
			stateMachine.ChangeState<EndOneState>();
		}
	}

	void PhaseThree()
	{
		if(sectionTwo != null)
		{
			sectionTwo.SetActive(true);
			wallTwo.tag = "Destruct";
			Destructible dest = wallTwo.AddComponent<Destructible>();
			sectionEnd.SetTarget(wallTwo);
			stateMachine.ChangeState<EndOneState>();
		}
	}

	void PhaseFour()
	{
		if(sectionThree != null)
		{
			sectionThree.SetActive(true);
			wallThree.tag = "Destruct";
			Destructible dest = wallThree.AddComponent<Destructible>();
			sectionEnd.SetTarget(wallThree);
			stateMachine.ChangeState<EndOneState>();
		}
	}
}
