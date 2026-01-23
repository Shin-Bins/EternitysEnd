using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.InputSystem; 
using UnityEngine.AI;
using System.Collections;
using UnityEngine.SceneManagement;

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

    public GameObject playerPrefab;
    private PlayerInput[] inputs;
    private NavMeshAgent[] agents;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
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

     void Start()
    {
        RefreshReferences();
    }
    
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        RefreshReferences();
    }
    
    void RefreshReferences()
    {
        //playerPrefab = GameObject.Find("Player");
        
        if (playerPrefab != null)
        {
            inputs = playerPrefab.GetComponentsInChildren<PlayerInput>();
        }
        else
        {
            inputs = new PlayerInput[0];
            Debug.LogWarning("Player not found in scene");
        }
        
        agents = FindObjectsByType<NavMeshAgent>(FindObjectsSortMode.None);
    }
     public void DisableInput()
    {
        if (inputs == null || inputs.Length == 0)
        {
            RefreshReferences();
        }
        
        foreach(var input in inputs)
        {
            if (input != null)
            {
                input.enabled = false;
            }
        }
        
        if(agents != null && agents.Length != 0)
        {
            foreach(var agent in agents)
            {
                if (agent != null)
                {
                    agent.isStopped = true;
                }
            }
        }
    }
    
    public void EnableInput()
    {
        if (inputs == null || inputs.Length == 0)
        {
            RefreshReferences();
        }
        
        foreach(var input in inputs)
        {
            if (input != null)
            {
                input.enabled = true;
            }
        }
        
        if(agents != null && agents.Length != 0)
        {
            foreach(var agent in agents)
            {
                if (agent != null)
                {
                    agent.isStopped = false;
                }
            }
        }
    }
    
    public void StartDialogue(PopUpDialogue.DialogueLine[] lines, PopUpDialogue trigger)
    {

     if (lines == null || lines.Length == 0)
        {
            Debug.LogError("Cannot start dialogue: lines array is null or empty!");
            return;
        }

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
        
        DisableInput();
        
        UpdateSpeaker();
        activeTypingCoroutine = StartCoroutine(TypeLine());
    }

    void UpdateSpeaker()
    {

          // Safety checks to prevent index out of bounds errors
        if (currentLines == null || currentLines.Length == 0)
        {
            Debug.LogWarning("CurrentLines is null or empty!");
            speakerImage.enabled = false;
            return;
        }
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
         if (src == null) return;
        AudioClip[] voice = currentLines[currentIndex].voice;
        if (voice == null || voice.Length == 0) return;
        
        // Randomly select mumble clip
        AudioClip selectedClip = voice[Random.Range(0, voice.Length)];
        
        if (selectedClip != null)
        {
            src.pitch = voicePitch + Random.Range(-pitchVariation, pitchVariation);
            src.PlayOneShot(selectedClip);
        }
    }

    public void NextLine()
    {
    // if text is typing, finish typing
        if (activeTypingCoroutine != null)
        {
            StopCoroutine(activeTypingCoroutine);
            activeTypingCoroutine = null;
            
            // Display the full text immediately
            diaText.text = currentLines[currentIndex].text;
            return;
        }

        // if the text is finished typing, go to the next line
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

        EnableInput();
        diaBox.SetActive(false);
        
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        currentLines = null;
        currentDialogueTrigger = null;
    }
}