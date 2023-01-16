using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StealthKillDetector : MonoBehaviour
{
    public bool canBeStealthKilled;
    public bool inStealth;
    public bool underKillAnimation;
    public bool enemyAlive;

    public GameObject player;
    public Animator _animator;
    public Transform killPosition;
    public GameObject enemy;
    public EnemyTakedDown enemyTakedDown;
    public ThirdPersonController thirdPersonController;
    
   
    

    void Update()
    {
        enemy.GetComponent<EnemyTakedDown>();
        enemyTakedDown = enemy.GetComponent<EnemyTakedDown>();
        enemyAlive = enemyTakedDown.isAlive;
        _animator.SetBool("isEnemyAlive", enemyAlive);

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Enemy" )
        {
            enemy = other.gameObject;
            StealthKill();
        }

        
    
    }

    private void OnTriggerExit(Collider other)
    {
        enemy = null;
    }

    public void StealthKill()
    {
        if (Input.GetKey("t") && enemyAlive==true )
        {
            inStealth = false;
           
            _animator.SetBool("Crouch", false);
            _animator.SetTrigger("StealthKill");
            underKillAnimation = true;
            enemyTakedDown.GetStealthKilled();
        }
    }




}
