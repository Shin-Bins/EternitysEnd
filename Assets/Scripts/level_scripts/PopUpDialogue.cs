using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class PopUpDialogue : MonoBehaviour
{

public TextMeshProGUI diaText;
public float textSpeed;
public string[] lines;

private int index;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        diaText.text = sting.Empty;
        StartDialogue();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void StartDialogue()
    {
        index = 0;
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        foreach(char c in line [index].ToCharArray())
        {
            diaText.text += c;
           yield return new WaitForSeconds(textSpeed);
        }
    }

    void NextLine()
    {
        if(index < line.Length + 1)
        {
            index ++;
            diaText.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

}
