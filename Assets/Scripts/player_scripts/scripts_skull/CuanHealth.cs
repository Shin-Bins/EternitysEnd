using UnityEngine;
using UnityEngine.UI;


public class CuanHealth : MonoBehaviour
{

    [Header("SluaghDeath")]
    public bool isHeld = false;
    public Image cuanHeldDeath;
    private float deathTimer;
    private float timeToKill = 8f;
    private float currentAlpha = 0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        deathTimer = 0f;
        Color color = cuanHeldDeath.color;
        color.a = 0f;
        cuanHeldDeath.color = color;
    }

    // Update is called once per frame
    void Update()
    {
        if(isHeld)
        {
            deathTimer += Time.deltaTime;
            currentAlpha = Mathf.MoveTowards(currentAlpha, 1f, 0.1f * Time.deltaTime);
        }
        else{
            currentAlpha = Mathf.MoveTowards(currentAlpha, 0f, 0.2f * Time.deltaTime);
        }
        Color color = cuanHeldDeath.color;
        color.a = currentAlpha;
        cuanHeldDeath.color = color;


        if(deathTimer >= timeToKill)
        {
            GameManager.Instance.Death();
        }
    }
}
