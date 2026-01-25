using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using System.Collections.Generic;


public class punch : MonoBehaviour
{
   public GameObject hitbox;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        hitbox = GameObject.Find("hitbox-punch");
        hitbox.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnPunch()
    {
        hitbox.SetActive(true);
        StartCoroutine(punchdelay());
    }

    private IEnumerator punchdelay()
    {
        yield return new WaitForSeconds(0.5f);
        hitbox.SetActive(false );
    }

  
}
