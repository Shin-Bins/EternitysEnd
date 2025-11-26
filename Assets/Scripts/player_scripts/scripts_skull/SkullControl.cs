using UnityEngine;
using UnityEngine.InputSystem;

public class SkullControl : MonoBehaviour
{
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;//how big of an area we check for the ground. Keep small so we dont have a floating skull :)
    public LayerMask groundLayer;
    private bool isGrounded = true;

    public float slamForce = 10;//How hard the skull hits the ground

    public float rotSpeed;//how fast it rotates left and right
    private bool rotLeft = false;
    private bool rotRight = false;
    private Rigidbody srb;
    private PlayerControls playerControls;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerControls = new PlayerControls();
        srb = GetComponent<Rigidbody>();
        SetupSkullControls();
    }

    // Update is called once per frame
    void Update()
    {
       isGrounded = Physics.CheckSphere(groundCheck.position, groundCheckRadius, groundLayer);

           if (rotLeft)
        {
            transform.Rotate(Vector3.up, -rotSpeed * Time.deltaTime, Space.Self);
        }
        if (rotRight)
        {
            transform.Rotate(Vector3.up, rotSpeed * Time.deltaTime, Space.Self);
        }

    }

    void GroundSlam(InputAction.CallbackContext context)
    {
        if(!isGrounded && transform.parent == null)
        {
            srb.linearVelocity = -transform.up * slamForce;

        }
    }
//below are the bools for rotation. Needs to be in update for a smoth turn around
   void RotLeftStart(InputAction.CallbackContext context)
    {
        rotLeft = true;
    }

    void RotLeftStop(InputAction.CallbackContext context)
    {
        rotLeft = false;
    }

    void RotRightStart(InputAction.CallbackContext context)
    {
        rotRight = true;
    }

    void RotRightStop(InputAction.CallbackContext context)
    {
        rotRight = false;
    }

    void SetupSkullControls()
    {
	    playerControls.Enable();
	    playerControls.Skull.Slam.performed += GroundSlam;
        playerControls.Skull.RotateLeft.started += RotLeftStart;
        playerControls.Skull.RotateLeft.canceled += RotLeftStop;
        playerControls.Skull.RotateRight.started += RotRightStart;
        playerControls.Skull.RotateRight.canceled += RotRightStop;

    }

    void OnDisable()
    {
        playerControls.Disable();
        playerControls.Skull.RotateLeft.started -= RotLeftStart;
        playerControls.Skull.RotateLeft.canceled -= RotLeftStop;
        playerControls.Skull.RotateRight.started -= RotRightStart;
        playerControls.Skull.RotateRight.canceled -= RotRightStop;

    }
}
