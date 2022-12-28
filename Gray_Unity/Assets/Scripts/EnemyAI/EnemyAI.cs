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
    float timer = 0f;

    int waypointIndex;
    public Animator animator;
    public Transform[] waypoints;
    NavMeshAgent agent;
    public Vector3 target;



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

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            agent.SetDestination(playerTransform.position);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            target = waypoints[waypointIndex].position;
            agent.SetDestination(target);
        }
    }
}
