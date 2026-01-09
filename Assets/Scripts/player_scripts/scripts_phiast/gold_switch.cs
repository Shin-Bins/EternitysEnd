using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class gold_switch : MonoBehaviour, IIinteractable
{

    GameObject goldorb;

    public int goldnumber;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        goldorb = GameObject.Find("orb-3");
    }

    public void Interaction()
    {
        goldnumber++;
        Debug.Log(goldnumber);
        goldorb.transform.rotation *= Quaternion.Euler(0f, 45f, 0f);
    }

    void Update()
    {
        if (goldnumber >= 4)
        {
            goldnumber = 0;
        }
    }
}

