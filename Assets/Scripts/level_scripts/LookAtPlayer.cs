using UnityEngine;

public class LookAtPlayer : MonoBehaviour
{
    public GameObject phist;
    private float rotSpeed = 10f;

    void Start()
    {
        phist = GameObject.Find("Phiast_NLA");
    }
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
