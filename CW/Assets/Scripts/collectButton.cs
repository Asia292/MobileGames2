using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collectButton : MonoBehaviour
{
    //GatherGoal item;
    GameObject obj;         // The obj that is being picked up
    GameObject player;
    GameObject dest;        // Destination when going up/down

    //// LISTENER SET UP ////
    public delegate void ItemPickedUp(GameObject collect);
    public static event ItemPickedUp OnItemCollected;

    public static void events(GameObject collect)
    {
        OnItemCollected(collect);
        //Debug.Log("passing " + collect);
    }

    //// TRIGGERS LISTENER WHEN BUTTON PRESSED ////
    public void pickUpItem()
    {
        obj = GameObject.Find("Ethan").GetComponent<playerMove>().obj;

        //Debug.Log(obj);
        events(obj);
        //Debug.Log(item.ItemID);

        /*if (obj.name == item.ItemID) //getting correct obj?
        {
            Debug.Log("Picking up");
        }*/
    }

    //// HIDES THE QUEST CANAS ON ACCEPT ////
    public void questAccept()
    {
        GameObject canvas = GameObject.Find("QuestCanvas");
        canvas.SetActive(false);
    }

    //// MOVES PLAYER TO CORRECT POSITION WHEN GOING UP ////
    public void upLadder()
    {
        player = GameObject.Find("Ethan");

        if (Vector3.Distance(player.transform.position, GameObject.Find("Up1").transform.position) < 0.4f)
            dest = GameObject.Find("Down1");
        else
            dest = GameObject.Find("Down2");

        player.transform.position = dest.transform.position;
    }

    //// MOVES PLAYER TO CORRECT POSITION WHEN GOING DOWN ////
    public void downLadder()
    {
        player = GameObject.Find("Ethan");

        if (Vector3.Distance(player.transform.position, GameObject.Find("Down1").transform.position) < 0.4f)
            dest = GameObject.Find("Up1");
        else
            dest = GameObject.Find("Up2");

        player.transform.position = dest.transform.position;
    }
}
