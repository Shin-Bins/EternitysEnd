using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class tankmove : MonoBehaviour
{

    private Tankcon _tankcon;
    bool rotateright;
    bool rotateleft;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        Vector3 positionChange = new Vector3(
            _tankcon.InputVector.x,
            0,
            _tankcon.InputVector.y)
            * Time.deltaTime;

        transform.position += positionChange;

        if (rotateleft )
        {
            transform.Rotate(new Vector3(0, -1, 0));
        }
        if (rotateright )
        {
            transform.Rotate(new Vector3(0, 1, 0));
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

}
