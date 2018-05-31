using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureChestController : MonoBehaviour {
	bool near = false;
	public Transform target;
	public TextMesh control;
	public Animator anim;

	public bool opened = false;

	public GameObject item;
	public int numberHeld = 1;

	public float countdownTime = 3f;
	public float startTime;

	void Awake() {
		target = GameObject.FindWithTag ("Player").transform;
	}

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
		startTime = 3f;

		numberHeld = Random.Range (0, numberHeld + 1);
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey (KeyCode.F)) {
			if (near && !opened) {
				anim.SetBool ("Open", true);
				for (int i = 0; i <= Random.Range(1, 5); i++) {
					Instantiate (item, new Vector3 ((transform.position.x + Random.Range(-.3f, .3f)), transform.position.y, 0), 
						Quaternion.identity);
				}
				opened = true;
				control.text = "";
			}
		}
	}

	void FixedUpdate() {
		if (opened) {
			float alpha = 1 / startTime * countdownTime;
			Color tempColor = GetComponent<SpriteRenderer> ().color;
			tempColor.a = alpha;
			GetComponent<SpriteRenderer> ().color = tempColor;
			countdownTime -= Time.deltaTime;
			Invoke ("destroy", 5f);
		}
	}

	void destroy() {
		Destroy(gameObject);
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.CompareTag ("Player")) {
			near = true;
			if (!opened) {
				control.text = "F";
			}
		}
	}

	void OnTriggerExit2D(Collider2D other) {
		if (other.gameObject.CompareTag ("Player")) {
			near = false;
			control.text  = "";
		}
	}
}
