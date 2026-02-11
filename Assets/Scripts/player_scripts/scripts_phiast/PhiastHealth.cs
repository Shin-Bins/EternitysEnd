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
    public GameObject deathmenu;
    public GameObject deathscreen;
    public GameObject pauseholder;
    public GameObject skullrespawn;
    public GameObject cuan;
    

    void Start()
    {
        rend = GetComponent<Renderer>();
        originalColour = rend.material.color;
        currentHealth = maxHealth;
        controller = GetComponent<CharacterController>();
        skully = GetComponent<PickUpSkull>();
        src = GetComponent<AudioSource>();
        pauseholder = GameObject.Find("HealthUI");
        healthbar = pauseholder.GetComponent<Image>();
        deathscreen = GameObject.Find("deadscreen");
        skullrespawn = GameObject.Find("reset");
        cuan = GameObject.Find("StCuan (1)");
        healthbar.enabled = false;
        healthdrop = 100;
        healthbar.fillAmount = healthdrop / 100f;
        deathmenu.SetActive(false);
        deathscreen.SetActive(false);

        
        
        
        
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
            gameObject.GetComponent<Knockback>().addimpact();

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
        //GameManager.Instance.Death();
        deathmenu.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        gameObject.GetComponent<Pause>().enabled = false;
        deathscreen.SetActive(true);
        Time.timeScale = 0f;
        
    }

    private IEnumerator hurtdelay()
    {
        canBeHurt = false;
        yield return new WaitForSeconds(0.5f);
        canBeHurt = true;
        yield return new WaitForSeconds(4f);
        healthbar.enabled = false;

    }

    public void respwanfromdeath()
    {
        currentHealth = maxHealth;
        gameObject.SetActive(true);
        deathmenu.SetActive(false);
        deathscreen.SetActive(false);
        Time.timeScale = 1f;
        gameObject.GetComponent<Pause>().enabled = true;
        cuan.transform.position = skullrespawn.transform.position;

    }
   
   

}
