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

    public void OnInteract()
    {
        Ray r = new Ray(Interactionsource.position, Interactionsource.forward);
        if (Physics.Raycast(r, out RaycastHit hitInfo, interactionrange))
        { 
            Debug.Log("Hit");
            if (hitInfo.collider.gameObject.TryGetComponent(out IIinteractable interactOBJ))
            {
                interactOBJ.Interaction();
            }
        }
       
    }
}
