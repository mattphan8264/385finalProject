using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LitJson;

public class Inventory : MonoBehaviour
{

    public GameObject slotPanel;
    public GameObject inventorySlot;
    public GameObject inventoryItem;
    public ItemDatabase database;

    private int slotAmount;
    public List<GameObject> slots = new List<GameObject>();

	public static List<ItemStats> saveItems = new List<ItemStats>();


	public void save() {
		saveItems = new List<ItemStats> ();
		for (int i = 0; i < slotAmount; i++) {
			if (slots [i].GetComponent<Slot> ().item != null) {
				saveItems.Add(slots[i].GetComponent<Slot>().item.GetComponent<ItemStats>());
			}
		}
	}

	void Awake() {
		database = GameObject.Find ("Player").GetComponent<ItemDatabase> ();

		slotAmount = 10;
		for (int i = 0; i < slotAmount; i++)
		{
			slots.Add(Instantiate(inventorySlot));

			slots[i].GetComponent<Slot> ().id = i;
			slots[i].transform.SetParent(slotPanel.transform);
		}
	}

    void Start()
    {
		
    }

	public void showData() {
		for (int i = 0; i < slotAmount; i++) {
			if (slots [i].GetComponent<Slot> ().item == null) {
				print(i);
			}
		}
	}

	public bool checkEmpty() {
		for (int i = 0; i < slotAmount; i++) {
			if (slots [i].GetComponent<Slot> ().item == null) {
				return true;
			}
		}
		return false;
	}
		
	public void pickUpItem(int id) {
		if (id < 0) {
			return;
		}

		for (int i = 0; i < slotAmount; i++)
		{
			if (slots [i].GetComponent<Slot> ().item == null) {
				Slot temp = slots [i].GetComponent<Slot> ();
				temp.item = Instantiate (inventoryItem);
				temp.item.transform.SetParent (slots [i].transform);
				temp.item.transform.position = slots [i].transform.position;
				temp.item.GetComponent<Image> ().sprite = database.FetchItemByID (id).Sprite;
				temp.item.transform.localScale = new Vector3 (.5f, .5f, 0);
				temp.item.GetComponent<ItemObject> ().slot = i;
				temp.item.GetComponent<ItemStats> ().loadStats(database.RerollFetch(id));
				break;
			}
		}
	}

    public void AddItem(int id)
    {
		if (id < 0) {
			return;
		}

		for (int i = 0; i < slotAmount; i++)
        {
			if (slots [i].GetComponent<Slot> ().item == null) {
				Slot temp = slots [i].GetComponent<Slot> ();
				temp.item = Instantiate (inventoryItem);
				temp.item.transform.SetParent (slots [i].transform);
				temp.item.transform.position = slots [i].transform.position;
				temp.item.GetComponent<Image> ().sprite = database.FetchItemByID (id).Sprite;
				temp.item.transform.localScale = new Vector3 (.5f, .5f, 0);
				temp.item.GetComponent<ItemObject> ().slot = i;
				temp.item.GetComponent<ItemStats> ().loadStats(database.FetchItemByID(id));
				break;
			}
    	}
    }

	public void AddItem(ItemStats inputStats) {
		for (int i = 0; i < slotAmount; i++) {
			if (slots [i].GetComponent<Slot> ().item == null) {
				Slot temp = slots [i].GetComponent<Slot> ();
				temp.item = Instantiate (inventoryItem);
				temp.item.transform.SetParent (slots [i].transform);
				temp.item.transform.position = slots [i].transform.position;
				temp.item.GetComponent<Image> ().sprite = database.FetchItemByID (inputStats.ID).Sprite;
				temp.item.transform.localScale = new Vector3 (.5f, .5f, 0);
				temp.item.GetComponent<ItemObject> ().slot = i;
				temp.item.GetComponent<ItemStats> ().loadStats(inputStats);
				break;
			}
		}
	}

	public void AddItemSlot(int slot, ItemStats id) {
		Slot temp = slots[slot].GetComponent<Slot>();
		temp.item = Instantiate (inventoryItem);
		temp.item.transform.SetParent (slots [slot].transform);
		temp.item.transform.position = slots [slot].transform.position;
		temp.item.GetComponent<Image>().sprite = database.FetchItemByID (id.ID).Sprite;
		temp.item.transform.localScale = new Vector3 (.5f, .5f, 0);
		temp.item.GetComponent<ItemObject> ().slot = slot;
		temp.item.GetComponent<ItemStats> ().loadStats(id);
	}

	public void RemoveItemSlot(int slot) {
		Slot temp = slots[slot].GetComponent<Slot>();
		Destroy (temp.item);
		temp.item = null;
	}


}