using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PhiastHealth : MonoBehaviour
{
    [SerializeField]private int maxHealth = 3;
    [SerializeField]private int currentHealth;
    [SerializeField] float invinceFrames = 2f;//how long till can be hurt again
    [SerializeField] float flashInterval = 0.2f;//how much flashing
    [SerializeField] bool canBeHurt = true;

    private Color damageFlash = Color.red;//colour we flash when damaged
    private Renderer rend;
    private Color originalColour;

     private Vector3 knockbackVelocity;
    public float knockbackForce = 5f;
    public float knockbackUpForce = 200f;
    public float knockbackDuration = 1f;
    private float knockbackTimer = 0f;//All this for a knockback btw

    private CharacterController controller;

    void Start()
    {
        rend = GetComponent<Renderer>();
        originalColour = rend.material.color;
        currentHealth = maxHealth;
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        if(knockbackTimer > 0)
        {
            controller.Move(knockbackVelocity * Time.deltaTime);
            knockbackVelocity.y -= 20f * Time.deltaTime;
            knockbackTimer -= Time.deltaTime;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Damage"))
        {
            TakeDamage(other.transform.position);
        }
    }

    public void TakeDamage(Vector3 damagePos)
    {
        if(canBeHurt)
        {
            currentHealth--;
            StartCoroutine(FlashEffect());

             Vector3 knockbackDir = (transform.position - damagePos).normalized;
             knockbackDir.y = 0; // Keep horizontal
        
             //horizontal knockback
             knockbackVelocity = knockbackDir * knockbackForce;
             knockbackVelocity.y = knockbackUpForce; // upward knockback
             knockbackTimer = knockbackDuration;

            if(currentHealth == 0)
            {
                PhiastDeath();
            }
        }
    }
    IEnumerator FlashEffect()
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

    void PhiastDeath()
    {
        Debug.Log("omae wa shindeiru");
        //we'll add in the death logic soon'
        GameManager.Instance.Death();
    }
   
}
