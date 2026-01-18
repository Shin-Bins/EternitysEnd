using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Spikewallmove : MonoBehaviour
{
    public Transform start;
    public Transform end;
    public float speed;
    bool go;
    
   
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        go = true;
    }

    // Update is called once per frame
    void Update()
    {

        if (go = true)
        {
            forward();
        }

        if (go = false)
        {
            back();
        }
        
    }

    public void forward()
    {

        
        {
            transform.position = Vector3.MoveTowards(transform.position, start.position, speed * Time.deltaTime);
            if (transform.position == start.position)
            {
                go = false;
            }
        }
        

    }

    public void back()
    {
        transform.position = Vector3.MoveTowards(transform.position, end.position, speed * Time.deltaTime);
        if (transform.position == end.position)
        {
            go = true;
        }
    }
}
