using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet9mm : MonoBehaviour
{
    private Rigidbody bulletRigidbody;
    private EnemyTarget enemyTarget;
   
    private void Awake()
    {
        bulletRigidbody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        float speed = 100f;
        bulletRigidbody.velocity = transform.forward * speed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<EnemyTarget>()!=null)
        {
            enemyTarget = other.GetComponent<EnemyTarget>();
            enemyTarget.TakeDamage();
            Debug.Log("DüþmanýVurdun");
            Destroy(this.gameObject);
        }
        else
        {
            Debug.Log(" Iskaladýn");
            //Miss
        }
       
    }
}
