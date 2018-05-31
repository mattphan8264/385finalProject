using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalBoss2Controller : MonoBehaviour {

	public float playerX;
	public float enemyX;
	public float playerY;
	public float enemyY;
	public Transform target;
	public Animator anim;

	public float left;
	public float right;
	public float down;
	public float up;

	public bool goLeft = true;
	public bool goRight = false;
	public bool goDown = true;
	public bool goUp = false;

	public GameObject bullet;
	public GameObject bullet2;
	Vector2 bulletPos;

	public BossHealth myHealth;
	int baseDamage = 25;
	int damage = 100;
	public float increaseDamage;

	public float dropDown;
	public float stayDown;
	public bool stayDropped = false;

	public bool shooting = true;
	public float shootTime;
	public float delayShot;

	public float shottime = 1f;

	void Awake() {
		target = GameObject.FindWithTag ("Player").transform;
		anim = GetComponent<Animator> ();
		myHealth = GetComponent<BossHealth> ();
	}

	void Start () {
		increaseDamage = Time.time;
		dropDown = Time.time;
	}

	void Update () {
		if (Time.time - increaseDamage > 30f) {
			damage += baseDamage;
			increaseDamage = Time.time;
		}

		if (myHealth.currentHealth < myHealth.maxHealth / 2) {
			shottime = .25f;
		} else if (myHealth.currentHealth < (myHealth.maxHealth / 4) * 3) {
			shottime = .5f;
		}

		playerX = target.transform.position.x;
		enemyX = transform.position.x;
		playerY = target.transform.position.y;
		enemyY = transform.position.y;

		if (Time.time - dropDown > 10) {
			shooting = false;
			if (transform.position.y > down && !stayDropped && goDown) {
				transform.position += Vector3.down * 1f * Time.deltaTime;
				stayDown = Time.time;
			} else if (Time.time - stayDown < 3f) {
				if (stayDropped == false) {
					stayDropped = true;
				}
			} else {
				if (transform.position.y < up) {
					transform.position += Vector3.up * 1f * Time.deltaTime;
				} else {
					goDown = true;
					dropDown = Time.time;
					stayDropped = false;
				}
			}
		} else {
			shooting = true;
			if (transform.position.x > left && goLeft) {
				transform.position += Vector3.left * 0.5f * Time.deltaTime;
			} else {
				goLeft = false;
				transform.position += Vector3.right * 0.5f * Time.deltaTime;
				if (transform.position.x > right) {
					goLeft = true;
				}
			}
		}

		if (shooting) {
			if (Time.time - shootTime > shottime) {
				fire ();
				shootTime = Time.time;
			}
			if (Time.time - delayShot > shottime) {
				delayFire ();
				delayShot = Time.time;
			}
		}
	}

	void fire() {
		float divider = Mathf.Sqrt (Mathf.Pow (playerX - enemyX, 2) + Mathf.Pow (playerY - enemyY , 2));
		BulletController shot = bullet.GetComponent<BulletController>();
		shot.setVelocity ((playerX - enemyX) / divider, (playerY - enemyY) / divider);
		shot.damage = damage + (int)(PlayerStatistics.maxHealth / 100f);
		bulletPos = new Vector2 (enemyX, enemyY);
		Instantiate (bullet, bulletPos, Quaternion.identity);
	}

	void delayFire() {
		float divider = Mathf.Sqrt (Mathf.Pow (playerX - enemyX, 2) + Mathf.Pow (playerY - enemyY , 2));
		BulletController shot = bullet2.GetComponent<BulletController>();
		shot.setVelocity ((playerX - enemyX) / divider, (playerY - enemyY) / divider);
		shot.damage = damage + (int)(PlayerStatistics.maxHealth / 100f);
		bulletPos = new Vector2 (enemyX, enemyY);
		shot.delayShot = false;
		Instantiate (bullet2, bulletPos, Quaternion.identity);
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.CompareTag ("Player")) {
			PlayerStatistics.takeDamage((damage / 2) + (PlayerStatistics.health / 100f));
			PlayerStatistics.takeDefDamage((damage / 2) + (PlayerStatistics.health / 100f));
		}
	}

	void OnTriggerStay2D(Collider2D other) {
		if (other.gameObject.CompareTag ("Player")) {
			PlayerStatistics.takeDamage((damage / 2) + (PlayerStatistics.health / 100f));
			PlayerStatistics.takeDefDamage((damage / 2) + (PlayerStatistics.health / 100f));
		}
	}
}
