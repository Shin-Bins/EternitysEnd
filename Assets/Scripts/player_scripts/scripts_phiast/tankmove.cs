using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class tankmove : MonoBehaviour
{

    public float _speed;
    public float acceleration;

    private Tankcon _tankcon;
    bool rotateright;
    bool rotateleft;
    bool Forwards;
    bool Backwards;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

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
    }

    private void Awake()
    {
        _tankcon = GetComponent<Tankcon>();
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
                acceleration = 5f;
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
                acceleration = -5f;
        }
    }


}
