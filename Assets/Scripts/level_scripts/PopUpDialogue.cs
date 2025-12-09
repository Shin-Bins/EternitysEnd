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
        public Sprite charSprite; //container for all aspects of the dialogue. Can add more in here like audio to personalize the speaker. Will update later
    }

[SerializeField]private GameObject diaBox;
[SerializeField] TMP_Text diaText;
[SerializeField] float textSpeed;
[SerializeField] Image speakerImage;//this is the game object that displays the image set above
[SerializeField] DialogueLine[] lines;
[SerializeField] bool hasTriggered = false;//fixes the issue of retriggering dialogue. Might need another solution if we want dialogues to repeat.
private int index;

[SerializeField]private PlayerInput cuan;
[SerializeField]private PlayerInput phiast;//ugly work around. Direct references to the player input components to turn them off when dialogue is happening. Need to find a better way

    void OnEnable()
    {

        if (hasTriggered)
        {
            diaBox.SetActive(false);
            return;
        }
         Cursor.visible = true;
         Cursor.lockState = CursorLockMode.None;
         diaText.text = string.Empty;
         diaBox.SetActive(true);
         StartDialogue();
    }

    void StartDialogue()
    {
        cuan.enabled = false;
        phiast.enabled = false;
        Debug.Log("Dialogue started");
        index = 0;
        UpdateSpeaker();
        StartCoroutine(TypeLine());
      
    }

    void UpdateSpeaker()
    {
        if (lines[index].charSprite != null)
        {
            speakerImage.sprite = lines[index].charSprite;
            speakerImage.enabled = true;
        }
        else
        {
            speakerImage.enabled = false;
        }
    }

    IEnumerator TypeLine()
    {
        foreach(char c in lines [index].text.ToCharArray())
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
            UpdateSpeaker();
            StartCoroutine(TypeLine());
        }
        else
        {
            hasTriggered = true;
            cuan.enabled = true;
            phiast.enabled = true;
            diaBox.SetActive(false);
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
}
