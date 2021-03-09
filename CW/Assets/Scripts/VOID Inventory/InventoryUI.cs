using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public RectTransform inventoryPanel;
    public RectTransform scrollViewContent;

    InventoryUIItem itemContainer { get; set; }
    bool menueIsActive { get; set; }

    Item currentSelectedItem { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        itemContainer = Resources.Load<InventoryUIItem>("Item_Container");

        UIEventHandler.OnItemAddedToInv += ItemAdded;

        inventoryPanel.gameObject.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I)) ////CHANGE FOR MOBILE - IF BUTTON PRESSED
        {
            menueIsActive = !menueIsActive;
            inventoryPanel.gameObject.SetActive(menueIsActive);
        }
    }

    public void ItemAdded(Item item)
    {
        InventoryUIItem emptyItem = Instantiate(itemContainer);
        emptyItem.SetItem(item);
        emptyItem.transform.SetParent(scrollViewContent);
        Debug.Log("I get here");
    }
}
