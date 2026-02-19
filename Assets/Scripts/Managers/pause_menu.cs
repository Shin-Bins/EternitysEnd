using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class pause_menu : MonoBehaviour
{
    Scene thisscene;
    GameObject phi;
    GameObject skl;
    GameObject resetpoint;
    public static pause_menu Instance { get; private set; }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
         thisscene = SceneManager.GetActiveScene();
        phi = GameObject.FindGameObjectWithTag("phiast");
        skl = GameObject.FindGameObjectWithTag("skull");
        resetpoint = GameObject.FindGameObjectWithTag("reset");
        DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

   

    public void unpause()
    {
        Time.timeScale = 1;
    }

    public void restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
    }

    public void quit()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void deathrestart()
    {
        GameManager.Instance.RespawnCheckpoint(phi.gameObject);
        phi.GetComponent<PhiastHealth>().respwanfromdeath();
        

        
    }

    private void OnSceneLoaded(Scene thisscene, LoadSceneMode mode)
    {
        if (thisscene.name == "MainMenu") 
        {
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
