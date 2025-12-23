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
        public AudioClip voice;
    }

[SerializeField]private GameObject diaBox;
[SerializeField] TMP_Text diaText;
[SerializeField] float textSpeed;
[SerializeField] DialogueLine[] lines;

private int index;

[SerializeField] Image speakerImage;//this is the game object that displays the image set above
private AudioSource src;
[SerializeField] float voicePitch = 1f;
[SerializeField]float pitchVariation = 0.1f;//changes the pitch to vary the line
[SerializeField] int voiceFreq = 3; //after how many characters the voice line plays again
private int characterCount = 0;

[SerializeField] bool hasTriggered = false;//fixes the issue of retriggering dialogue. Might need another solution if we want dialogues to repeat.

    void OnEnable()
    {

        if (hasTriggered)
        {
            diaBox.SetActive(false);
            return;
        }
         Cursor.visible = true;
         Cursor.lockState = CursorLockMode.None;
         src = GetComponent<AudioSource>();
         diaText.text = string.Empty;
         diaBox.SetActive(true);
         StartDialogue();
    }

    void StartDialogue()
    {
       GameManager.Instance.DisableInput();//Function in the game manager that disables inputs
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
        characterCount = 0;
        foreach(char c in lines [index].text.ToCharArray())
        {
           diaText.text += c;
          
               // skips spaces in text
            if (c != ' ' && characterCount % voiceFreq == 0)
            {
                PlayMumble();
            }
            
            if (c != ' ')
            {
                characterCount++;
            }
           yield return new WaitForSeconds(textSpeed);
        }
    }

     void PlayMumble()
    {

       if (src == null) return;
        
        // Only play if this line has a mumble sound assigned
        if (lines[index].voice != null)
        {
            src.pitch = voicePitch + Random.Range(-pitchVariation, pitchVariation);
            src.PlayOneShot(lines[index].voice);
        }
    }

    public void NextLine()
    {
        if(index < lines.Length - 1)
        {
            index ++;
            diaText.text = string.Empty;
            Debug.Log("NextLine");
            UpdateSpeaker();
            StartCoroutine(TypeLine());
        }
        else
        {
            hasTriggered = true;
            GameManager.Instance.EnableInput();//reenables inputs
            diaBox.SetActive(false);
            Debug.Log("Finito");
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
}
