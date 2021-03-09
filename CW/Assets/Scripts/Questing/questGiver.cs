using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Assigns the quest to the player

public class questGiver : MonoBehaviour 
{
    //public bool assignQuest;

    public GameObject canvas;       // Quest canvas

    [SerializeField]
    GameObject quests;              // Obj quest assigned to
    [SerializeField]
    GameObject questAv;             // Obj that tells player quest available
    [SerializeField]
    string questType;               // Which quest to assign
    [SerializeField]
    string prevQuest;               // The prev quest

    Quest quest;                    // The curr quest

    void Update()
    {
        //// FINDS PREV QUEST AND CHECKS IF COMPLETE ////
        quests = GameObject.Find("Quests");

        quest = (Quest)quests.GetComponent(System.Type.GetType(prevQuest));

        if (quest.Completed)
            questAv.SetActive(true);

        //// RAYCAST TO SEE IF BOARD HAS BEEN TOUCHED - IF YES THEN RUN INTERACT ////
        if (((Input.touchCount > 0) && (Input.GetTouch(0).phase == TouchPhase.Began)) || (Input.GetMouseButtonDown(0)))
        {
            if ((/*Input.GetTouch(0).position.x*/ Input.mousePosition.x > Screen.width / 4) || (/*Input.GetTouch(0).position.y*/ Input.mousePosition.y > Screen.height/2))
            {
                //Debug.Log("RayCasting");
                Ray rayCast = Camera.main.ScreenPointToRay(Input.mousePosition);    //(Input.GetTouch(0).position)
                RaycastHit rayCastHit;
                if ((Physics.Raycast(rayCast, out rayCastHit)) && (rayCastHit.collider.name == "Board"))
                {
                    
                    interact();
                }
            }
        }
    }


    void interact()
    {
        /*quests = GameObject.Find("Quests");
        quest = (Quest)quests.GetComponent(System.Type.GetType(prevQuest));*/
        //Debug.Log(quest);
        //// DESTROYS PREV QUEST AND ASSIGNS NEXT IF PREV COMPLETE ////
        if (quest.Completed)
        {
            Destroy(quest);
            canvas.SetActive(true);
            assign();
        }



        /*if (!assignQuest)
        {
            canvas.SetActive(true);
            assign();
        }*/
    }

    void assign()
    {
        //// ATTATCHES NEW QUEST TO GAME OBJECT ////
        //assignQuest = true;
        quest = (Quest)quests.AddComponent(System.Type.GetType(questType));
        questAv.SetActive(false);
        //Debug.Log(quest);
    }


}
