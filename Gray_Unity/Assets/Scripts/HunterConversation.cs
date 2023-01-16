using DialogueEditor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HunterConversation : MonoBehaviour
{
  public NPCConversation myConverasation;

    private void OnTriggerStay(Collider other)
    {
        if (Input.GetKeyDown(KeyCode.E) && other.gameObject.tag == "Player")
        {
            ConversationManager.Instance.StartConversation(myConverasation);
        }
    }
}
