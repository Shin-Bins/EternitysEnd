using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem; 
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
   public static GameManager Instance { get; private set; }
    
    private Vector3 currentCheckpoint = Vector3.zero;

    [Header("Level Loading")]
    [SerializeField] private Image fadeOut;
    [SerializeField] private float fadeDuration = 0.5f;

    private GameObject phiast;
    private GameObject skull;

    public int nextsceneload;
 
     void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

    }

    void Start()
    {
         phiast = GameObject.Find("Phiast_NLA");
         skull = GameObject.Find("StCuan (1)");
    }

    public void SetCheckpoint(Vector3 position)
    {
        currentCheckpoint = position;
        Debug.Log("Checkpoint set");
    }
    
    public void RespawnCheckpoint()
    {
        if(currentCheckpoint != Vector3.zero)
        {
            /*CharacterController phiastChar = phiast.GetComponent<CharacterController>();
            Rigidbody skullRB = skull.GetComponent<Rigidbody>();

            phiastChar.enabled = false;
            if (skullRB != null) 
            skullRB.isKinematic = true;

            phiast.transform.position = currentCheckpoint;
            skull.transform.position = currentCheckpoint;
            Physics.SyncTransforms();

           // if (skullRB != null) 
            //skullRB.isKinematic = false;
            phiastChar.enabled = true;
            Debug.Log("nodie");*/
            StartCoroutine(RingoRoadagain());

        }
        else
        {
            Death();
        }
    } 

    IEnumerator RingoRoadagain()
    {
        CharacterController phiastChar = phiast.GetComponent<CharacterController>();
        Rigidbody skullRB = skull.GetComponent<Rigidbody>();
        

        phiastChar.enabled = false;
        skullRB.isKinematic = true;

        skullRB.linearVelocity = Vector3.zero;
        skullRB.angularVelocity = Vector3.zero;

        phiast.transform.position = currentCheckpoint;
        skull.transform.position = currentCheckpoint;

        yield return null;

        skullRB.isKinematic = false;
        phiastChar.enabled = true;
    }
   
    public void Death()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LoadLevelWithFade(string sceneName)
    {
        StartCoroutine(FadeTransition(sceneName));
    }
    
    public void LoadRegionWithLoadingScreen(string sceneName)
    {
        StartCoroutine(LoadWithFullScreen(sceneName));
    }
    
    private IEnumerator FadeTransition(string sceneName)
    {
        // Fade to black
        yield return StartCoroutine(Fade(1f));
        
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        
        // Wait until scene is loaded
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        yield return new WaitForSeconds(0.2f); 
        // Fade back in
        yield return StartCoroutine(Fade(0f));
    }
    
    private IEnumerator LoadWithFullScreen(string targetScene)
    {
        yield return StartCoroutine(Fade(1f));
        
        SceneManager.LoadScene("LoadingScreen");
    }
    
    private IEnumerator Fade(float targetFill)
    {
        float startFill = fadeOut.fillAmount;
        float elapsed = 0f;
        
        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            fadeOut.fillAmount = Mathf.Lerp(startFill, targetFill, elapsed / fadeDuration);
            yield return null;
        }
        fadeOut.fillAmount = targetFill;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}