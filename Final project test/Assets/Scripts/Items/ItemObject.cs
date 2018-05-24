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
	public PlayerTutorial target2;

	public bool tutorial = false;
	public PlayerResources player;
	public bool multiDelete = false;

	void Start() {
		inv = GameObject.Find("Inventory").GetComponent<Inventory>();
		eInv = GameObject.Find ("EquipmentInventory").GetComponent<Equipment> ();
		tooltip = inv.GetComponent<Tooltip>();
		eTooltip = eInv.GetComponent<EquipmentTooltip> ();

		player = GameObject.Find ("Player").GetComponent<PlayerResources> ();
		target = GameObject.FindGameObjectWithTag ("Player").GetComponent<Player> ();
		if (!tutorial) {
			question = target.playerCanvas;
		} else {
			target2 = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerTutorial> ();
			question = target2.playerCanvas;
		}
	}

	public void removeStats(ItemStats temp) {
		PlayerStatistics.str -= temp.str;
		PlayerStatistics.dex -= temp.dex;
		PlayerStatistics.wis -= temp.wis;
		PlayerStatistics.luk -= temp.luk;
		PlayerStatistics.atk -= temp.atk;
		PlayerStatistics.def -= temp.def;
	}

	public void addStats(ItemStats temp) {
		if (temp.type == "Helmet") {
			player.changeHelmet ("DrawingsV2/Items/Equipment/" + temp.Slug);
		} else if (temp.type == "Armor") {
			player.changeArmor("DrawingsV2/Items/Equipment/" + temp.Slug);
		} else if (temp.type == "Sword") {
			player.changeSword("DrawingsV2/Items/Equipment/" + temp.Slug);
		} else if (temp.type == "Spear") {
			player.changeSpear("DrawingsV2/Items/Equipment/" + temp.Slug);
		} else if (temp.type == "Axe") {
			player.changeAxe("DrawingsV2/Items/Equipment/" + temp.Slug);
		} else if (temp.type == "Dagger") {
			player.changeDagger("DrawingsV2/Items/Equipment/" + temp.Slug);
		}
		PlayerStatistics.str += temp.str;
		PlayerStatistics.dex += temp.dex;
		PlayerStatistics.wis += temp.wis;
		PlayerStatistics.luk += temp.luk;
		PlayerStatistics.atk += temp.atk;
		PlayerStatistics.def += temp.atk;
	}

	public void OnPointerClick(PointerEventData eventData) {
		if (eventData.button == PointerEventData.InputButton.Right) {
			if (!equipped) {
				ItemStats temp = inv.slots[slot].GetComponent<Slot> ().item.GetComponent<ItemStats> ();
				if (temp.type == "Helmet") {
					if (eInv.slots[0].GetComponent<EquipmentSlot> ().item != null) {
						ItemStats temp2 = eInv.slots[0].GetComponent<EquipmentSlot> ().item.GetComponent<ItemStats> ();
						removeStats (temp2);
						addStats (temp);
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
						removeStats (temp2);
						addStats (temp);
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
						removeStats (temp2);
						addStats (temp);
						inv.RemoveItemSlot (slot);
						eInv.RemoveItemSlot (2);
						inv.AddItem (temp2);
						eInv.AddItem (temp, 2, true);
						player.UIChange ();
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
						player.UIChange ();
					}
				}

				if (temp.type == "Spear") {
					if (eInv.slots[3].GetComponent<EquipmentSlot> ().item != null) {
						ItemStats temp2 = eInv.slots[3].GetComponent<EquipmentSlot> ().item.GetComponent<ItemStats> ();
						removeStats (temp2);
						addStats (temp);
						inv.RemoveItemSlot (slot);
						eInv.RemoveItemSlot (3);
						inv.AddItem (temp2);
						eInv.AddItem (temp, 3, true);
						player.UIChange ();
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
						player.UIChange ();
					}
				}

				if (temp.type == "Axe") {
					if (eInv.slots[4].GetComponent<EquipmentSlot> ().item != null) {
						ItemStats temp2 = eInv.slots[4].GetComponent<EquipmentSlot> ().item.GetComponent<ItemStats> ();
						removeStats (temp2);
						addStats (temp);
						inv.RemoveItemSlot (slot);
						eInv.RemoveItemSlot (4);
						inv.AddItem (temp2);
						eInv.AddItem (temp, 4, true);
						player.UIChange ();
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
						player.UIChange ();
					}
				}

				if (temp.type == "Dagger") {
					if (eInv.slots[5].GetComponent<EquipmentSlot> ().item != null) {
						ItemStats temp2 = eInv.slots[5].GetComponent<EquipmentSlot> ().item.GetComponent<ItemStats> ();
						removeStats (temp2);
						addStats (temp);
						inv.RemoveItemSlot (slot);
						eInv.RemoveItemSlot (5);
						inv.AddItem (temp2);
						eInv.AddItem (temp, 5, true);
						player.UIChange ();
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
						player.UIChange ();
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
					if (temp.type == "Helmet") {
						player.resetHelmet ();
					} else if (temp.type == "Armor") {
						player.resetArmor ();
					} else if (temp.type == "Sword") {
						player.resetSword ();
					} else if (temp.type == "Spear") {
						player.resetSpear ();
					} else if (temp.type == "Axe") {
						player.resetAxe ();
					} else if (temp.type == "Dagger") {
						player.resetDagger ();
					}
					tooltip.Deactivate ();
					player.UIChange ();
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
		GameObject delete = GameObject.Find ("Delete");
		if (delete != null) {
			if (showQuestion) {
				if (target != null) {
					target.tryingToDelete = true;
					if (question.clicked) {
						if (question.answer == true) {
							inv.RemoveItemSlot (slot);
							question.hideQuestion ();
							question.clicked = false;
							target.tryingToDelete = false;
						} else {
							noDelete = false;
							showQuestion = false;
							question.hideQuestion ();
							question.clicked = false;
							target.tryingToDelete = false;
						}
					}
				}
			}
		}

	}


}
