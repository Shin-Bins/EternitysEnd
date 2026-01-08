using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class tankmove : MonoBehaviour
{
    [Header("Movement vars")]
    public float _speed;
    public float holdSpeed;
    public float acceleration;
    public float jforce = 1.0f;
    public Vector3 Jumpnow;

    private Tankcon _tankcon;
    bool rotateright;
    bool rotateleft;
    bool Forwards;
    bool Backwards;

    [Header("Jump and Ground check")]
    bool jump;
    public bool grounded;
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;

    public bool holdingSkull = false;

    Rigidbody rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        rb = GetComponent<Rigidbody>();
        Jumpnow = new Vector3(0.0f, 2.0f, 0.0f);
    }

    // Update is called once per frame
    private void Update()
    {
        //Vector3 positionChange = new Vector3(
        //_tankcon.InputVector.x,
        // 0,
        //_tankcon.InputVector.y)
        // * Time.deltaTime
        //* _speed;


        //transform.position += positionChange;

        // this shit sucks ass do not use ever, it's staying here to be shamed

        grounded = Physics.CheckSphere(groundCheck.position, groundCheckRadius, groundLayer);
    

        if (rotateleft)
        {
            transform.Rotate(transform.up, -100.0f * Time.deltaTime, Space.World);
        }
        if (rotateright)
        {
            transform.Rotate(transform.up, 100.0f * Time.deltaTime, Space.World);
        }

        if (Forwards)
        {
            transform.Translate(transform.forward * acceleration * Time.deltaTime, Space.World);
            acceleration = 5;
        }

        if (Backwards)
        {
            transform.Translate(transform.forward * acceleration * Time.deltaTime, Space.World);
            acceleration = -5;
        }

        if (jump)
        {
            if (grounded == true && !holdingSkull)
            {
                Jump();
            }
            
        }
    }

    private void Awake()
    {
        _tankcon = GetComponent<Tankcon>();
    }

    void Jump()
    {
        Vector3 velocity = rb.linearVelocity;
        velocity.y = 0f;
        rb.linearVelocity = velocity;
        rb.AddForce(Jumpnow * jforce, ForceMode.Impulse);
        grounded = false;
    }

    public void OnRotateleft(InputValue val)
    {
        Debug.Log("Left " + val.Get<float>());
        switch (val.Get<float>())
        {
            case 0: rotateleft = false; break;
            case 1: rotateleft = true; break;
            default: rotateleft = false; break;
        }
    }
    public void OnRotateright(InputValue val)
    {
        Debug.Log("Right " + val.Get<float>());
        switch (val.Get<float>())
        {
            case 0: rotateright = false; break;
            case 1: rotateright = true; break;
            default: rotateright = false; break;
        }
    }

    void OnForwards(InputValue val)
    {
       Debug.Log("Forward" + val.Get<float>());
        switch (val.Get<float>())
        {
            case 0: Forwards = false; break;
            case 1: Forwards = true; break;
            default: Forwards = false; break;
                
        }
    }


    void OnBackwards(InputValue val)
    {
        Debug.Log("Backwards" + val.Get<float>());
        switch (val.Get<float>())
        {
            case 0: Backwards = false; break;
            case 1: Backwards = true; break;
            default: Backwards = false; break;
                
        }
    }

    void OnJump(InputValue val)
    {
         Debug.Log("Jump" +  val.Get<float>());
        switch (val.Get<float>())
        {
            case 0: jump = false; break;
            case 1: jump = true; break;
            default: jump = false; break;
        }
    }
}
