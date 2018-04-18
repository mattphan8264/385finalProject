﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PisanController : MonoBehaviour {

	private float playerX;
	private float enemyX;
	private float playerY;
	private float enemyY;
	public Transform target;

	public GameObject bullet;
	Vector2 bulletPos;
	private float fireRate = 1;
	public float lastFire;

	public float left;
	public float right;
	public float up;
	public float down;
	public int speed = 1;
	int count = 1;

	public bool bulletGround = true;
	public Animator anim;

	private Transform myself;
	private BossHealth temp;

	float currentHealth;
	float maxHealth;

	void Awake() {
		target = GameObject.FindWithTag ("Player").transform;
		anim = GetComponent<Animator> ();
		myself = GameObject.FindWithTag("Boss").transform;
		temp = myself.GetComponent<BossHealth>();
	}
	//Declare target as player (who hes gonna shoot at)
	//Set health and attach it to health bar
	void Start () {


	}

	//Move pattern is in a square, top left to the left then down etc.
	//Can change as hp gets lower
	void Update () {
		currentHealth = temp.currentHealth;
		maxHealth = temp.maxHealth;
		if (currentHealth / maxHealth <= .5) {
			bulletGround = false;
		}

		if (transform.position.x > left && count == 1) {
			transform.position += Vector3.left * speed * Time.deltaTime;
			if (transform.position.x < left) {
				count++;
			}
		} else if (transform.position.y > down && count == 2) {
			transform.position += Vector3.down * speed * Time.deltaTime;
			if (transform.position.y < down) {
				count++;
			}

		} else if (transform.position.x < right && count == 3) {
			transform.position += Vector3.right * speed * Time.deltaTime;
			if (transform.position.y > right) {
				count++;
			}
		} else {
			transform.position += Vector3.up * speed * Time.deltaTime;
			if (transform.position.y > up) {
				count = 1;
			}
		}
		//Get the positions of player and enemy
		playerX = target.transform.position.x;
		enemyX = transform.position.x;
		playerY = target.transform.position.y;
		enemyY = transform.position.y;
		//Attack only at a particular interval (fire rate)
		if (Time.time - lastFire > fireRate && anim.GetBool("TookDamage") == false) {
			anim.SetBool ("Attacking", true);
			fire ();
			lastFire = Time.time;
		} else {
			anim.SetBool ("Attacking", false);
		}


	}

	//Instantiate bullet and shoot it at players location
	void fire() {
		float divider = Mathf.Sqrt (Mathf.Pow (playerX - enemyX, 2) + Mathf.Pow (playerY - enemyY, 2));
		BulletController shot = bullet.GetComponent<BulletController>();
		shot.setGroundCollide (bulletGround);
		shot.setVelocity ((playerX - enemyX) / divider, (playerY - enemyY) / divider);
		bulletPos = transform.position;
		Instantiate (bullet, bulletPos, Quaternion.identity);
	}

	//Deal damage if player touches pisan or take damage if the sword hits
	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.CompareTag ("Player")) {
			Player health = other.GetComponent<Player> ();
			health.takeDamage (1);
		}
	}
}