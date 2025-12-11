using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private Vector3 currentCheckpoint = Vector3.zero;
    private GameObject phiast;
    private GameObject cuan;

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
        cuan = GameObject.Find("skull");
        phiast = GameObject.Find("Phiast-tank 1");
        if(cuan == null || phiast == null)
        {
            Debug.Log("Where are mah boahs");
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

 public void Death()
 {
     SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
 }
}
