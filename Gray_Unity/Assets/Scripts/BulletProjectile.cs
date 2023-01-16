using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletProjectile : MonoBehaviour
{
    private Rigidbody bulletRigidbody;
    public PlayerManager playerManager;
    public EnemyManager enemyManager;
    public GameObject enemy;
    public GameObject player;


    private void Awake()
    {
        bulletRigidbody = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
        playerManager = player.GetComponent<PlayerManager>();

    }

    private void Start()
    {
      
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.GetComponent<BulletTarget>() != null )
        {
            Debug.Log("PLAYER VURULDU");
            playerManager.TakeDamage();
            Destroy(this.gameObject);
        }
    }
}
