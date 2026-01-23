using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class pause_menu : MonoBehaviour
{
    Scene thisscene;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
         thisscene = SceneManager.GetActiveScene();
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
}
