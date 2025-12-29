using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance { get; private set; }

    [SerializeField] private GameObject diaBox;
    [SerializeField] private TMP_Text diaText;
    [SerializeField] private Image speakerImage;
    [SerializeField] private float textSpeed = 0.05f;
    [SerializeField] private int voiceFreq = 3;
    [SerializeField] private float voicePitch = 1f;
    [SerializeField] private float pitchVariation = 0.1f;

    private AudioSource src;
    private Coroutine activeTypingCoroutine;
    private int currentIndex;
    private int characterCount;
    private PopUpDialogue.DialogueLine[] currentLines;
    private PopUpDialogue currentDialogueTrigger;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        src = GetComponent<AudioSource>();
        if (src == null)
        {
            src = gameObject.AddComponent<AudioSource>();
        }
    }

    public void StartDialogue(PopUpDialogue.DialogueLine[] lines, PopUpDialogue trigger)
    {
        // Stop any currently running dialogue
        if (activeTypingCoroutine != null)
        {
            StopCoroutine(activeTypingCoroutine);
        }

        currentDialogueTrigger = trigger;
        currentLines = lines;
        currentIndex = 0;

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        
        diaBox.SetActive(true);
        diaText.text = string.Empty;
        
        GameManager.Instance.DisableInput();
        
        UpdateSpeaker();
        activeTypingCoroutine = StartCoroutine(TypeLine());
    }

    void UpdateSpeaker()
    {
        if (currentLines[currentIndex].charSprite != null)
        {
            speakerImage.sprite = currentLines[currentIndex].charSprite;
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
        diaText.text = string.Empty;

        foreach (char c in currentLines[currentIndex].text.ToCharArray())
        {
            diaText.text += c;

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

        activeTypingCoroutine = null;
    }

    void PlayMumble()
    {
        if (src == null || currentLines[currentIndex].voice == null) return;

        src.pitch = voicePitch + Random.Range(-pitchVariation, pitchVariation);
        src.PlayOneShot(currentLines[currentIndex].voice);
    }

    public void NextLine()
    {
        if (activeTypingCoroutine != null)
        {
            StopCoroutine(activeTypingCoroutine);
            activeTypingCoroutine = null;
        }

        if (currentIndex < currentLines.Length - 1)
        {
            currentIndex++;
            diaText.text = string.Empty;
            UpdateSpeaker();
            activeTypingCoroutine = StartCoroutine(TypeLine());
        }
        else
        {
            EndDialogue();
        }
    }

    void EndDialogue()
    {
        if (currentDialogueTrigger != null)
        {
            currentDialogueTrigger.MarkAsTriggered();
        }

        GameManager.Instance.EnableInput();
        diaBox.SetActive(false);
        
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        currentLines = null;
        currentDialogueTrigger = null;
    }
}