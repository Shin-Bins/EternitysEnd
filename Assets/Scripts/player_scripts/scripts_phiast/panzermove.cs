using UnityEngine;
using UnityEngine.InputSystem;

public class panzermove : MonoBehaviour
{
    private Vector2 input;
    private float verticalVelocity = 0f;
    
    public float moveSpeed = 7f;
    public float turnSpeed = 100f;
    public float jumpHeight = 2f;
    public float gravity = 9.81f;

    public bool isPushing = false;
    private float pushSpeed = 4f;
    public bool holdingSkull = false;
    
    private CharacterController controller;
    
    void Awake()
    {
        controller = GetComponent<CharacterController>();
    }
    
    void Update()
    {
        bool isGrounded = controller.isGrounded;
        
        if (input.x != 0)
        {
            float turn = input.x * turnSpeed * Time.deltaTime;
            transform.Rotate(0, turn, 0);
        }

        if (isGrounded && verticalVelocity < 0)
        {
            verticalVelocity = -2f;
        }
        else
        {
            verticalVelocity -= gravity * Time.deltaTime;
        }
        
        Vector3 direction = transform.forward * input.y * moveSpeed;
        direction.y = verticalVelocity;
        
        controller.Move(direction * Time.deltaTime);

        if(isPushing)
        {
            moveSpeed = pushSpeed;
        }
        else{
            moveSpeed = 7f;
        }
    }
    
    public void OnMove(InputValue value)
    {
        input = value.Get<Vector2>();
    }
    
    public void OnJump()
    {
        if (controller.isGrounded && !holdingSkull)
        {
            verticalVelocity = Mathf.Sqrt(jumpHeight * 2f * gravity);
        }
    }
}
