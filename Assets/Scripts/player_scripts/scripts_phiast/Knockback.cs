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
    int speed = 20;
    public Transform player;
   

    bool goback = false;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        character = GetComponent<CharacterController>();
        player = gameObject.transform;
        
        
    }
   
    // Update is called once per frame
    void Update()
    {
        character = GetComponent<CharacterController>();
        
    }

    public void OnTestbutton()
    {
        addimpact();
    }

    public void addimpact()
    {

        Vector3 moveDirection = new Vector3(0, -25, 0);


        goback = true;
        


        if (goback == true)
        {
            character.SimpleMove(moveDirection);
            Debug.Log("fuck");
            StartCoroutine(Cease());
        }
       

        
    }

    private IEnumerator Cease()
    {
        yield return new WaitForSeconds(2); 
        goback = false;
        character.SimpleMove(Vector3.zero);
    }

 
    
}
