using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class puzzle_data : MonoBehaviour
{

    GameObject button1;
    GameObject button2;    
    GameObject button3;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        button1 = GameObject.Find("Button-1");
        button2 = GameObject.Find("Button-2");
        button3 = GameObject.Find("Button-3");
        
    }

    // Update is called once per frame
    void Update()
    {
      

    }
}
