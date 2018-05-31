using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalBoss3Controller : MonoBehaviour {

	private float playerX;
	private float enemyX;
	private float playerY;
	private float enemyY;
	public Transform target;
	public Animator anim;

	public GameObject bullet;
	public GameObject bullet2;
	public GameObject bullet3;
	Vector2 bulletPos;

	public float left;
	public float right;
	public float down;
	public float up;

	public bool goLeft = true;
	public bool goRight = false;
	public bool goDown = true;
	public bool goUp = false;

	public BossHealth myHealth;
	int baseDamage = 30;
	int damage = 200;
	public float increaseDamage;

	public float dropDown;
	public float stayDown;
	public bool stayDropped = false;

	public bool shooting = true;
	public float shootTime;
	public float sideShot;
	public float targetShoot;

	float timedown = 3f;
	float shot1Timer = 2f;

	void Awake() {
		target = GameObject.FindWithTag ("Player").transform;
		anim = GetComponent<Animator> ();
		myHealth = GetComponent<BossHealth> ();
	}

	// Use this for initialization
	void Start () {
		increaseDamage = Time.time;
		dropDown = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time - increaseDamage > 20f) {
			damage += baseDamage;
			increaseDamage = Time.time;
		}

		if (myHealth.currentHealth < myHealth.maxHealth / 2) {
			timedown = 2f;
			shot1Timer = 1.5f;
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
			} else if (Time.time - stayDown < timedown) {
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
			if (Time.time - shootTime > .5f) {
				fire ();
				shootTime = Time.time;
			}
		}

		if (Time.time - sideShot > shot1Timer) {
			sideFire ();
			sideShot = Time.time;
		}

		if (Time.time - targetShoot > shot1Timer) {
			targetShot ();
			targetShoot = Time.time;
		}
	}

	void fire() {
		float divider = Mathf.Sqrt (Mathf.Pow (playerX - enemyX, 2) + Mathf.Pow (playerY - enemyY , 2));
		BulletController shot = bullet3.GetComponent<BulletController>();
		shot.setVelocity ((playerX - enemyX) / divider, (playerY - enemyY) / divider);
		shot.damage = damage + (int)(PlayerStatistics.maxHealth / 100f);
		bulletPos = new Vector2 (enemyX, enemyY);
		Instantiate (bullet3, bulletPos, Quaternion.identity);
	}

	void sideFire() {
		BulletController shot = bullet2.GetComponent<BulletController>();
		shot.setVelocity (3, 0);
		shot.damage = damage + (int)(PlayerStatistics.maxHealth / 100f);
		bulletPos = new Vector2 (left, playerY);
		Instantiate (bullet2, bulletPos, Quaternion.identity);

		BulletController shot2 = bullet2.GetComponent<BulletController>();
		shot2.setVelocity (-3, 0);
		shot2.damage = damage + (int)(PlayerStatistics.maxHealth / 100f);
		bulletPos = new Vector2 (right, playerY);
		Instantiate (bullet2, bulletPos, Quaternion.identity);
	}

	void targetShot() {
		Instantiate (bullet, target.transform.position, Quaternion.identity);
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
