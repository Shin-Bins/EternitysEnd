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

    private Color damageFlash = Color.red;//colour we flash when damaged
    private Renderer rend;
    private Color originalColour;
    private float flashInterval = 0.2f;

    [Header("Attack")]
    public float attackDelay = 10f;//how long in between attacks he doesn't do anything'
    public float holdAttack = 3f;//how long he holds his attackDelay

    private StateMachine stateMachine;

    void Start()
    {
        currentHealth = maxHealth;
        rend = GetComponent<Renderer>();
        originalColour = rend.material.color;
        stateMachine = GetComponent<StateMachine>();
    }

   public void Damaged()
    {
        if(canBeHurt)
        {
            currentHealth --;
            StartCoroutine(DamageFlash());
        }
        if(currentHealth <= 0)
        {
            stateMachine.ChangeState<DeathState>();
        }
        else{
            stateMachine.ChangeState<DamagedState>();
        }
    }

    IEnumerator DamageFlash()
    {
        float elapsed = 0f;
        
       //only way to have the character flash rather than just turning another color
        while (elapsed < invinceFrames)
        {
            rend.material.color = damageFlash;
            yield return new WaitForSeconds(flashInterval);
            
            rend.material.color = originalColour;
            yield return new WaitForSeconds(flashInterval);
            
            elapsed += flashInterval * 2;
        }
        
        //resets material and bool
        rend.material.color = originalColour;
        canBeHurt = true;
    }
}
