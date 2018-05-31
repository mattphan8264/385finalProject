using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerStatistics  : MonoBehaviour
{
	public static float coins = 50;

	public static float lastHit = 0;
	public static float lastDefHit = 0;

	public static float baseHealth = 125;
	// current hp
	public static float health = 125;
	// maximum health points
	public static float maxHealth = 125;

	public static float baseMana = 10;
	// current mp
	public static float mana = 10;
	// maximum mana points
	public static float maxMana = 10;

	// speed of the character
	//public static float speed;
	// rate at which the character jumps
	//public static float jumpSpeed;

	// ability stats
	// stats manipulate abilities and modifiers on weapons, skills, etc.

	// stats should be able to be rese t and reallocated through
	// earned in game currency (maybe increasing price up to fixed amount)
	public static float atk = 0;
	public static float matk = 0;
	// strength modifies all physical weapon attack damage, increases health
	// crit damage
	public static float str = 1;
	// dexterity modifies accuracy and avoidability
	public static float dex = 1;
	// increases magic attack, mana points
	public static float wis = 1;
	// increases chances of drops, exp gained
	// crit chance
	public static float luk = 1;

	// all weapons do some base damage when physically swung based on this stat
	public static float wa = 1;
	// skills and effects will scale off of magic attack
	public static float ma = 1;

	// physical defense reduces damage taken by %
	public static float def = 1;


	// the rate at which you hit the enemy (enemy avoid - your accuracy)
	public static float acc;
	// the rate at which enemies hit you (your avoid - enemy accuracy)
	public static float avo;

	// crit chance
	public static float cc = 5;

	public static float cd = 50;

	// exp as a total of all the exp the character has EVER gained
	public static float exp = 0;

	// ignore enemy defense% (enemy defense * (ied / 100))
	public static float ied = 30;

	public static float level = 1;
	// exp needed to reach next level
	public static float nextLevel = 10;

	public float manaRegen = 0;
	public bool updateTextIcon = false;

	public static float healthPotTimer;
	public static float manaPotTimer;

	// everyone starts with 5 stat points
	public static float statPoints = 10;

	public LevelText text;

	void Start() {
		text = GameObject.Find ("LevelText").GetComponent<LevelText> ();
		text.updateLevel ();
	}

	public static void reset() {
		exp = 0;
		coins = 50;
		str = 1;
		dex = 1;
		wis = 1;
		luk = 1;
		atk = 0;
		matk = 0;
		def = 1;
		mana = baseMana;
		health = baseHealth;
		level = 1;
		statPoints = 10;
	}

	public static void load() {
		maxHealth = baseHealth + (3 * str);
		maxMana = baseMana + (float)(int)(wis / 4);
		health = maxHealth;
		mana = maxMana;
	}

	public static float calcPDSword() {
		float crit = UnityEngine.Random.Range (0, 101);
		float dam = atk * 1.5f;
		if (cc > crit) {
			dam = atk * (1 + (cd / 100));
		}
		return dam;
	}

	public static float calcPDSpear() {
		float crit = UnityEngine.Random.Range (0, 101);
		float dam = atk * 1.5f;
		if (cc > crit) {
			dam = atk * (1 + (cd / 100));
		}
		return dam / .75f;
	}

	public static float calcPDAxe() {
		float crit = UnityEngine.Random.Range (0, 101);
		float dam = atk * 1.5f;
		if (cc > crit) {
			dam = atk * (1 + (cd / 100));
		}
		return dam * 1.5f;
	}

	public static float calcPDDagger() {
		float crit = UnityEngine.Random.Range (0, 101);
		float dam = atk * 1.5f;
		if (cc > crit) {
			dam = atk * (1 + (cd / 100));
		}
		return dam / 2f;
	}
	public static float calcMD() {
		float crit = UnityEngine.Random.Range (0, 101);
		float dam = matk * 2.5f;
		if (cc > crit) {
			dam = matk * (1 + (cd / 100));
		}
		return dam;
	}

	public static void takeDamage(float damage) {

		if (Time.time - lastHit >= .3f) {
			if (def < 0) {
				int avoid = UnityEngine.Random.Range (0, 100);
				if (avoid > avo) {
					float damageIncrease = Mathf.Abs (def) / 25;
					damageIncrease *= 5;
					health -= (damage / 100) * (100 + damageIncrease);
					if (health <= 0) {
						health = 0;
					}
					lastHit = Time.time;
					health = (float)(int)health;
				}
			} else {
				int avoid = UnityEngine.Random.Range (0, 100);
				if (avoid > avo) {
					float damageReduction = def / 60;
					damageReduction *= 3.5f;
					float temp = (damage / 100) * (100 - damageReduction);
					if (temp <= 0) {

					} else {
						health -= temp;
					}
					if (health <= 0) {
						health = 0;
					}
					lastHit = Time.time;
					health = (float)(int)health;
				}
			}
		}
	}

	public static void takeDefDamage(float damage) {
		if (Time.time - lastDefHit >= .3f) {
			int avoid = UnityEngine.Random.Range (0, 100);
			if (avoid > (avo / 2)) {
				health -= damage;
				if (health <= 0) {
					health = 0;
				}
				lastDefHit = Time.time;
				health = (float)(int)health;
			}
		}
	}

	// put in stat points
	public static void aStr()
	{
		if (statPoints >= 1)
		{
			statPoints -= 1;
			str += 1;
		}
	}

	public static void aDex()
	{
		if (statPoints >= 1)
		{
			statPoints -= 1;
			dex += 1;
		}
	}

	public static void aInt()
	{
		if (statPoints >= 1)
		{
			statPoints -= 1;
			wis += 1;
		}
	}

	public static void aLuk()
	{
		if (statPoints >= 1)
		{
			statPoints -= 1;
			luk += 1;
		}
	}



	private void Update()
	{
		if (maxMana > 75) {
			if (Time.time - manaRegen > 4) {
				float manaAdd = maxMana / 12;
				if (mana + manaAdd > maxMana) {
					mana = maxMana;
				} else {
					mana += (float)(int)manaAdd;
				}
				manaRegen = Time.time;
			}
		}
		//
		maxHealth = baseHealth + (3 * str);
		if (maxHealth <= 1) {
			maxHealth = 1;
		}
		if (health > maxHealth) {
			health = maxHealth;
		}
		maxMana = (float)(int)(baseMana + (float)(int)(wis / 4));
		if (maxMana <= 1) {
			maxMana = 1;
		}
		if (mana > maxMana) {
			mana = maxMana;
		}

		float natk = (float)((str * 0.1) + (dex * 0.3) + (float)(int)(wa / 3));
		if (natk <= 0) {
			atk = 1;
		} else {
			atk = natk;
		}
			
		float nmatk = (float)(int)wa / 3 + (float)(wis * 1 / 3);
		if (nmatk <= 0) {
			matk = 1;
		} else {
			matk = nmatk;
		}
			
		float ncc = 5 + (float)(luk * 0.3);
		if (ncc > 100) {
			cc = 100;
		} else {
			cc = ncc;
		}

		// enemy avo - acc is hit chance
		acc = 90 + (float)(dex * 0.5);

		// chance to get hit
		float nAvo = 0 + (float)((dex * 0.25) + (luk * 0.4));
		if(nAvo > 40){
			avo = 40;
		}
		else{
			avo = nAvo;
		}
		// ied
		ied = 30 + (float)(luk * 0.05);
		//crit damage
		cd = 30 + (float)((dex * 0.1) + (luk * 0.45));

		// check if exp reached
		if(exp >= nextLevel)
		{
			health = maxHealth;
			mana = maxMana;
			// next level reached
			exp -= nextLevel;

			if (level <= 5) {
				nextLevel *= 1.3f;
			} else if (level <= 15) {
				nextLevel *= 1.4f;
			} else {
				nextLevel *= 1.5f;
			}
			nextLevel = (float)(int)nextLevel + 1;
			// more stat points
			statPoints += 7;
			level++;
			text.updateLevel ();
		}
	}
}
