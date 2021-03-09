using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Second quest player recieves

public class FirstQuest : Quest
{
    //// UI VARIABLES ////
    Text popUpTitle;
    Text popUpDesc;
    Text UITitle;
    Text UIDesc;
    Text complete;
    textFadeOut fade;
    Color og;

    //bool run;
    //int curr;

    //GatherGoal ammount;
    // Start is called before the first frame update
    void Start()
    {
        QuestName = "First Quest";
        Hint = null;

        //// GOALS OF THE QUEST ////
        Goals.Add(new GatherGoal(this, "Pin", "Find 2 pins", false, 0, 2, false));
        //Goals.Add(new GatherGoal(this, "Sugar", "Find a sugar cube", false, 0, 1, false));
        Goals.Add(new GatherGoal(this, "Battery", "Find 3 batteries", false, 0, 3, false));
        Goals.Add(new GatherGoal(this, "Tissue", "Find a piece of tissue", false, 0, 1, false));


        Goals.ForEach(g => g.Init());   // Calls init function (GatherGoal script) for each goal

        //// UI VARIABLE ASSIGNMENT ////
        popUpTitle = GameObject.Find("QuestTitle").GetComponent<Text>();
        popUpDesc = GameObject.Find("QuestItems").GetComponent<Text>();
        UITitle = GameObject.Find("questTitle").GetComponent<Text>();
        UIDesc = GameObject.Find("questItems").GetComponent<Text>();
        complete = GameObject.Find("wrongItem").GetComponent<Text>();
        //// TEXT DEFAULT COLOUR ////
        og.r = 0.964f;
        og.g = 1.000f;
        og.b = 0.000f;
        og.a = 1.000f;

        //Debug.Log(og);

        //// SETS THE INFO FOR THE QUEST POP UP WINDOW AND SIDE BAR ////
        popUpTitle.text = QuestName;
        UITitle.text = QuestName;

        foreach (GatherGoal goal in Goals)
        {
            Debug.Log(goal.Description);
            popUpDesc.text += goal.Description + "\n";

            UIDesc.text += goal.ItemID + "  " + goal.CurrAmount + "/" + goal.ReqAmount + "\n";
        }

        if (Hint != null)   // Adds hint to pop up window if there is one
            popUpDesc.text += "\n Hint: " + Hint;

    }

    //// UPDATES THE SIDE BAR UI WHEN AN ITEM IS PICKED UP - OVERRIDES BASE FUNCTION ////
    public override void updateUI()
    {
        //run = false;
        //// RESETS TEXT ////
        UIDesc.text = "";
        complete.text = "";
        complete.color = og;

        foreach (GatherGoal goal in Goals)
        {
            if (goal.CurrAmount < goal.ReqAmount)   // Updates curr amount on side bar if goal not reached
                UIDesc.text += goal.ItemID + "  " + goal.CurrAmount + "/" + goal.ReqAmount + "\n";
            else if (goal.CurrAmount == goal.ReqAmount && goal.Gathered == false)   // When goal reached tells player - not added to UI side bar
            {
                //Debug.Log("Goal Reached");
                //Debug.Log(goal.ItemID);
                complete.text = goal.ItemID + " goal reached";
                //Debug.Log(complete.text);
                fade = (textFadeOut)GameObject.Find("wrongItem").GetComponent(typeof(textFadeOut));
                fade.FadeOut();
                goal.Gathered = true;
            }
        }
    }

}
