using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ObjectiveSystem : MonoBehaviour
{
    public Text objectiveText;

    public string[] objectives;

    public string currenObj;

    public GameObject gun;

    

    private void Start()
    {
          
        objectiveText.text = objectives[0];
        currenObj =objectives[0];
        
    }

    private void OnTriggerEnter(Collider coll)
    {
        if(coll.gameObject.tag == "Player")
        MissionManager();
    }

    private void MissionManager()
    {
        if (this.gameObject.tag == "Mission1")
        {
            objectiveText.text = objectives[1];
            currenObj = objectives[1];
            Destroy(gameObject);

        }
        
        if (this.gameObject.tag == "Mission2")
        {
            objectiveText.text = objectives[2];
            currenObj = objectives[2];
            Destroy(gameObject);


        }

        if (this.gameObject.tag == "Mission3")
        {
            objectiveText.text = objectives[3];
            currenObj = objectives[3];
            Destroy(gameObject);
        }

        if (this.gameObject.tag == "Mission4")
        {
            objectiveText.text = objectives[4];
            currenObj = objectives[4];
            Destroy(gameObject);
        }

        if (this.gameObject.tag == "Mission5")
        {
            objectiveText.text = objectives[5];
            currenObj = objectives[5];
            Destroy(gameObject);
        }

        if (this.gameObject.tag == "Mission6")
        {
            objectiveText.text = objectives[6];
            currenObj = objectives[6];
            Destroy(gameObject);
        }
        if (this.gameObject.tag == "Mission7")
        {
            objectiveText.text = objectives[7];
            currenObj = objectives[7];
            Destroy(gameObject);
        }
        if (this.gameObject.tag == "Mission8")
        {
            objectiveText.text = objectives[8];
            currenObj = objectives[8];
            Destroy(gameObject);
        }
        if (this.gameObject.tag == "Mission9")
        {
            objectiveText.text = objectives[9];
            currenObj = objectives[9];
            Destroy(gameObject);
        }
        if (this.gameObject.tag == "Mission10")
        {
            objectiveText.text = objectives[10];
            currenObj = objectives[10];
            Destroy(gun);
            Destroy(gameObject);
        }
        if (this.gameObject.tag == "Mission11")
        {
            objectiveText.text = objectives[11];
            currenObj = objectives[11];
            Destroy(gameObject);
        }
    }
}
