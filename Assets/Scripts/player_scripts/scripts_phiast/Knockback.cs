using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;






public class Knockback : MonoBehaviour
{
    private CharacterController character;
     public float force = 5f;

    bool goback = false;
    

   
    





    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        character = GetComponent<CharacterController>();
        
      
        
        
    }
   
    // Update is called once per frame
    void Update()
    {
        character = GetComponent<CharacterController>();

        if (goback == true)
        {
            this.transform.Translate(Vector3.forward * force * Time.deltaTime);
            StartCoroutine(knocktimer());
            
        }

        if (goback == false)
        {
            transform.Translate(0,0,0);
        }


    }

   
    public void addimpact()
    {
        goback = true;
    }

    public IEnumerator knocktimer()
    {
        yield return new WaitForSeconds(0.5f);
        goback = false;
    }

 
    
}
