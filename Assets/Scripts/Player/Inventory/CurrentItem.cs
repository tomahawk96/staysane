using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CurrentItem : MonoBehaviour, IPointerClickHandler, IDropHandler {

    [HideInInspector]
    public int index;

    GameObject inventoryObj;
    Inventory inventory;

    // Use this for initialization
    void Start () {
        inventoryObj = GameObject.FindGameObjectWithTag("InventoryManager");
        inventory = inventoryObj.GetComponent<Inventory>();
	}

    public void OnPointerClick(PointerEventData eventData)
    {

        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if (inventory.item[index].customEvent!=null)
            {
                inventory.item[index].customEvent.Invoke();
            }
            if (inventory.item[index].isRemovable)
            {
                Remove();
            }
        }

        if (eventData.button == PointerEventData.InputButton.Right)
        {
            if (inventory.item[index].isDroped)
            {
                Drop();
                Remove();
            }
        }
    }
    public void Remove()
    {
        if (inventory.item[index].id != 0)
        {
            if (inventory.item[index].count > 1)
            {
                inventory.item[index].count--;
            }
            else
            {
                inventory.item[index] = new Item();
            }
            inventory.DisplayItems();
        }
    }
    public void Drop ()
    {
        if (inventory.item[index].id != 0)
        {
            for (int i = 0; i < inventory.database.transform.childCount; i++)
            {
                Item item = inventory.database.transform.GetChild(i).GetComponent<Item>();
                if (item)
                {
                    if (inventory.item[index].id == item.id)
                    {
                        GameObject dropedObj = Instantiate(item.gameObject);
                        dropedObj.transform.position = Camera.main.transform.position + Camera.main.transform.forward;
                    }
                }
                
            }
        }
     }

    public void OnDrop(PointerEventData eventData)
    {
        GameObject dragedObject = Drag.dragedObject;

        if (dragedObject == null)
        {
            return;
        }

        CurrentItem currentdragedItem = dragedObject.GetComponent<CurrentItem>();

        if (currentdragedItem)
        {
            Item currentItem = inventory.item[GetComponent<CurrentItem>().index];
            inventory.item[GetComponent<CurrentItem>().index] = inventory.item[currentdragedItem.index];
            inventory.item[currentdragedItem.index] = currentItem;
            inventory.DisplayItems();
        }
    }
}
