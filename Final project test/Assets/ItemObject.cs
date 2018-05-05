﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;


public class ItemObject : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler {

	public int slot;
	public Vector2 offset;
	public Inventory inv;
	private Equipment eInv;


	public Tooltip tooltip;
	public EquipmentTooltip eTooltip;
	public bool equipped = false;

	//Question
	public bool noDelete = false;
	public bool showQuestion = false;
	CanvasController question;
	public Player target;

	public PlayerResources player;

	void Start() {
		inv = GameObject.Find("Inventory").GetComponent<Inventory>();
		eInv = GameObject.Find ("Equipment").GetComponent<Equipment> ();
		tooltip = inv.GetComponent<Tooltip>();
		eTooltip = eInv.GetComponent<EquipmentTooltip> ();

		player = GameObject.Find ("Player").GetComponent<PlayerResources> ();
		target = GameObject.FindGameObjectWithTag ("Player").GetComponent<Player> ();
		question = target.playerCanvas;
	}

	public void OnPointerClick(PointerEventData eventData) {
		if (eventData.button == PointerEventData.InputButton.Right) {
			if (!equipped) {
				ItemStats temp = inv.slots[slot].GetComponent<Slot> ().item.GetComponent<ItemStats> ();
				if (temp.type == "Helmet") {
					if (eInv.slots[0].GetComponent<EquipmentSlot> ().item != null) {
						ItemStats temp2 = eInv.slots[0].GetComponent<EquipmentSlot> ().item.GetComponent<ItemStats> ();
						inv.RemoveItemSlot (slot);
						eInv.RemoveItemSlot (0);
						inv.AddItem (temp2);
						eInv.AddItem (temp, 0, true);
					} else {
						inv.RemoveItemSlot (slot);
						eInv.AddItem (temp, 0, true);
						tooltip.Deactivate ();
						player.changeHelmet ("DrawingsV2/Items/Equipment/" + temp.Slug);
						PlayerStatistics.str += temp.str;
						PlayerStatistics.dex += temp.dex;
						PlayerStatistics.wis += temp.wis;
						PlayerStatistics.luk += temp.luk;
						PlayerStatistics.atk += temp.atk;
						PlayerStatistics.def += temp.atk;
					}
				}

				if (temp.type == "Armor") {
					if (eInv.slots[1].GetComponent<EquipmentSlot> ().item != null) {
						ItemStats temp2 = eInv.slots[1].GetComponent<EquipmentSlot> ().item.GetComponent<ItemStats> ();
						inv.RemoveItemSlot (slot);
						eInv.RemoveItemSlot (1);
						inv.AddItem (temp2);
						eInv.AddItem (temp, 1, true);
					} else {
						inv.RemoveItemSlot (slot);
						eInv.AddItem (temp, 1, true);
						tooltip.Deactivate ();
						player.changeArmor("DrawingsV2/Items/Equipment/" + temp.Slug);
						PlayerStatistics.str += temp.str;
						PlayerStatistics.dex += temp.dex;
						PlayerStatistics.wis += temp.wis;
						PlayerStatistics.luk += temp.luk;
						PlayerStatistics.atk += temp.atk;
						PlayerStatistics.def += temp.atk;
					}
				}

				if (temp.type == "Sword") {
					if (eInv.slots[2].GetComponent<EquipmentSlot> ().item != null) {
						ItemStats temp2 = eInv.slots[2].GetComponent<EquipmentSlot> ().item.GetComponent<ItemStats> ();
						inv.RemoveItemSlot (slot);
						eInv.RemoveItemSlot (2);
						inv.AddItem (temp2);
						eInv.AddItem (temp, 2, true);
					} else {
						inv.RemoveItemSlot (slot);
						eInv.AddItem (temp, 2, true);
						tooltip.Deactivate ();
						player.changeSword ("DrawingsV2/Items/Equipment/" + temp.Slug);
						PlayerStatistics.str += temp.str;
						PlayerStatistics.dex += temp.dex;
						PlayerStatistics.wis += temp.wis;
						PlayerStatistics.luk += temp.luk;
						PlayerStatistics.atk += temp.atk;
						PlayerStatistics.def += temp.atk;
					}
				}

				if (temp.type == "Spear") {
					if (eInv.slots[3].GetComponent<EquipmentSlot> ().item != null) {
						ItemStats temp2 = eInv.slots[3].GetComponent<EquipmentSlot> ().item.GetComponent<ItemStats> ();
						inv.RemoveItemSlot (slot);
						eInv.RemoveItemSlot (3);
						inv.AddItem (temp2);
						eInv.AddItem (temp, 3, true);
					} else {
						inv.RemoveItemSlot (slot);
						eInv.AddItem (temp, 3, true);
						tooltip.Deactivate ();
						player.changeSpear ("DrawingsV2/Items/Equipment/" + temp.Slug);
						PlayerStatistics.str += temp.str;
						PlayerStatistics.dex += temp.dex;
						PlayerStatistics.wis += temp.wis;
						PlayerStatistics.luk += temp.luk;
						PlayerStatistics.atk += temp.atk;
						PlayerStatistics.def += temp.atk;
					}
				}

				if (temp.type == "Axe") {
					if (eInv.slots[4].GetComponent<EquipmentSlot> ().item != null) {
						ItemStats temp2 = eInv.slots[4].GetComponent<EquipmentSlot> ().item.GetComponent<ItemStats> ();
						inv.RemoveItemSlot (slot);
						eInv.RemoveItemSlot (4);
						inv.AddItem (temp2);
						eInv.AddItem (temp, 4, true);
					} else {
						inv.RemoveItemSlot (slot);
						eInv.AddItem (temp, 4, true);
						tooltip.Deactivate ();
						player.changeAxe ("DrawingsV2/Items/Equipment/" + temp.Slug);
						PlayerStatistics.str += temp.str;
						PlayerStatistics.dex += temp.dex;
						PlayerStatistics.wis += temp.wis;
						PlayerStatistics.luk += temp.luk;
						PlayerStatistics.atk += temp.atk;
						PlayerStatistics.def += temp.atk;
					}
				}

				if (temp.type == "Dagger") {
					if (eInv.slots[5].GetComponent<EquipmentSlot> ().item != null) {
						ItemStats temp2 = eInv.slots[5].GetComponent<EquipmentSlot> ().item.GetComponent<ItemStats> ();
						inv.RemoveItemSlot (slot);
						eInv.RemoveItemSlot (5);
						inv.AddItem (temp2);
						eInv.AddItem (temp, 5, true);
					} else {
						inv.RemoveItemSlot (slot);
						eInv.AddItem (temp, 5, true);
						tooltip.Deactivate ();
						player.changeDagger ("DrawingsV2/Items/Equipment/" + temp.Slug);
						PlayerStatistics.str += temp.str;
						PlayerStatistics.dex += temp.dex;
						PlayerStatistics.wis += temp.wis;
						PlayerStatistics.luk += temp.luk;
						PlayerStatistics.atk += temp.atk;
						PlayerStatistics.def += temp.atk;
					}
				}
			} else {
				if (eventData.button == PointerEventData.InputButton.Right) {
					ItemStats temp = eInv.slots[slot].GetComponent<EquipmentSlot> ().item.GetComponent<ItemStats> ();
					eInv.RemoveItemSlot (slot);
					inv.AddItem (temp);
					PlayerStatistics.str -= temp.str;
					PlayerStatistics.dex -= temp.dex;
					PlayerStatistics.wis -= temp.wis;
					PlayerStatistics.luk -= temp.luk;
					PlayerStatistics.atk -= temp.atk;
					PlayerStatistics.def -= temp.def;
				}
			}
		}
	}
		
