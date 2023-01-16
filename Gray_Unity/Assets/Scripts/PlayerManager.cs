using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    
   
    public float playerHealth=100f;
    public bool playerIsAlive;
   
    void Update()
    {
        Debug.Log(playerHealth);
        PlayerHealthManager();
    }
    private void Awake()
    {
        playerIsAlive = true;
        playerHealth = 10000f;
    }

    void PlayerHealthManager()
    {
        if (playerHealth <= 0)
        {
            playerHealth = 0;
            playerIsAlive = false;
        }

        if(playerHealth > 0)
        {
            playerIsAlive = true;
        }
    }

    public void TakeDamage()
    {
        playerHealth -= 25;
    }
}
