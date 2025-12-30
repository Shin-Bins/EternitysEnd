using UnityEngine;
using UnityEngine.InputSystem;

public class PushAndPull : MonoBehaviour
{
       [SerializeField] private float pushDistance = 1.5f;

       void OnInteract()
       {
           Push();
       }
    
    private void Push()
    {
        // Get player's forward direction
        Vector3 pushDir = transform.forward;
        pushDir.y = 0;
        pushDir.Normalize();

        // Raycast to find pushable blocks
        if (Physics.Raycast(transform.position + Vector3.up * 0.5f, pushDir, out RaycastHit hit, pushDistance))
        {
            MvableBlocks block = hit.collider.GetComponent<MvableBlocks>();
            if (block != null)
            {
                block.TryPush(pushDir);
            }
        }
    }
}