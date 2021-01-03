using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public List<Item> item;
	public GameObject cellContainer;
	public GameObject inventoryContainer;
	public KeyCode showInventory;
	public KeyCode takeButton;
	private Item currentItem;
	public GameObject database;
	public GameObject dragPrefab;
	public GameObject tooltipWindow;

    void Start()
    {
		item = new List<Item>();
		for(int i = 0; i < cellContainer.transform.childCount; i++){
			cellContainer.transform.GetChild(i).GetComponent<CurrentItem>().index = i;
			item.Add(new Item());
		}
    }

    void Update()
    {
        ToggleInventory();
		if(Input.GetKeyDown(takeButton)){
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			if(Physics.Raycast(ray, out hit, 20f)){
				if (hit.collider.GetComponent<Item>())
                {
                    Item currentItem = hit.collider.GetComponent<Item>();
                    AddItem(hit.collider.GetComponent<Item>());
					tooltipWindow.SetActive(false);
                }
			}
		}

		if(Input.GetKeyDown(KeyCode.Escape)){
			CloseInventory();
		}
    }

	void ToggleInventory(){
		if(Input.GetKeyDown(showInventory)){
			if(inventoryContainer.activeSelf){
				inventoryContainer.SetActive(false);
			}
			else{
				inventoryContainer.SetActive(true);
			}
		}
	}

	public void CloseInventory(){
		if(inventoryContainer.activeSelf){
			inventoryContainer.SetActive(false);
		}
	}
	
	void AddUnstackableItem(Item currentItem)
    {
        for (int i = 0; i < item.Count; i++)
        {
            if (item[i].id == 0)
            {
                item[i] = currentItem;
                item[i].count = 1;
                DisplayItems();
                Debug.Log("Добавлен не стак");
                Destroy(currentItem.gameObject);
                break;
            }
        }
    }

    void AddStackableItem(Item currentItem)
    {
        for (int i = 0; i < item.Count; i++)
        {
            if (item[i].id==currentItem.id)
            {
                item[i].count++;
                DisplayItems();
                Debug.Log("Добавлен стак");
                Destroy(currentItem.gameObject);
                return;
            }
        }
        AddUnstackableItem(currentItem);
    }

	void AddItem(Item currentItem){
		if(currentItem.isStackable){
			AddStackableItem(currentItem);
		}
		else{
			AddUnstackableItem(currentItem);
		}
	}

	public void DisplayItems(){
		for(int i = 0; i < item.Count; i++){
			Transform cell = cellContainer.transform.GetChild(i);
			Transform icon = cell.GetChild(0);
			Transform count = icon.GetChild(0);
			Text txt = count.GetComponent<Text>();
			Image img = icon.GetComponent<Image>();
			if(item[i].id != 0){
				img.enabled = true;
				img.sprite = item[i].icon;
				if(item[i].count > 1){
					txt.text = item[i].count.ToString();
				}
				else{
					txt.text = null;
				}
			}
			else{
				img.enabled = false;
				img.sprite = null;
				txt.text = null;
			}
		}
	}
}
