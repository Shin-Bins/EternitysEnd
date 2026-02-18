using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class green_switch : MonoBehaviour, IIinteractable
{

    GameObject greenorb;
    public AudioSource src;
    public int greennumber;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        greenorb = GameObject.Find("orb-2");
    }

    public void Interaction()
    {
        greennumber++;
        src.Play();
        Debug.Log(greennumber);
        greenorb.transform.rotation *= Quaternion.Euler(0f, 45f, 0f);
    }

    void Update()
    {
        if (greennumber >= 4)
        {
            greennumber = 0;
        }
    }
}

