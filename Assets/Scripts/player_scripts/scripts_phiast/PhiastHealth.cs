using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;



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

    private AudioSource src;
    public AudioClip hurtAud;
    private CharacterController controller;
    private PickUpSkull skully;
    public Image healthbar;
    private float healthdrop;

    void Start()
    {
        rend = GetComponent<Renderer>();
        originalColour = rend.material.color;
        currentHealth = maxHealth;
        controller = GetComponent<CharacterController>();
        skully = GetComponent<PickUpSkull>();
        src = GetComponent<AudioSource>();
        healthbar.enabled = false;
        healthdrop = 100;
        healthbar.fillAmount = healthdrop / 100f;
        
    }


    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Damage"))
        {
            TakeDamage(other.transform.position);
        }

        if(other.CompareTag("HealthPack"))
        {
            if (currentHealth == 3)
            {
                Debug.Log("healthfull");
            }
            else
            {

                heal();
                other.gameObject.SetActive(false);

            }
            
        }
    }

    

    public void TakeDamage(Vector3 damagePos)
    {
        if(canBeHurt)
        {
            currentHealth--;
            healthbar.enabled = true;
            healthdrop -= 33;
            healthbar.fillAmount = healthdrop / 100f;

            StartCoroutine(FlashEffect());
            StartCoroutine(hurtdelay());

            src.PlayOneShot(hurtAud);
            skully.Drop();
            if(currentHealth == 0)
            {
                PhiastDeath();
            }
        }
    }

    public void heal()
    {
        if (currentHealth < 3)
        {
            currentHealth++;
            healthbar.enabled = true;
            healthdrop += 33;
            healthbar.fillAmount = healthdrop / 100f;
            StartCoroutine(hurtdelay());
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

    private IEnumerator hurtdelay()
    {
        canBeHurt = false;
        yield return new WaitForSeconds(0.5f);
        canBeHurt = true;
        yield return new WaitForSeconds(4f);
        healthbar.enabled = false;

    }
   
   

}
