﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GEnemyR : MonoBehaviour {
	//Start variables
	public float lastFire;
	public Transform target;
	public Animator anim;

	//Update variables
	public bool location;
	public float playerX;
	public float enemyX;
	public float playerY;
	public float enemyY;

	//Movement variables
	public float MinDist = 0.1f;
	public bool facing = false;
	public float speed;
	public float jumpTime;
	public bool jumping;
	public float jumpSpeed;

	//Fire variables
	public GameObject bullet;
	Vector2 bulletPos;
	public float fireRate = 1;

	// Use this for initialization
	void Start () {
		lastFire = 0;
		target = GameObject.FindWithTag ("Player").transform;
		anim = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (!location) {
			Destroy (gameObject);
		}

		playerX = target.transform.position.x;
		enemyX = transform.position.x;
		playerY = target.transform.position.y;
		enemyY = transform.position.y;

		if (Time.time - jumpTime > .4) {
			jumping = false;
		}

		if (playerY - enemyY > .6) {
			jumping = true;
			jumpTime = Time.time;
		}
			
		if (enemyX < playerX) {
			if (facing) {
				flip ();
			}
		}
		if (enemyX > playerX) {
			if (!facing) {
				flip ();
			}
		}
		collideMove ();

		if (!(enemyX - playerX > MinDist) && !(enemyX - playerX < -MinDist)) {
			anim.SetBool ("attacking", true);
			anim.SetBool ("walking", false);
			if (Time.time - lastFire > fireRate) {
				Invoke ("fire", 1.0f);
				lastFire = Time.time;
			}
		}
	}

	void collideMove() {
		anim.SetBool ("attacking", false);
		if (jumping) {
			transform.position += Vector3.up * jumpSpeed * .07f;
			if (enemyX - playerX < -MinDist) {
				transform.position += Vector3.right * speed * Time.deltaTime;
			} else {
				transform.position += Vector3.left * speed * Time.deltaTime;
			}
		} else {
			
			if (enemyX - playerX < -MinDist || enemyX - playerX > MinDist) {
				anim.SetBool ("walking", true);
				if (enemyX - playerX < -MinDist) {
					transform.position += Vector3.right * speed * Time.deltaTime;
				}
				if (enemyX - playerX > MinDist) {
					transform.position += Vector3.left * speed * Time.deltaTime;
				}
			}
		}
	}

	void fire() {
		float divider = Mathf.Sqrt (Mathf.Pow (playerX - enemyX, 2) + Mathf.Pow (playerY - enemyY, 2));
		BulletController shot = bullet.GetComponent<BulletController>();
		shot.setVelocity ((playerX - enemyX) / divider, (playerY - enemyY) / divider);
		bulletPos = transform.position;
		Instantiate (bullet, bulletPos, Quaternion.identity);
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.CompareTag ("outofbounds")) {
			location = true;
		}
		if (other.gameObject.CompareTag ("Player")) {
			PlayerStatistics.takeDamage(1);
		}
	}

	void OnTriggerExit2D(Collider2D other) {
		if (other.gameObject.CompareTag ("outofbounds")) {
			location = false;
		}
	}

	void flip() {
		facing = !facing;
		Vector3 charscale = transform.localScale;
		charscale.x *= -1;
		transform.localScale = charscale;
	}
}