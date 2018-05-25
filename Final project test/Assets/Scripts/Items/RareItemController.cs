﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RareItemController : MonoBehaviour {

	public int ID;
	//Potion or Item
	public string type;
	public bool setItem = false;

	public Transform target;
	public Player player;

	public bool location = false;
	float spawnTime;
	public bool rareItem = false;
	// Use this for initialization
	void Awake() {

	}

	void Start () {
		target = GameObject.FindGameObjectWithTag ("Player").transform;
		player = target.GetComponent<Player> ();
		spawnTime = Time.time;

		typeSet ();
	}

	void typeSet() {
		if (!setItem) {
			int randType = Random.Range (0, 10);
			if (randType < 6) {
				type = "Item";
			} else {
				type = "Potion";
			}

			if (type == "Item") {
				generateItem ();
			} else {
				generatePotion ();
			}
		}
	}

	void generateItem() {
		int roll = Random.Range (0, 10);
		if (roll < 4) {
			roll = Random.Range (0, 100);
			if (roll < 20 && rareItem) {
				ID = Random.Range (36, 60);
			} else if (roll < 25) {
				ID = Random.Range (60, 66);
			} else if (roll < 45) {
				ID = Random.Range (30, 36);
			} else {
				ID = Random.Range (6, 30);
			}
		} else {
			ID = Random.Range (0, 5);
		}
	}

	void generatePotion() {
		int roll = Random.Range (0, 10);
		if (roll < 1) {
			ID = Random.Range (2, 4);
		} else {
			ID = Random.Range (0, 2);
		}
	}

	// Update is called once per frame
	void Update () {
		if (!location) {
			transform.position = target.position;
		}
		if (Time.time - spawnTime > 10) {
			transform.position = target.position;
		}
	}

	void pickUP() {
		if (type == "Potion") {
			if (player.pInv.checkEmpty ()) {
				player.addPotion (ID);
				Destroy (transform.parent.gameObject);
			}
		} else {
			if (player.inv.checkEmpty ()) {
				player.addItem (ID);
				Destroy (transform.parent.gameObject);
			}
		}
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.CompareTag ("Player")) {
			pickUP ();
		}

		if (other.gameObject.CompareTag ("outofbounds")) {
			location = true;
		}
	}

	void OnTriggerStay2D(Collider2D other) {
		if (other.gameObject.CompareTag ("Player")) {
			pickUP ();
		}
	}

	void OnTriggerExit2D(Collider2D other) {

	}
}
