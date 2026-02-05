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

    private StateMachine stateMachine;

    void Start()
    {
        currentHealth = maxHealth;
        stateMachine = GetComponent<StateMachine>();
        damZone.SetActive(false);
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
        }
        if(currentHealth <= 0)
        {
            stateMachine.ChangeState<DeathState>();
        }
        else{
            stateMachine.ChangeState<DamagedState>();
        }
    }
}
