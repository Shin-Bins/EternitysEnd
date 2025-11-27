using UnityEngine;
using UnityEngine.InputSystem;

public class Tankcon : MonoBehaviour
{
   
    public Vector2 InputVector {  get; private set; }

    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        
        
    }

    private void OnMove(InputValue inputValue)
    {
        InputVector = inputValue.Get<Vector2>();
    }

    // Update is called once per frame
   
}
