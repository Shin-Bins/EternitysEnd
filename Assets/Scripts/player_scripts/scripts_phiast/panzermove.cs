using UnityEngine;
using UnityEngine.InputSystem;

public class panzermove : MonoBehaviour
{
    private Vector2 input;
    private float verticalVelocity = 0f;
    
    public float moveSpeed = 5f;
    public float turnSpeed = 100f;
    public float jumpHeight = 2f;
    public float gravity = 9.81f;
    public float pushForce = 1f;//how strong we push physics objects

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

    void OnControllerColliderHit(ControllerColliderHit hit)//Physics without da rigidbawdy
    {     
        Rigidbody body = hit.collider.attachedRigidbody;
        if (body != null && !body.isKinematic)
        {
            Vector3 pushDir = hit.moveDirection;
            pushDir.y = 0;

            if (Mathf.Abs(pushDir.x) > Mathf.Abs(pushDir.z))
            {
                pushDir = new Vector3(Mathf.Sign(pushDir.x), 0, 0);
            }
            else
            {
                pushDir = new Vector3(0, 0, Mathf.Sign(pushDir.z));
            }
        
            
            body.AddForce(pushDir * pushForce, ForceMode.Impulse);
        }
    }
}
