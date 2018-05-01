﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LitJson;

public class Inventory : MonoBehaviour
{

    GameObject inventoryPanel;
    public GameObject slotPanel;
    public GameObject inventorySlot;
    public GameObject inventoryItem;
    ItemDatabase database;

    private int slotAmount;
    public List<Item> items = new List<Item>();
    public List<GameObject> slots = new List<GameObject>();

	void Awake() {
		database = GetComponent<ItemDatabase>();

		slotAmount = 10;
		inventoryPanel = GameObject.Find("Inventory Panel");
		// slotPanel = inventoryPanel.transform.Find("Slot Panel").gameObject;

		for (int i = 0; i < slotAmount; i++)
		{
			items.Add(new Item());
			slots.Add(Instantiate(inventorySlot));

			slots[i].GetComponent<Slot> ().id = i;
			slots[i].transform.SetParent(slotPanel.transform);

		}
	}

    void Start()
    {
        
    }
		
    public void AddItem(int id)
    {
		
        Item itemToAdd = database.FetchItemByID(id);
		if (itemToAdd == null) {
			Debug.Log ("nll");
		}
        if (itemToAdd.Stackable && CheckIfItemIsInInventory(itemToAdd))
        {
            for(int i = 0; i < items.Count; i++)
            {
                if(items[i].ID == id)
                {
                    ItemData data = slots[i].transform.GetChild(0).GetComponent<ItemData>();
                    data.amount++;
                    data.transform.GetChild(0).GetComponent<Text>().text = data.amount.ToString();
                    break;
                }
            }

        }
        else
        {
            for (int i = 0; i < items.Count; i++)
            {
                if (items[i].ID == -1)
                {
                    items[i] = itemToAdd;
                    GameObject itemObj = Instantiate(inventoryItem);

					itemObj.GetComponent<ItemData> ().item = itemToAdd;
					itemObj.GetComponent<ItemData> ().slot = i;
                    itemObj.transform.SetParent(slots[i].transform);
					itemObj.transform.position = slots [i].transform.position;
                    itemObj.GetComponent<Image>().sprite = itemToAdd.Sprite;
                    itemObj.name = itemToAdd.Title;
                    ItemData data = slots[i].transform.GetChild(0).GetComponent<ItemData>();
                    data.amount = 1;
                    break;

                }
            }
        }

    }

	public void RemoveItem(int id) {
		Item itemToRemove = database.FetchItemByID (id);
		if (itemToRemove.Stackable && CheckIfItemIsInInventory (itemToRemove)) {
			for (int j = 0; j < items.Count; j++) {
				if (items [j].ID == id) {
					ItemData data = slots [j].transform.GetChild (0).GetComponent<ItemData> ();
					data.amount--;
					data.transform.GetChild (0).GetComponent<Text> ().text = data.amount.ToString ();
					if (data.amount == 0) {
						Destroy (slots [j].transform.GetChild (0).gameObject);
						items [j] = new Item ();
						break;
					}
					if (data.amount == 1) {
						slots [j].transform.GetChild (0).transform.GetChild (0).GetComponent<Text> ().text = "";
						break;
					}
					break;
				}
			}
		} else {
			for (int i = 0; i < items.Count; i++) {
				if (items [i].ID != -1 && items [i].ID == id) {
					Destroy (slots [i].transform.GetChild (0).gameObject);
					items [i] = new Item ();
					break;
				}
			}
		}
	}

    bool CheckIfItemIsInInventory(Item item)
    {
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].ID == item.ID)
            {
                return true;
            }
        }
        return false;
    }

}