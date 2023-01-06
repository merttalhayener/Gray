using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public Transform playerTransform;
    public float maxTime = 1f;
    public float maxDistance = 1f;
    public float eyeRange = 5f;

    int waypointIndex;
    public Animator animator;
    public Transform[] waypoints;
    NavMeshAgent agent;
    public Vector3 target;
   
    public LayerMask playerLayer;



    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        UpdateDestination();
    }

    private void Update()
    {
        AgentEmpty();
        
        animator.SetFloat("Speed", agent.velocity.magnitude);
       
        if (Vector3.Distance(transform.position, target) < 1)
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
        agent.SetDestination(playerTransform.position);
    }

    private void OnTriggerStay(Collider other)
    {
        RaycastHit hit;

        if (other.gameObject.tag == "Player")
        {
            if (Physics.Raycast(this.transform.position, transform.TransformDirection(Vector3.forward), out hit, playerLayer))
            {
                Debug.DrawRay(this.transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.blue);
                Debug.Log(hit.collider.name);
                ChasePlayer();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            target = waypoints[waypointIndex].position;
            ChasePlayer();
        }
    }

}
