using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem; 
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
   public static GameManager Instance { get; private set; }
    
    private Vector3 currentCheckpoint = Vector3.zero;
    private GameObject playerPrefab;
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
        }
    }
    
    void OnDestroy()
    {
        if (Instance == this)
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
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
        playerPrefab = GameObject.Find("Player");
        
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
        else
        {
            Death();
        }
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
                    agent.enabled = false;
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
                    agent.enabled = true;
                }
            }
        }
    }
    
    public void Death()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}