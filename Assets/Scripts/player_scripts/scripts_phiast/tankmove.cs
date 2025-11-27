using Unity.VisualScripting;
using UnityEngine;

public class tankmove : MonoBehaviour
{

    private Tankcon _tankcon;
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
    }

    private void Awake()
    {
        _tankcon = GetComponent<Tankcon>();
    }

    
}
