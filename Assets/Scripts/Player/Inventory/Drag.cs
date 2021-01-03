using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Drag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {

    public static GameObject dragedObject;

    Inventory inventory;

    // Use this for initialization
    void Start () {
        inventory = GameObject.FindGameObjectWithTag("InventoryManager").GetComponent<Inventory>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnBeginDrag(PointerEventData eventData)
    {
        dragedObject = gameObject;
        inventory.dragPrefab.SetActive(true);
        inventory.dragPrefab.GetComponent<CanvasGroup>().blocksRaycasts = false;

        if (dragedObject.GetComponent<CurrentItem>())
        {
            int index = dragedObject.GetComponent<CurrentItem>().index;
            inventory.dragPrefab.GetComponent<Image>().sprite = inventory.item[index].icon;
            if (inventory.item[index].count > 1)
            {
                inventory.dragPrefab.transform.GetChild(0).GetComponent<Text>().text = inventory.item[index].count.ToString();
            }
            else
            {
                inventory.dragPrefab.transform.GetChild(0).GetComponent<Text>().text = null;
            }

            if (inventory.dragPrefab.GetComponent<Image>().sprite == null)
            {
                dragedObject = null;
                inventory.dragPrefab.SetActive(false);
            }
           
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        inventory.dragPrefab.transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            dragedObject.GetComponent<CurrentItem>().Drop();
            dragedObject.GetComponent<CurrentItem>().Remove();
        }

        dragedObject = null;
        inventory.dragPrefab.SetActive(false);
        inventory.dragPrefab.GetComponent<CanvasGroup>().blocksRaycasts = true;
    }
}
