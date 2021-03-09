using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Defines the nature of this goal - to gather an item

public class GatherGoal : Goal
{
    //// UNIQUE GOAL VARIABLES ////
    public string ItemID;
    public bool Gathered;

    //// UI VARIABLES ////
    Text complete;
    textFadeOut fade;
    Color og;
    AudioSource collected;

    //// TAKES VARS PASSED IN AND SETS THEM EQUAL TO THAT IN THIS INSTANCE ////
    public GatherGoal(Quest quest, string itemID, string description, bool completed, int currAmount, int reqAmount, bool gathered)
    {
        this.Quest = quest;
        this.ItemID = itemID;
        this.Description = description;
        this.Completed = completed;
        this.CurrAmount = currAmount;
        this.ReqAmount = reqAmount;
        this.Gathered = gathered;
    }

    //// ASSIGNS THE FUNCTION TO THE LISTENER ////
    public override void Init()
    {
        //base.Init();    //event listener for when item picked up - do i need? -  call on button press instead
        //UIEventHandler.OnItemAdded += ItemGathered;    
        collectButton.OnItemCollected += ItemGathered;
    }

    //// DECIDES WHAT TO DO WHEN ITEM PICKED UP ////
    void ItemGathered(GameObject collect)
    {
        //// UI VARIABLES ////
        collected = GameObject.Find("ItemCollect").GetComponent<AudioSource>();
        complete = GameObject.Find("wrongItem").GetComponent<Text>();
        og.r = 0.964f;
        og.g = 1.000f;
        og.b = 0.000f;
        og.a = 1.000f;
        //Debug.Log(this.ItemID);
        if(collect.name == this.ItemID)     // If item being picked up matches goal
        {
            /*if (this.CurrAmount < ReqAmount)
                collect.SetActive(false);*/

            
            if (Gathered)       // If goal already reached tells player item not needed
            {
                complete.color = og;
                complete.text = "You don't need this anymore";
                //Debug.Log(complete.text);
                fade = (textFadeOut)GameObject.Find("wrongItem").GetComponent(typeof(textFadeOut));
                fade.FadeOut();
            }
            else        // Updates goal and disables item upon pick up, also calls goal eval to see if reached
            {
                collect.SetActive(false);
                collected.Play();
                this.CurrAmount++;
                //Debug.Log(this.CurrAmount);
                Eval();
            }
        }
    }
}
