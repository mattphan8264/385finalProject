using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour {

	/**
	 * This class controls enemy bullets. It controls how they move
	 * when to remove them, dealing damage, etc
	 **/

	//Public variables
	public float velocityX;
	public float velocityY;
	Rigidbody2D rb2d;
	public bool enemyUnit = true;
	public Transform target;
	public Animator anim;
	public bool explosion = false;
	public bool portal = false;

	public bool delayShot = true;
	public bool activate = false;

	public bool bossBullet = false;

	public int damage;
	void Awake() {
		target = GameObject.FindWithTag ("Player").transform;
	}
	// Declare rigid body for bullet
	void Start () {
		rb2d = this.GetComponent<Rigidbody2D>();
		transform.rotation = Quaternion.LookRotation (Vector3.forward, target.position - transform.position);
		if (portal) {
			if (target.position.x < transform.position.x) {
				transform.position = new Vector3 (transform.position.x - 0.1f, transform.position.y, 1);
			} else {
				transform.position = new Vector3 (transform.position.x + 0.1f, transform.position.y, 1);
			}
		}

		if (bossBullet) {
			Destroy (gameObject, 2f);
		}
	}
	
	//Add velocity to the bullet
	//If its an enemy shooting, remove after 4 seconds, if its a boss
	//Remove after 8 seconds
	void Update () {

		if (delayShot) {
			rb2d.velocity = new Vector2 (velocityX, velocityY) * 3;
		}

		if (!delayShot && !activate) {
			Invoke ("sendShot", 3f);
			activate = true;
		}

		if (!delayShot) {
			transform.rotation = Quaternion.LookRotation (Vector3.forward, target.position - transform.position);
		}
	}

	public void sendShot() {
		float divider = Mathf.Sqrt (Mathf.Pow (target.position.x - transform.position.x, 2) + Mathf.Pow (target.position.y -  transform.position.y , 2));
		setVelocity((target.position.x - transform.position.x) / divider, (target.position.y -  transform.position.y) / divider);
		delayShot = true;
	}

	//Set velocity
	public void setVelocity(float x, float y) {
		velocityX = x;
		velocityY = y;
	}

	//On collision with player do damage
	//On collision with ground dissapear
	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.CompareTag ("Player")) {
			float bulletDamage = PlayerStatistics.maxHealth / 200f;

			PlayerStatistics.takeDamage(bulletDamage + damage);
			PlayerStatistics.takeDefDamage (bulletDamage + damage);
			if (!explosion) {
				Destroy (gameObject);
			} else {
				Invoke ("remove", 1f);
				anim.SetBool ("hit", true);
				setVelocity (0, 0);
			}
		}
		if (other.gameObject.CompareTag ("Ground")) {
				if (!explosion) {
					Destroy (gameObject);
				} else {
					Invoke ("remove", 1f);
					anim.SetBool ("hit", true);
					setVelocity (0, 0);
				}
		}
	}

	void OnTriggerStay2D(Collider2D other) {
		if (other.gameObject.CompareTag ("Player")) {
			float bulletDamage = PlayerStatistics.maxHealth / 200f;

			PlayerStatistics.takeDamage(bulletDamage + damage);
			PlayerStatistics.takeDefDamage (bulletDamage + damage);
		}
	}

	void remove() {
		Destroy (gameObject);
	}
		
}
