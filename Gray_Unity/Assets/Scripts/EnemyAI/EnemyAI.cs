using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    NavMeshAgent agent;
    public Transform[] waypoints;
    int waypointIndex;
    public Vector3 target;
    public Animator animator;



    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        UpdateDestination();
    }

    private void Update()
    {
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

    void IterateWaypointIndex()
    {
        waypointIndex++;
        if (waypointIndex == waypoints.Length)
        {
            waypointIndex = 0;
        }
    }









}
