﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GasController : MonoBehaviour {
	public float gasTime = 4f;
	public bool inGas = false;
	// Use this for initialization
	void Start () {
		Destroy (gameObject, gasTime);
	}
	
	// Update is called once per frame
	void Update () {
		if (inGas) {
			if (PlayerStatistics.level <= 5) {
				PlayerStatistics.takeDamage (2 + PlayerStatistics.level / 5);
			} else if (PlayerStatistics.level <= 10) {
				PlayerStatistics.takeDamage (2 + PlayerStatistics.level / 3);
			} else {
				PlayerStatistics.takeDamage (2 + PlayerStatistics.level / 2);
			}
		}
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.CompareTag ("Player")) {
			inGas = true;
		}
	}

	void OnTriggerExit2D(Collider2D other) {
		if (other.gameObject.CompareTag ("Player")) {
			inGas = false;
		}
	}
}