	public void OnPointerDown(PointerEventData eventData)
	{
		if (!equipped) {
			offset = eventData.position - new Vector2 (this.transform.position.x, this.transform.position.y);
			this.transform.position = eventData.position - offset;
		}
	}

	public void OnBeginDrag(PointerEventData eventData)
	{
		if (!equipped) {
			noDelete = false;
			this.transform.SetParent (this.transform.parent.parent);
			GetComponent<CanvasGroup> ().blocksRaycasts = false;
		}
	}

	public void OnDrag(PointerEventData eventData)
	{
		if (!equipped) {
			this.transform.position = eventData.position - offset;
		}
	}

	public void OnEndDrag(PointerEventData eventData)
	{
		if (!equipped) {
			this.transform.SetParent (inv.slots [slot].transform);
			this.transform.position = inv.slots [slot].transform.position;
			GetComponent<CanvasGroup> ().blocksRaycasts = true;

			if (noDelete == false) {
				showQuestion = true;
				question.showQuestion ();
			} else {
				showQuestion = false;
			}
		}
	}
		
	public void OnPointerEnter(PointerEventData eventData)
	{
		if (equipped) {
			eTooltip.Activate (eInv.slots[slot].GetComponent<EquipmentSlot>().item.GetComponent<ItemStats>());
		} else {
			tooltip.Activate (inv.slots[slot].GetComponent<Slot>().item.GetComponent<ItemStats>());
		}
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		if (equipped) {
			eTooltip.Deactivate();
		} else {
			tooltip.Deactivate ();
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (showQuestion) {
			if (question.clicked) {
				if (question.answer == true) {
					inv.RemoveItemSlot (slot);
					question.hideQuestion ();
					question.clicked = false;
				} else {
					noDelete = false;
					showQuestion = false;
					question.hideQuestion ();
					question.clicked = false;
				}
			}
		}
	}


}
