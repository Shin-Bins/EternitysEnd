using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;

public class PopUpDialogue : MonoBehaviour
{

       [System.Serializable]
    public class DialogueLine
    {
        [TextArea(2, 4)]
        public string text;
        public Sprite charSprite;
        public AudioClip[] voice;
    }

    [SerializeField] private DialogueLine[] lines;
    [SerializeField] private bool hasTriggered = false;

    void OnEnable()
    {
        if (hasTriggered)
        {
            return;
        }
           Debug.Log($"PopUpDialogue OnEnable called. Lines array length: {(lines != null ? lines.Length : 0)}");
        
        if (lines == null || lines.Length == 0)
        {
            Debug.LogError("Lines array is null or empty in PopUpDialogue component!", this);
            return;
        }

        if (DialogueManager.Instance != null)
        {
            DialogueManager.Instance.StartDialogue(lines, this);
        }
        else
        {
            Debug.LogError("DialogueManager not found in scene!");
        }
    }

    public void MarkAsTriggered()
    {
        hasTriggered = true;
    }

    public void ResetDialogue()
    {
        hasTriggered = false;
    }
}