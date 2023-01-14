using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    NavMeshAgent agent;
    public Animator animator;
   
    public GameObject player;

    public Transform[] waypoints;
    public Vector3 target;
    public Vector3 playerDirection;
  
   

    public LayerMask playerLayer;
    int waypointIndex;


    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        UpdateDestination();
        
    }

    private void Update()
    {
      
        //Player'a doðru bir ray oluþturuyoruz.
        playerDirection =player.transform.position - this.transform.position;
       
       
        // RaycastHit hit;
        // Physics.Raycast(this.transform.position, playerDirection, out hit, Mathf.Infinity);

        // Debug.DrawRay(this.transform.position, playerDirection * hit.distance, Color.red);


        AgentEmpty();

        animator.SetFloat("Speed", agent.velocity.magnitude);
       
        if (Vector3.Distance(transform.position, target) < 2)
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
        waypointIndex++;
        if (waypointIndex == waypoints.Length)
        {
            waypointIndex = 0;
        }
    }
    
    void ChasePlayer()
    {
        agent.SetDestination(player.transform.position);
       
    }

    private void OnTriggerStay(Collider other)
    {
        //Swat Player'a ray yolluyor.
        RaycastHit hit;
        Physics.Raycast(this.transform.position, playerDirection, out hit, Mathf.Infinity, playerLayer);
        Debug.DrawRay(this.transform.position, playerDirection * hit.distance, Color.red);


        Debug.Log(hit.collider.tag);


        if (other.gameObject.tag == "Player")
        {
           
              
              ChasePlayer();
            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            target = waypoints[waypointIndex].position;
            Debug.Log("Player görüþ alanýndan çýktý");
            
        }
    }

}
