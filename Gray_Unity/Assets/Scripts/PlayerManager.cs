using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    
   
    public float playerHealth=100f;
    public bool playerIsAlive;
    public Animator playerAnimator;
    public ThirdPersonController tpController;
    
   
    void Update()
    {
        Debug.Log(playerHealth);
        PlayerHealthManager();
       
    }
    private void Awake()
    {
        playerIsAlive = true;
        playerHealth = 100f;
        playerAnimator.SetBool("PlayerIsAlive", playerIsAlive);

    }

    void PlayerHealthManager()
    {
        if (playerHealth <= 0)
        {
            playerHealth = 0;
            playerIsAlive = false;
            tpController.enabled = false;
            playerAnimator.SetBool("PlayerIsAlive", playerIsAlive);
            
        }

        if(playerHealth > 0)
        {
            playerIsAlive = true;
            playerAnimator.SetBool("PlayerIsAlive", playerIsAlive);
        }
    }

    public void TakeDamage()
    {
        playerHealth -=50;
    }
}
