using StarterAssets;
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
    

    public GameObject playerRaycast;
    public GameObject player;
    public GameObject startRayPosition;
    public GameObject swatHead;
  
    public SimpleShoot enemyGunShot;

    public Transform[] waypoints;
    public Vector3 target;
    public Vector3 playerDirection;
  
    public LayerMask visibleLayer;
    int waypointIndex;

    public bool alerted;
    public bool sawPlayer;
    public float attackRange;
    public bool playerInAttackRange;
    public LayerMask whatIsPlayer;

    
    public PlayerManager playerManager;
    

    public bool inAttackMode;
    
    
    
    
 



    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        UpdateDestination();
    }



    private void Update()
    {
        animator.SetBool("InAttackMode", inAttackMode);
        animator.SetBool("Alerted", alerted);
        animator.SetBool("SawPlayer", sawPlayer);
        animator.SetFloat("Speed", agent.velocity.magnitude);

        StopController();
        AgentEmpty();
        isAllerted();
        AttackMode();
        Shoot();
        

        




        
       


        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);
      
       
        //Player ve swat arasýndaki yönün belirlenmesi.
        playerDirection = playerRaycast.transform.position - this.transform.position;


        Debug.DrawRay(startRayPosition.transform.position, playerDirection *10f, Color.red);

       
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
        
        waypointIndex++;
        if (waypointIndex == waypoints.Length)
        {
            waypointIndex = 0;
        }
    }
    
    void ChasePlayer()
    {
        agent.stoppingDistance = attackRange;
        swatHead.transform.Rotate(playerDirection);
        target = playerRaycast.transform.position;
        // agent.SetDestination(player.transform.position);
        agent.SetDestination(target);
        
    }
    
    void AttackMode()
    {
        if(alerted==true && playerInAttackRange ==true && sawPlayer == true )
        {
            
            inAttackMode = true;
        }
        else
        {
            
            inAttackMode = false;
           
        }
    }

    void Shoot()
    {
        if (inAttackMode == true )
        {
            agent.stoppingDistance = attackRange;
            agent.transform.LookAt(player.transform);
            agent.isStopped = true;
            animator.SetTrigger("Shoot!");
           
        }
    }

    void PullTheTrigger()
    {
        enemyGunShot.Shoot();
        enemyGunShot.CasingRelease();
    }

    void StopController()
    {
        if(agent.isStopped && animator.GetCurrentAnimatorStateInfo(0).IsName("Shooting"))
        {
            agent.isStopped = false;
        }
        else if(animator.GetCurrentAnimatorStateInfo(0).IsName("Shooting"))
        {
            agent.isStopped = true;
        }
    }

    void isAllerted()
    {
        if (sawPlayer == false && alerted)
        {
            agent.stoppingDistance = 4f;
            agent.isStopped = false;
            StartCoroutine(WaitForUnAlert());
        }

        if (alerted == true)
        {
            agent.speed = 2f;
        }

        if (alerted == false)
        {
            agent.stoppingDistance = 1f;
            agent.speed = 2f;
        }
    }
   

    private void OnTriggerStay(Collider other)
    {
        //Swat Player'a ray yolluyor.
        RaycastHit hit;
        Physics.Raycast(startRayPosition.transform.position, playerDirection, out hit, Mathf.Infinity, visibleLayer);
        


        if (other.gameObject.tag == "Player")
        {
           if(hit.collider.tag == "Player")
            {
                sawPlayer = true;
                alerted = true;
                Debug.DrawRay(startRayPosition.transform.position, playerDirection * 25f, Color.green);
                ChasePlayer();
            }
        }

       else
        {
            if(hit.collider.tag!= "Player")
            {
                sawPlayer = false;
            }
        }
    }


    IEnumerator WaitForUnAlert()
    {
        yield return new WaitForSeconds(10f);
        //10 saniye geçti.
        alerted = false;
    }
}
