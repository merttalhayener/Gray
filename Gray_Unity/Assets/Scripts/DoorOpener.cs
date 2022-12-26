using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpener : MonoBehaviour
{
    private Animator anim;
    private bool doorOpen;

    void Start()
    {
        anim = GetComponent<Animator>();
        doorOpen = false;
    }

    private void Update()
    {
        
    }
    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag=="Player")
            if (Input.GetKey(KeyCode.E))
            {
                if (doorOpen == false)
                {
                    doorOpen = true;
                    anim.SetBool("isOpen", doorOpen);
                }
            }
       
    }
}
