using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueEditor;

public class MarketerCon : MonoBehaviour
{
    public NPCConversation myConverasation;
    

    void OnTriggerStay(Collider coll)
    {
       
            if (Input.GetKeyDown(KeyCode.E) && coll.gameObject.tag == "Player")
            ConversationManager.Instance.StartConversation(myConverasation);
                    
    }



}
