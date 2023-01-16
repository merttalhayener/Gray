using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public float enemyHealth = 100f;
    public bool enemyIsAlive = true;

    public Animator enemyAnimator;
    private EnemyAI enemyAI;
    

    private void Awake()
    {
        enemyIsAlive = true;
        enemyAI = GetComponent<EnemyAI>();
       
    }

    void Update()
    {

        enemyAnimator.SetBool("isEnemyAlive", enemyIsAlive);
        EnemyHealthManager();
    }

    public void EnemyHealthManager()
    {
        if (enemyHealth <= 0)
        {
            enemyAI.enabled = false;
            enemyHealth = 0;
            enemyIsAlive = false;
        }

        if (enemyHealth > 0)
        {
            enemyIsAlive = true;
        }
    }

    public void TakeDamage()
    {
        enemyHealth -= 50;
    }
}
