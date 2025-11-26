using UnityEngine;
using UnityEngine.InputSystem;

public class SkullControl : MonoBehaviour
{
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;
    private bool isGrounded = true;

    public float slamForce = 10;
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
    }

    void GroundSlam(InputAction.CallbackContext context)
    {
        while(isGrounded && transform.parent == null)
        {
            srb.AddForce(-transform.up * slamForce);

        }
    }

    void SetupSkullControls()
    {
	    playerControls.Enable();
	    playerControls.Skull.Slam.performed += GroundSlam;

    }

    void OnDisable()
    {
        playerControls.Disable();
        playerControls.Skull.Slam.performed -= GroundSlam;
    }
}
