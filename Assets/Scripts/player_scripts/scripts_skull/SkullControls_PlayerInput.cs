using UnityEditor.AnimatedValues;
using UnityEngine;
using UnityEngine.InputSystem;

public class SkullControls_PlayerInput : MonoBehaviour
{
    bool rotatingLeft;
    bool rotatingRight;
    public AnimFloat windup;
    public AnimationCurve windupCurve;

    private bool isGrounded;
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;//how big of an area we check for the ground. Keep small so we dont have a floating skull :)
    public LayerMask groundLayer;
    public float slamForce = 10f;
    private Rigidbody srb;

    //public Transform anPhiast;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        srb = GetComponent<Rigidbody>();   
    }

    public void OnFindPhiast()
    {
       // transform.LookAt(anPhiast); 
    }

    public void OnRotateLeft(InputValue val)
    {
        Debug.Log("Left " + val.Get<float>());
        switch (val.Get<float>())
        {
            case 0: rotatingLeft = false; break;
            case 1: rotatingLeft = true; break;
            default: rotatingLeft=false; break;
        }
    }
    public void OnRotateRight(InputValue val)
    {
        Debug.Log("Right " + val.Get<float>());
        switch (val.Get<float>())
        {
            case 0: rotatingRight = false; break;
            case 1: rotatingRight = true; break;
            default: rotatingRight = false; break;
        }
    }
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundCheckRadius, groundLayer);

        if (rotatingLeft && isGrounded)
        {
            transform.Rotate(new Vector3(0,-1,0));
        }
        if (rotatingRight && isGrounded)
        {
            transform.Rotate(new Vector3(0, 1, 0));
        }
    }

    public void OnSlam()
    {
        if (!isGrounded && transform.parent == null)
        {
            srb.linearVelocity = -transform.up * slamForce;
        }
    }
}
