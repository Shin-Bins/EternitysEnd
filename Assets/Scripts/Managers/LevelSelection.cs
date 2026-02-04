using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class LevelSelection : MonoBehaviour
{
    public Button[] lvlbuttons;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        int LevelAt = PlayerPrefs.GetInt("LevelAT", 2);

        for (int i = 0; i < lvlbuttons.Length; i++)
        {
            if (i + 2 > LevelAt)
                lvlbuttons[i].interactable = false;

        }
    }

  
}
