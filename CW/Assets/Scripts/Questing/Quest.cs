using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// Base class for all quests

public class Quest : MonoBehaviour
{
    public List<Goal> Goals = new List<Goal>();
    public string QuestName;
    public string Hint;
    public bool Completed;

    Text finished;
    AudioSource complete;

    //public int curr;

    //// USED TO RESET GO HOME TEXT IF CURR SCENE IS HOME ////
    void Update()
    {
        finished = GameObject.Find("complete").GetComponent<Text>();
        Scene currentScene = SceneManager.GetActiveScene();

        if(currentScene.name == "Home" && Completed)
        {
            finished.text = "";
            //Debug.Log("Here");
        }
    }

    //// CHECKS IF ALL GOALS ARE COMPLETE ////
    public void CheckGoals()
    {
       Completed = Goals.All(g => g.Completed);
       //Debug.Log(Completed);
       if(Completed)        // If they are play quest complete sound and update UI to tell player to go home
       {
            complete = GameObject.Find("QuestComplete").GetComponent<AudioSource>();
            Debug.Log("Complete");
            finished.text = "Return home";
            complete.Play();
       }
    }

    //// FUNCTION TO UPDATE UI - OVERRIDE PER QUEST ////
    public virtual void updateUI() { }
}
