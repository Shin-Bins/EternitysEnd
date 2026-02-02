using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using System.Collections.Generic;


public class punch : MonoBehaviour
{
   public GameObject hitbox;
   public panzermove Panzermove;
    public GameObject Phist;
    


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        hitbox = GameObject.Find("hitbox-punch");
        hitbox.SetActive(false);
    }

    void OnPunch()
    {
        hitbox.SetActive(true);
        Panzermove.enabled =false;
        Phist.transform.Translate(transform.forward * 60f * Time.deltaTime);
        StartCoroutine(punchdelay());
        Debug.Log("PUNCH");
    }

    private IEnumerator punchdelay()
    {
        yield return new WaitForSeconds(0.5f);
        hitbox.SetActive(false );
        Panzermove.enabled =true;
    }
}
