using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Base class for quest goal

public class Goal
{
    public Quest Quest;
    public string Description;
    public bool Completed;
    public int CurrAmount;
    public int ReqAmount;
    
    //// INITIALIZATION OF GOAL - REDIFENED FOR GOAL TYPE ////
    public virtual void Init()
    {

    }

    //// EVALUATES IF GOAL HAS BEEN MET ////
    public void Eval()
    {
        //Quest.curr = CurrAmount;
        Quest.updateUI();
        if (CurrAmount >= ReqAmount)
        {
            //Debug.Log("Evaluating");
            Complete();
        }
    }

    //// CHECKS IF ALL GOALS COMPLETE ////
    public void Complete()
    {
        Completed = true;
        //Debug.Log("Checking");
        Quest.CheckGoals();
    }
}
