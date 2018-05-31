using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cooldowns : MonoBehaviour {

	public Text health;
	public Text mana;
	public Text exp;
	public Text coin;

	Player temp;

	// Use this for initialization
	void Start () {
		temp = GameObject.Find ("Player").GetComponent<Player>();
	}
	
	// Update is called once per frame
	void Update () {
		float healthtime = Time.time - PlayerStatistics.healthPotTimer;
		float manatime = Time.time - PlayerStatistics.manaPotTimer;
		if (healthtime < 0 || healthtime > 3f) {
			healthtime = 0;
		}
		if (manatime < 0 || manatime > 3f) {
			manatime = 0;
		}
		health.text = "Health Potion cooldown: " + (int)healthtime;
		mana.text = "Mana Potion cooldown: " + (int)manatime;
		if (temp.expBoost) {
			exp.text = "Exp Boost active for: " + (int)(Time.time - temp.expTime);
		} else {
			exp.text = "Exp Boost not active";
		}
		if (temp.goldBoost) {
			coin.text = "Gold Boost active for: " + (int)(Time.time - temp.goldTime);
		} else {
			coin.text = "Gold Boost not active";
		}
	}
}
