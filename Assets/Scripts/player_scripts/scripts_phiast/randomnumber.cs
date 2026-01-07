using UnityEngine;

public class randomnumber : MonoBehaviour, IIinteractable
{
    public void Interaction()
    {
        Debug.Log(Random.Range(0, 100));
    }
}
