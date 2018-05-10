﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialEnemyHealth : MonoBehaviour {
	public float maxHealth;
	public float currentHealth;
	public Transform target;
	public float lastHit;
	public GameObject parent;
	public EnemyController parentController;
	public float fill;
	public string monsterName;

	public int enemyNumber;

	public Image healthbar;
	//Drops
	public GameObject coin;
	public GameObject item;

	public TextMesh control;
	Renderer render;

	void Awake() {
	}

	void Start () {
		//If maxhealth hasnt been initialized, default value of 100
		if (maxHealth == 0) {
			maxHealth = 100;
		}
		lastHit = 0;
		target = GameObject.FindWithTag ("Player").transform;
		parent = transform.parent.gameObject;
		parentController = parent.GetComponent<EnemyController> ();

		currentHealth = maxHealth;
		control = GetComponentInChildren<TextMesh> ();
		control.text = monsterName;
		render = parent.GetComponent<Renderer> ();
	}

	void Update () {
		if (Time.time - lastHit > 2f) {
			control.text = monsterName;
		}
		healthbar.fillAmount = currentHealth / maxHealth;
		if (currentHealth <= 0) {
			dropCoin ();
			dropItem ();
			parentController.destroy();
			if (enemyNumber == 0) {
				PlayerTutorial temp = target.GetComponent<PlayerTutorial> ();
				temp.killedFirst = true;
			} else if (enemyNumber == 1) {
				PlayerTutorial temp = target.GetComponent<PlayerTutorial> ();
				temp.killedSecond = true;
			} else if (enemyNumber == 2) {
				PlayerTutorial temp = target.GetComponent<PlayerTutorial> ();
				temp.killedThird = true;
			} else if (enemyNumber == 3) {
				PlayerTutorial temp = target.GetComponent<PlayerTutorial> ();
				temp.killedFourth = true;
			} else if (enemyNumber == 4) {
				PlayerTutorial temp = target.GetComponent<PlayerTutorial> ();
				temp.killedFifth = true;
			}
		}
	}

	//Takes in an int and decreases player health by an amount
	//Records previous time since last hit and doesn't inflict damage
	//unless time since last hit is past the point
	public void takeDamage(int damage) {
		if (parentController.active == true) {
			control.text = monsterName + "    " + damage;
			if (transform.position.x - target.position.x < 0) {
				parentController.moveLeft ();
			} else {
				parentController.moveRight ();
			}
			currentHealth -= damage;
			if (currentHealth <= 0) {
			}
			if (Time.time - lastHit > 0.5) {
				damageAnimation ();
				Invoke ("damageAnimation", .1f);
			}
			lastHit = Time.time;
		}
	}

	public void damageAnimation() {
		render.enabled = !render.enabled;
	}

	public void dropCoin() {
		Instantiate (coin, transform.position, Quaternion.identity);
	}

	public void dropItem() {
		Instantiate (item, transform.position, Quaternion.identity);
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.CompareTag ("Player")) {
			PlayerStatistics.takeDamage(1);
		}
	}

	void OnTriggerStay2D(Collider2D other) {
		if (other.gameObject.CompareTag ("Player")) {
			PlayerStatistics.takeDamage(1);
		}
	}
}