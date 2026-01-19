using UnityEngine;

public class BossStats : MonoBehaviour
{
    [Header("Health")]
    private int maxHealth = 5;
    public int currentHealth;
    public bool canBeHurt = true;
    public bool isHurt = false;
    public float invinceFrames = 3f;

    [Header("Attack")]
    public float attackDelay = 10f;//how long in between attacks he doesn't do anything'
    public float holdAttack = 3f;//how long he holds his attackDelay

    void Start()
    {
        currentHealth = maxHealth;
    }

    void Damaged()
    {

    }
}
