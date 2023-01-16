using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class EnemyTakedDown : MonoBehaviour
{
    public Transform player;
    public Transform killPosition;
    public Animator animator;
    private NavMeshAgent agent;

    public bool isAlive = true;
    public bool takedDown = false;
    public float health;

    public void Start()
    {
        health = 100f;
        agent = GetComponent<NavMeshAgent>();
        
    }

    public void Update()
    {
        if (isAlive == false)
        {
            agent.enabled = false;
            this.gameObject.GetComponent<CapsuleCollider>().enabled = false;
        }
       isHeAlive();
       Debug.Log(health);
    }

    public void GetStealthKilled()
    {

        transform.rotation = player.rotation;
        transform.position = killPosition.position;
        animator.SetTrigger("TakedDown");
        isAlive = false;

    }

    private void isHeAlive()
    {
        if (health < 100)
        {
            isAlive = false;
        }
        else
        {
            isAlive = true;
        }

        if (isAlive == false)
        {
            animator.SetBool("isHeAlive", false);
            //Debug.Log("Öldü");
        }
        else
        {
            animator.SetBool("isHeAlive", true);
            //Debug.Log("Yaþýyor");
        }
       
    }
   


}
