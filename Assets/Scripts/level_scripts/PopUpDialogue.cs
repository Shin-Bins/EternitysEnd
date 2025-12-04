using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
public class PopUpDialogue : MonoBehaviour
{

public TMP_Text diaText;
public float textSpeed;
public string[] lines;

private int index;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
         Cursor.visible = true;
         Cursor.lockState = CursorLockMode.None;
         diaText.text = string.Empty;
         StartDialogue();
    }

    void StartDialogue()
    {
        index = 0;
        StartCoroutine(TypeLine());
      
    }

    IEnumerator TypeLine()
    {
        foreach(char c in lines [index].ToCharArray())
        {
           diaText.text += c;
           yield return new WaitForSeconds(textSpeed);
        }
    }

    public void NextLine()
    {
        if(index < lines.Length - 1)
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
