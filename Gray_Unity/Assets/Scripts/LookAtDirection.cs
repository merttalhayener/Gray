using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtDirection : MonoBehaviour
{
    public ThirdPersonController controllerScript;
    private Camera mainCamera;
    void Start()
    {
       
       
    }

    // Update is called once per frame
    void Update()
    {
        //Vector2 screenCenterPoint = new Vector2(Screen.width / 2f, Screen.height / 2f);//Ekranýn ortasýný alýyoruz
        //  transform.position = controllerScript.CinemachineCameraTarget.transform.position;

       Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray,out RaycastHit raycastHit))
        {
            transform.position = raycastHit.point;
        }
            
    }
}
