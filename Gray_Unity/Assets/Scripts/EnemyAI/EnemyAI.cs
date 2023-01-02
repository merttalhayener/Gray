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
    public float eyeRange = 5f;

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
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out RaycastHit hit›nfo, 15f))
        {
                Debug.Log("Hit Player");
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit›nfo.distance, Color.red);
        }

        else
        {
            Debug.Log("Hit Nothing");
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit›nfo.distance, Color.green);
        }

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
    
    void ChasePlayer()
    {
        agent.SetDestination(playerTransform.position);
    }

    private void OnTriggerStay(Collider other)
    {
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
            ChasePlayer();
        }
    }

}
