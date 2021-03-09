using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIEventHandler : MonoBehaviour
{
    public delegate void ItemEventHandler(Item item);
    public static event ItemEventHandler OnItemAddedToInv;

    public static void ItemAddedToInv(Item item)
    {
        OnItemAddedToInv(item);
        Debug.Log("Adding item");
    }

}
