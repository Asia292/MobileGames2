using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// A "dummy" quest that allows the system to assign a new quest to work with the first quest
// Not an actual quest, just a flag for a completed one

public class DummyQuest : Quest
{
    void Start()
    {
        Completed = true;

    }
}
