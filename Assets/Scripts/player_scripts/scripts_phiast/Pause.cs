using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Pause : MonoBehaviour
{

    bool ispaused = false;
    public GameObject pausemenu;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        pausemenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale > 0)
        {
            ispaused = false;
            pausemenu.SetActive(false);
        }
    }

    public void OnPause () 
    {
        if (ispaused == true)
        {
            Time.timeScale = 1;
            Debug.Log("unpause");
            ispaused = false;
            pausemenu.SetActive(false);
           
        }

        else if (ispaused == false)
        {
            Time.timeScale = 0;
            ispaused = true;
            Debug.Log("paused");
            pausemenu.SetActive(true);
        }

        


    }
}
