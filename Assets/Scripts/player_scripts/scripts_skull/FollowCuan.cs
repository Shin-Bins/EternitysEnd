using UnityEngine;

public class FollowCuan : MonoBehaviour
{
    private GameObject cuan;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cuan = GameObject.Find("StCuan (1)");
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = cuan.transform.position;
    }
}
