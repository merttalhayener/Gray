using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using StarterAssets;
using UnityEngine.InputSystem;

// Bu scriptte aim sistemini ve ateþ etme sistemini yapacaðýz.
public class ThirdPersonAimController : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera aimVirtualCamera;
    [SerializeField] private float normalSensitivity;
    [SerializeField] private float aimSensitivity;
    [SerializeField] private LayerMask aimColliderMask = new LayerMask();
    [SerializeField] private Transform debugTransform;
    [SerializeField] private Transform pfBulletProjectile;
    [SerializeField] private Transform spawnBulletPosition;
    [SerializeField] AudioClip gunFire;

    private GameObject targetEnemy;
    private EnemyTakedDown enemyScript;

    AudioSource audioSource;


    private ThirdPersonController thirdPersonController;
    private StarterAssetsInputs starterAssestInputs;
    private Animator animator;

    public SimpleShoot playerGunShot;

    private void Awake()
    {
        thirdPersonController = GetComponent<ThirdPersonController>();
        starterAssestInputs = GetComponent<StarterAssetsInputs>();
        animator = GetComponent<Animator>();
        enemyScript = GetComponent<EnemyTakedDown>();
        audioSource = GetComponent<AudioSource>();

    }

    private void Update()
    {
       
        //Aim codes
        Vector3 mouseWorldPosition = Vector3.zero;
        Vector2 screenCenterPoint = new Vector2(Screen.width / 2f, Screen.height / 2f);//Ekranýn ortasýný alýyoruz
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        Transform hitTransform = null;
        
        if (Physics.Raycast(ray, out RaycastHit raycastHit, 999f, aimColliderMask))
        {
            debugTransform.position = raycastHit.point;
            mouseWorldPosition = raycastHit.point;
            hitTransform = raycastHit.transform;
        }

        if (starterAssestInputs.aim)
        {
            aimVirtualCamera.gameObject.SetActive(true);
            thirdPersonController.SetSensitivity(aimSensitivity);
            thirdPersonController.SetRotateOnMove(false);
            animator.SetLayerWeight(1, Mathf.Lerp(animator.GetLayerWeight(1), 1f, Time.deltaTime * 10f));

            Vector3 worldAimTarget = mouseWorldPosition;
            worldAimTarget.y = transform.position.y;
            Vector3 aimDirection = (worldAimTarget - transform.position).normalized;

            transform.forward = Vector3.Lerp(transform.forward, aimDirection, Time.deltaTime * 20f);
        }
        else
        {
            aimVirtualCamera.gameObject.SetActive(false);
            thirdPersonController.SetSensitivity(normalSensitivity);
            thirdPersonController.SetRotateOnMove(true);
            animator.SetLayerWeight(1, Mathf.Lerp(animator.GetLayerWeight(1), 0f, Time.deltaTime * 10f));
        }
        Debug.Log(hitTransform);

        if (starterAssestInputs.shoot)
        {
            Vector3 aimDirection = (mouseWorldPosition - spawnBulletPosition.position).normalized;
            Instantiate(pfBulletProjectile, spawnBulletPosition.position, Quaternion.LookRotation(aimDirection, Vector3.up));
            starterAssestInputs.shoot = false;

            
           
            if (!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(gunFire);
                PullTheTrigger();
            }
            
           
            if (hitTransform != null)
            {

                //hit something
                if (hitTransform.GetComponent<EnemyManager>() != null)
                {
                    Debug.Log("Hedef vuruldu");
                    hitTransform.gameObject.GetComponent<EnemyManager>().TakeDamage();
                    //Hit target
                }
                else
                {
                    Debug.Log("Baþka birþey vuruldu");
                    //Hit something else 
                }

            }
         
        }

    }

    void PullTheTrigger()
    {
        playerGunShot.Shoot();
    }

}
