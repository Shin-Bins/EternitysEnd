using UnityEngine;

public class CharacterManager : MonoBehaviour
{

    public static CharacterManager Instance { get; private set; }
    public bool cuanHeld = false;

      private void Awake()
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

    public void HandleHolding()
    {
        if(!cuanHeld)
        {
            cuanHeld = true;
        }
        else{
            cuanHeld = false;
        }
    }
}
