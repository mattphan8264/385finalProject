﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSpells : MonoBehaviour {

	//All spell objects
	public GameObject[] spells;
	public GameObject fireball;
	public GameObject lightning;
	public GameObject heal;
	public GameObject bomb;
	public GameObject flamethrower;
	public GameObject speed;

	public bool[] unlocked;
	public int selectedSpell;
	public int numberOfSpells;

	public int[] level;

	public Image currentSpell;
	public Image previousSpell;
	public Image nextSpell;
	public Text currentSpellLevel;
	public Text previousSpellLevel;
	public Text nextSpellLevel;

	//Saving
	public static bool[] saveUnlocked;
	public static int[] saveLevel;

	public void save() {
		saveUnlocked = unlocked;
		saveLevel = level;
	}

	public void load() {
		unlocked = saveUnlocked;
		level = saveLevel;
	}

	void loadSpells() {
		spells = new GameObject[numberOfSpells];
		spells [0] = fireball;
		spells [1] = lightning;
		spells [2] = heal;
		spells [3] = bomb;
		spells [4] = flamethrower;
		spells [5] = speed;
	}

	void Start () {
		numberOfSpells = 6;
		loadSpells ();
		unlocked = new bool[numberOfSpells];
		unlocked [0] = true;
		level = new int[numberOfSpells];

		currentSpell = GameObject.Find ("CurrentSpell").GetComponent<Image> ();
		previousSpell = GameObject.Find ("PreviousSpell").GetComponent<Image> ();
		nextSpell = GameObject.Find ("NextSpell").GetComponent<Image> ();
		currentSpellLevel = GameObject.Find ("CurrentSpell").GetComponentInChildren<Text> ();
		previousSpellLevel = GameObject.Find ("PreviousSpell").GetComponentInChildren<Text> ();
		nextSpellLevel = GameObject.Find ("NextSpell").GetComponentInChildren<Text> ();

		spellUIUpdate ();
	}

	public void addSpell(int input) {
		if (unlocked [input] == false) {
			unlocked [input] = true;
		} else {
			level [input]++;
		}
		spellUIUpdate ();
	}

	public void spellUIUpdate() {
		currentSpell.sprite = spells[selectedSpell].GetComponent<SpriteRenderer> ().sprite;
		currentSpellLevel.text = "Level " + level [selectedSpell];
		int plus = selectedSpell + 1;
		if (plus == numberOfSpells) {
			plus = 0;
		}
		int minus = selectedSpell - 1;
		if (minus == -1) {
			minus = numberOfSpells - 1;
		}
		while (unlocked [plus] != true) {
			plus++;
			if (plus == numberOfSpells) {
				plus = 0;
			}
		}
		while (unlocked [minus] != true) {
			minus--;
			if (minus == -1) {
				minus = numberOfSpells - 1;
			}
		}
		nextSpell.sprite = spells[plus].GetComponent<SpriteRenderer> ().sprite;
		previousSpell.sprite = spells[minus].GetComponent<SpriteRenderer> ().sprite;
		nextSpellLevel.text = "Level " + level [plus];
		previousSpellLevel.text  = "Level " + level [minus];

	}
	
	public void useSpell() {
		if (unlocked [selectedSpell] == true) {
			if (selectedSpell == 0) {
				fireballSpell ();
			}
			if (selectedSpell == 1) {
				lightningSpell ();
			}
			if (selectedSpell == 2) {
				healingSpell ();
			}
			if (selectedSpell == 3) {
				bombSpell ();
			}
			if (selectedSpell == 4) {
				flamethrowerSpell ();
			}
			if (selectedSpell == 5) {
				speedSpell ();
			}
		}
	}

	public void fireballSpell() {
		if (PlayerStatistics.mana >= 1) {
			PlayerStatistics.mana -= 1;
		
			Vector2 cursorL = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			float divider = Mathf.Sqrt (Mathf.Pow (cursorL.x - transform.position.x, 2) + Mathf.Pow (cursorL.y - transform.position.y, 2));
			FireBallController shot = fireball.GetComponent<FireBallController> ();
			shot.modifiedDamage = shot.baseDamage + (level [selectedSpell] * 10);
			shot.setVelocity ((cursorL.x - transform.position.x) / divider, (cursorL.y - transform.position.y) / divider);
			float angle = Mathf.Atan2 (transform.position.x - cursorL.x, cursorL.y - transform.position.y) * Mathf.Rad2Deg;
			Instantiate (fireball, transform.position, Quaternion.Euler (new Vector3 (0, 0, angle)));
		}
	}

	public void lightningSpell() {
		if (PlayerStatistics.mana >= 1) {
			PlayerStatistics.mana -= 1;

			Vector2 cursorL = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			LightningController shot = lightning.GetComponent<LightningController> ();
			shot.modifiedDamage = shot.baseDamage + (level [selectedSpell] * 10);
			shot.setVelocity (0, -1f);
			Instantiate (lightning, cursorL, Quaternion.identity);
		}
	}

	public void healingSpell() {
		if (PlayerStatistics.mana >= 2) {
			PlayerStatistics.mana -= 2;
			HealingController shot = heal.GetComponent<HealingController> ();
			shot.modifiedHealing = shot.baseHealing + level [selectedSpell];
			Instantiate (heal, transform.position, Quaternion.identity);
		}
	}

	public void bombSpell() {
		if (PlayerStatistics.mana >= 1) {
			PlayerStatistics.mana -= 1;

			Vector2 cursorL = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			float divider = Mathf.Sqrt (Mathf.Pow (cursorL.x - transform.position.x, 2) + Mathf.Pow (cursorL.y - transform.position.y, 2));
			BombController shot = bomb.GetComponent<BombController> ();
			shot.modifiedDamage = shot.baseDamage + level [selectedSpell] * 5;
			shot.setVelocity ((cursorL.x - transform.position.x) / divider, (cursorL.y - transform.position.y) / divider);
			float angle = Mathf.Atan2 (transform.position.x - cursorL.x, cursorL.y - transform.position.y) * Mathf.Rad2Deg;
			Instantiate (bomb, transform.position, Quaternion.Euler (new Vector3 (0, 0, angle)));


		}
	}

	public void flamethrowerSpell() {
		if (PlayerStatistics.mana >= .5f) {
			PlayerStatistics.mana -= .5f;

			Vector2 cursorL = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			float divider = Mathf.Sqrt (Mathf.Pow (cursorL.x - transform.position.x, 2) + Mathf.Pow (cursorL.y - transform.position.y, 2));
			FlamethrowerController shot = flamethrower.GetComponent<FlamethrowerController> ();
			shot.modifiedDamage = shot.baseDamage + level [selectedSpell] * 5;
			shot.setVelocity ((cursorL.x - transform.position.x) / divider, (cursorL.y - transform.position.y) / divider);
			float angle = Mathf.Atan2 (transform.position.x - cursorL.x, cursorL.y - transform.position.y) * Mathf.Rad2Deg;
			Instantiate (flamethrower, transform.position, Quaternion.Euler (new Vector3 (0, 0, angle)));

			Player temp = GameObject.Find ("Player").GetComponent<Player> ();
			temp.spellTime = Time.time - .3f;
		}
	}

	public void speedSpell() {
		if (PlayerStatistics.mana >= 1) {
			PlayerStatistics.mana -= 1;
			Instantiate (speed, transform.position, Quaternion.identity);
		}
	}

	public void increase() {
		if (selectedSpell == numberOfSpells - 1) {
			selectedSpell = 0;
			while (unlocked [selectedSpell] == false) {
				selectedSpell++;
				if (selectedSpell == numberOfSpells) {
					selectedSpell = 0;
				}
			}
		} else {
			selectedSpell++;
			while (unlocked [selectedSpell] == false) {
				selectedSpell++;
				if (selectedSpell == numberOfSpells) {
					selectedSpell = 0;
				}
			}
		}
		spellUIUpdate ();
	}

	public void decrease() {
		if (selectedSpell == 0) {
			selectedSpell = numberOfSpells - 1;
			while (unlocked [selectedSpell] == false) {
				selectedSpell--;
				if (selectedSpell == -1) {
					selectedSpell = numberOfSpells - 1;
				}
			}
		} else {
			selectedSpell--;
			while (unlocked [selectedSpell] == false) {
				selectedSpell--;
				if (selectedSpell == -1) {
					selectedSpell = numberOfSpells - 1;
				}
			}
		}
		spellUIUpdate ();
	}


	void Update () {
		
	}
}