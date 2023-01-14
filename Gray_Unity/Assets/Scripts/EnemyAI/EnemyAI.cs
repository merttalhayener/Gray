using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    NavMeshAgent agent;
    public Animator animator;

    public GameObject player;
    public GameObject startRayPosition;
    public GameObject swatHead;

    public Transform[] waypoints;
    public Vector3 target;
    public Vector3 playerDirection;
  
    public LayerMask playerLayer;
    int waypointIndex;

    public bool alerted;




    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        UpdateDestination();
        alerted = false;
        
        
    }

    private void Update()
    {
        AgentEmpty();
        IsAlerted();
      
       
        //Player ve swat arasýndaki yönün belirlenmesi.
        playerDirection = player.transform.position - this.transform.position;
       
       

        animator.SetFloat("Speed", agent.velocity.magnitude);
       
        if (Vector3.Distance(transform.position, target) < 2f)
        {
            
            IterateWaypointIndex();
            UpdateDestination();
        }
    }

    void UpdateDestination()
    {
        target = waypoints[waypointIndex].position;
        agent.SetDestination(target);
    }

   
   void AgentEmpty() 
    {
        if (agent.hasPath == false)
        {
            target = waypoints[waypointIndex].position;
            agent.SetDestination(target);
        }
    } 

    void IterateWaypointIndex()
    {
        agent.stoppingDistance = 0f;
        waypointIndex++;
        if (waypointIndex == waypoints.Length)
        {
            waypointIndex = 0;
        }
    }
    
    void ChasePlayer()
    {
        alerted = true;
        swatHead.transform.Rotate(playerDirection);
        target = player.transform.position;
        // agent.SetDestination(player.transform.position);
        agent.SetDestination(target);
    }

    void IsAlerted()
    {
        if (alerted == true)
        {
            Debug.Log("Alerted");
        }
    }

    private void OnTriggerStay(Collider other)
    {
        //Swat Player'a ray yolluyor.
        RaycastHit hit;
        Physics.Raycast(startRayPosition.transform.position, playerDirection, out hit, Mathf.Infinity, playerLayer);
        Debug.DrawRay(startRayPosition.transform.position, playerDirection * hit.distance, Color.red);


        Debug.Log(hit.collider.tag);


        if (other.gameObject.tag == "Player")
        {
           if(hit.collider.tag == "Player")
            {
              Debug.Log("Player görüþ menzilinde");
                Debug.DrawRay(this.transform.position, playerDirection * hit.distance, Color.blue);
                ChasePlayer();
                
            }
        }
    }
    

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Player" )
        {
            alerted = false;
            Debug.Log("Get back to patroling");
        }
    }
}
