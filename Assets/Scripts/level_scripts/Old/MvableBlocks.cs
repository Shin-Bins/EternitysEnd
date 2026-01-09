using UnityEngine;
using System.Collections;

public class MvableBlocks : MonoBehaviour
{
   [Header("Grid Settings")]
    [SerializeField] private float gridSize = 1f;
    [SerializeField] private float pushSpeed = 3f;
    
    [Header("Collision Detection")]
    [SerializeField] private float checkRadius = 0.4f;
    
    private bool isMoving = false;
    private Vector3 targetPosition;

    void Start()
    {
        // Snap to grid on start
        transform.position = GetGridPosition(transform.position);
        targetPosition = transform.position;
    }

    public bool TryPush(Vector3 direction)
    {
        if (isMoving) return false;

        // Normalize to cardinal directions
        direction = GetCardinalDirection(direction);
        
        // Calculate target position
        Vector3 nextPos = transform.position + direction * gridSize;
        
        if (IsBlocked(nextPos, direction))
            return false;

        StartCoroutine(PushToPosition(nextPos));
        return true;
    }

    private IEnumerator PushToPosition(Vector3 target)
    {
        isMoving = true;
        targetPosition = target;
        
        Vector3 startPos = transform.position;
        float elapsed = 0f;
        float duration = gridSize / pushSpeed;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / duration;
            
            transform.position = Vector3.Lerp(startPos, target, t);
            
            yield return null;
        }

        transform.position = target;
        isMoving = false;
    }

    private bool IsBlocked(Vector3 position, Vector3 direction)
    {
         Collider[] colliders = Physics.OverlapSphere(position, checkRadius);
        
        foreach (Collider col in colliders)
        {
            // Ignore self
            if (col.gameObject == gameObject)
                continue;
            
            // Ignore triggers
            if (col.isTrigger)
                continue;
                
            // If we found any solid collider, position is blocked
            return true;
        }

        return false;
    }

    private Vector3 GetCardinalDirection(Vector3 dir)
    {
        // Convert any direction to nearest cardinal direction
        dir.y = 0;
        dir.Normalize();

        if (Mathf.Abs(dir.x) > Mathf.Abs(dir.z))
            return new Vector3(Mathf.Sign(dir.x), 0, 0);
        else
            return new Vector3(0, 0, Mathf.Sign(dir.z));
    }

    private Vector3 GetGridPosition(Vector3 worldPos)
    {
        return new Vector3(
            Mathf.Round(worldPos.x / gridSize) * gridSize,
            worldPos.y,
            Mathf.Round(worldPos.z / gridSize) * gridSize
        );
    }

    public bool IsMoving() => isMoving;
}