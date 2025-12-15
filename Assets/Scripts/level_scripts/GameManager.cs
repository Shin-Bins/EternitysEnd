using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem; 
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    //Chckpoints
    private Vector3 currentCheckpoint = Vector3.zero;

    //Dialogue
    private GameObject playerPrefab;
    private PlayerInput[] inputs;
    private NavMeshAgent[] agents;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    void Start()
    {
       playerPrefab = GameObject.Find("Player");
       inputs = playerPrefab.GetComponentsInChildren<PlayerInput>();
       agents = FindObjectsByType<NavMeshAgent>(FindObjectsSortMode.None);

       if(playerPrefab == null)
        {
            Debug.Log("Where are mah boahs");
        }
        if(inputs.Length == 0)
        {
            Debug.Log("No inputto");
        }
    }

 public void SetCheckpoint(Vector3 position)
 {
     currentCheckpoint = position;
     Debug.Log("Checkpoint set");
 }

 public void RespawnCheckpoint(GameObject charToRespawn)
 {
     if(currentCheckpoint != Vector3.zero)
     {
         charToRespawn.transform.position = currentCheckpoint;
     }
 }

 public void DisableInput()
 {
     foreach(var input in inputs)
     {
         input.enabled = false;
     }

     if(agents.Length != 0)
     {
         foreach(var agent in agents)
         {
             agent.enabled = false;
         }
     }
 }

 public void EnableInput()
 {
     foreach(var input in inputs)
     {
         input.enabled = true;
     }

     if(agents.Length != 0)
     {
         foreach(var agent in agents)
         {
             agent.enabled = false;
         }
     }
 }
 public void Death()
 {
     SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
 }
}
