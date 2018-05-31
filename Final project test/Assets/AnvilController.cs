using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnvilController : MonoBehaviour {

	public TextMesh control;

	// Use this for initialization
	void Start () {
		control = GetComponentInChildren<TextMesh> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.CompareTag ("Player")) {
			control.text = "Press U in inventory \nto upgrade equipment\nPress Y in inventory \nto reroll equipment";
		}
	}

	void OnTriggerExit2D(Collider2D other) {
		if (other.gameObject.CompareTag ("Player")) {
			control.text  = "";
		}
	}
}
