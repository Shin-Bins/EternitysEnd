using UnityEngine;

public class LookAtPlayer : MonoBehaviour
{
    public Transform phist;
    private float rotSpeed = 10f;

    void Update()
    {
        Vector3 looker = phist.transform.position - transform.position;
        looker.y = 0;

        if (looker != null)
        {
            Quaternion targetRotation = Quaternion.LookRotation(looker);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotSpeed);
        }
    }
}
