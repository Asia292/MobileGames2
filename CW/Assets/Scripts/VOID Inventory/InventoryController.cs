using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryController : MonoBehaviour
{
    public static InventoryController Instance { get; set; }

    public InventoryUIDetails inventoryDetailsPanel;
    public List<Item> playerItems = new List<Item>();

    // Start is called before the first frame update
    void Start()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;

        GiveItem("rope");
        GiveItem("sugar");
    }

    public void GiveItem(string itemSlug)
    {
        Item item = ItemDatabase.Instance.GetItem(itemSlug);
        playerItems.Add(item);
        UIEventHandler.ItemAddedToInv(item);
    }

    public void SetItemDetails(Item item, Button selectedButton)
    {
        inventoryDetailsPanel.SetItem(item, selectedButton);
    }
}



///////https://www.youtube.com/watch?v=04_qOAHGtpk&list=PLivfKP2ufIK6ToVMtpc_KTHlJRZjuE1z0&index=11 ///////////////
