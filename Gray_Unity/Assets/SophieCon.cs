using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueEditor;

public class SophieCon : MonoBehaviour
{
    public NPCConversation myConversation;

    private void OnTriggerStay(Collider coll)
    {
        if (Input.GetKeyDown(KeyCode.E) && coll.gameObject.tag == "Player")
        {
            ConversationManager.Instance.StartConversation(myConversation); 
        }
    }
}
