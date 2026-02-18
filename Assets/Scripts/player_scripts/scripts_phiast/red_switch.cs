using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class red_switch : MonoBehaviour, IIinteractable
{

    GameObject redorb;
    public AudioSource src;
    public int rednumber;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        redorb = GameObject.Find("orb-1");
    }

   public void Interaction()
    {
        rednumber++;
        src.Play();
        Debug.Log(rednumber);
        redorb.transform.rotation *= Quaternion.Euler(0f, 45f, 0f);
    }

    void Update()
    {
        if (rednumber >= 4)
        {
            rednumber = 0;
        }
    }
}
