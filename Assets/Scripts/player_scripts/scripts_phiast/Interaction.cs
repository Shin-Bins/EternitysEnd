using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using JetBrains.Annotations;

interface IIinteractable
{
    public void Interaction();
}

public class Interaction : MonoBehaviour
{
    public Transform Interactionsource;
    public float interactionrange;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update() 
    { 
    
    }

    public void OnInteract()
    {
        Ray r = new Ray(Interactionsource.position, Interactionsource.forward);
        if (Physics.Raycast(r, out RaycastHit hitInfo, interactionrange))
        { 
            if (hitInfo.collider.gameObject.TryGetComponent(out IIinteractable interactOBJ))
            {
                interactOBJ.Interaction();
            }
        }
       
    }
}
