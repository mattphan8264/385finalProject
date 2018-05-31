using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour {
	//Resources
	public PlayerResources resources;
	public PlayerSpells spells;

	int count;
	public Text coinText;
	public bool door = false;
	public int stageCount = 0;

	//Awake() variables
	public static Player Instance;
	public Image healthbar;
	public Image manabar;
	public Image expbar;
	public Text healthText;
	public Text manaText;
	public Text expText;
	Transform spawn;

	//Start() variables
	Rigidbody2D rb2d;
	public Animator anim;

	//Update() variables
	public static bool loadedChar = false;
	public float loadTime = 0;
	public bool setLoadTime = false;
	public Transform groundCheck;
	public float groundCheckRadius;
	public LayerMask groundLayer;
	public bool onGround;
	public bool facing = false;
	public float speed;
	public float jumpSpeed;
	public bool attacking = false;
	int wepState = 1;
	bool created = true;
	bool jumpDown = false;
	public CanvasController playerCanvas;
	public float spellTime;
	public bool dupeSpeed;

	public bool location = false;

	//Taking damage variables
	public float lastHit;

	//KillCount
	public int killCount;
	public bool killedBoss = false;

	// For Climbing
	public bool onLadder = false;
	public float climbSpeed;
	private float climbVelocity;
	private float gravityStore;

	//Inventory
	public Inventory inv;
	public PotionInventory pInv;
	public Equipment eInv;
	public bool tryingToDelete = false;

	//Time stops
	public bool menu = false;
	public bool inventory = false;
	public bool shop = false;
	public bool timeStop;

	//Buffs
	public bool goldBoost = false;
	public bool expBoost = false;
	public float goldTime;
	public float expTime;

	public bool jumping;
	//Location
	float x;
	float y;

	public TextMesh text;
	public bool upgradeAvailable = false;

	public Image loadingScreen;

	void Awake() {
		if (Instance == null) {
			DontDestroyOnLoad (gameObject);
			Instance = this;
		} else if (Instance != this) {
			Destroy (gameObject);
		}
		PlayerStatistics.load ();

		//Player UI
		playerCanvas = GameObject.Find ("Base Player UI").GetComponent<CanvasController> ();
		healthbar = GameObject.Find ("Health").GetComponent<Image> ();
		manabar = GameObject.Find ("Mana").GetComponent<Image> ();
		expbar = GameObject.Find ("Exp").GetComponent<Image> ();
		healthText = GameObject.Find ("Health").GetComponentInChildren<Text> ();
		manaText = GameObject.Find ("Mana").GetComponentInChildren<Text> ();
		expText = GameObject.Find ("Exp").GetComponentInChildren<Text> ();

		//Player weapons and spells
		resources = GetComponent<PlayerResources> ();
		spells = GetComponent<PlayerSpells> ();
		count = resources.weaponCount;


		//Player inventory UI
		inv = GameObject.Find ("Inventory").GetComponent<Inventory> ();
		pInv = GameObject.Find ("InventoryP").GetComponent<PotionInventory> ();
		eInv = GameObject.Find ("EquipmentInventory").GetComponent<Equipment> ();
	}

	public void getSpawnLocation(float x, float y) {
		this.x = x;
		this.y = y;
		transform.position = new Vector3(x, y, 0);
		healthbar = GameObject.Find ("Health").GetComponent<Image> ();
		manabar = GameObject.Find ("Mana").GetComponent<Image> ();
		expbar = GameObject.Find ("Exp").GetComponent<Image> ();
		resources = GetComponent<PlayerResources> ();
		count = resources.weaponCount;

		coinText.text = PlayerStatistics.coins.ToString ();

	}

	public void addItem(int input) {
		inv.pickUpItem (input);
	}

	public void addItem(ItemStats inputStats) {
		inv.AddItem (inputStats);
	}

	public void addPotion(int input) {
		pInv.AddItem (input);
	}

	public void addEquipment(Item input, int slot, bool equipped) {
		eInv.AddItem (input, slot, equipped);
	}

	public void addEquipment(Item input, int slot) {
		eInv.AddItem (input, slot);
	}

	public void addSpell(int input) {
		spells.addSpell (input);
	}
		
	//
	void Start () {
		rb2d = this.GetComponent<Rigidbody2D> ();
		anim = GetComponent<Animator> ();
		killCount = 0;
		gravityStore = rb2d.gravityScale;

		if (!loadedChar) {
			Destroy (loadingScreen);
		}
		//GetComponent<SpriteRenderer> ().color = Color.blue;
	}
		
	void Update () {
		timeStop = (menu || inventory || shop);
		if (loadedChar && !setLoadTime) {
			loadTime = Time.time;
			setLoadTime = true;
		}
		if (loadedChar && Time.time - loadTime > 3f) {
			LoadPlayer load = new LoadPlayer ();
			load.Load (this);
			loadedChar = false;
			spells.load ();
			PlayerStatistics temp = GetComponent<PlayerStatistics> ();
			temp.text.updateLevel ();
			Destroy (loadingScreen);
		}

		if (location == false) {
			transform.position = new Vector2 (x, y);
		}

		if (Time.time - goldTime > 70) {
			goldBoost = false;
		}
		if (Time.time - expTime > 70) {
			expBoost = false;
		}

		coinText.text = PlayerStatistics.coins.ToString ();
		if (created == true) {
			for (int i = 1; i < count; i++) {
				resources.setActiveFalse (i);
			}

			created = false;
		}
		healthbar.fillAmount = PlayerStatistics.health / PlayerStatistics.maxHealth;
		manabar.fillAmount = PlayerStatistics.mana / PlayerStatistics.maxMana;
		expbar.fillAmount = PlayerStatistics.exp / PlayerStatistics.nextLevel;
		healthText.text = PlayerStatistics.health + "/" + PlayerStatistics.maxHealth;
		manaText.text = PlayerStatistics.mana + "/" + PlayerStatistics.maxMana;
		expText.text = PlayerStatistics.exp + "/" + PlayerStatistics.nextLevel;
		if (PlayerStatistics.health <= 0) {
			if (facing) {
				flip ();
			}
			resources.setArmorOff ();
			anim.SetBool ("walking", false);
			anim.SetBool ("attacking", false);
			anim.SetBool ("dead", true);

			text.text = "Press R to respawn";
			if (Input.GetKeyDown (KeyCode.R)) {
				SceneManager.LoadScene (1);
				resources.setArmorOn ();
				PlayerStatistics.coins /= 2;
				PlayerStatistics.coins = (float)(int)PlayerStatistics.coins;
				PlayerStatistics.exp = 0;
				anim.SetBool ("dead", false);
				text.text = "";
				playerReset ();
				GetComponentInChildren<PlayerMusic> ().TM ();
			}
		} else {
			if (!loadedChar) {
				anim.SetInteger ("weapon state", wepState);
				anim.SetInteger ("weapon equipped", resources.wepEQPD);
				onGround = Physics2D.OverlapCircle (groundCheck.position, groundCheckRadius, groundLayer);
				if (!timeStop) {
					if (onGround) {
						jumping = false;
					}
					if (Input.GetMouseButton (0)) {
						anim.SetBool ("attacking", true);
						attacking = true;
					}
					if (Input.GetMouseButtonUp (0)) {
						anim.SetBool ("attacking", false);
						attacking = false;
					}

					if (Input.GetKeyDown (KeyCode.Space) && onGround && !jumpDown && !Input.GetKey (KeyCode.S)) {
						rb2d.gravityScale = gravityStore;
						rb2d.velocity = new Vector2 (rb2d.velocity.x, jumpSpeed);
						jumping = true;
					}

					if (Input.GetKeyDown (KeyCode.S)) {
						anim.SetBool ("crouching", true);
					}
					if (Input.GetKeyUp (KeyCode.S)) {
						anim.SetBool ("crouching", false);
					}

					if (Input.GetMouseButton (1)) {
						if (Time.time - spellTime > .5f) {
							spellTime = Time.time;
							spells.useSpell ();
						}
					}

					float wheel = Input.GetAxis ("Mouse ScrollWheel");
					if (wheel > 0f) {
						changeWeapon (true);
					} else if (wheel < 0f) {
						changeWeapon (false);
					}

					if (Input.GetMouseButtonDown (2)) {
						changeWeapon (true);
					}

					if (Input.GetKeyDown (KeyCode.Q)) {
						spells.decrease ();
					}

					if (Input.GetKeyDown (KeyCode.E)) {
						spells.increase ();
					}

					if (Input.GetKeyUp (KeyCode.A)) {
						anim.SetBool ("walking", false);
					}
					if (Input.GetKeyUp (KeyCode.D)) {
						anim.SetBool ("walking", false);
					}

					if (Input.GetKeyDown (KeyCode.W)) {
						jumping = false;
					}
				}

				if (onLadder) {
					if (Input.GetKeyDown (KeyCode.Space)) {
						rb2d.gravityScale = gravityStore;
						rb2d.velocity = new Vector2 (rb2d.velocity.x, jumpSpeed);
						jumping = true;
					} 
					if (!jumping) {
						rb2d.gravityScale = 0f;
						climbVelocity = climbSpeed * Input.GetAxisRaw ("Vertical");

						rb2d.velocity = new Vector2 (0, climbVelocity);
					}
				}

				if (!onLadder) {
					rb2d.gravityScale = gravityStore;
				}
			}
		}
	}

	void FixedUpdate() {
		if (!timeStop && (anim.GetBool("dead") != true)) {
			if (Input.GetKey (KeyCode.A)) {
				anim.SetBool ("walking", true);
				if (!facing) {
					flip ();
				}
				transform.position += Vector3.left * speed * Time.deltaTime;
			}
			if (Input.GetKey (KeyCode.D)) {
				anim.SetBool ("walking", true);
				if (facing) {
					flip ();
				}
				transform.position += Vector3.right * speed * Time.deltaTime;
			}
		}
	}

	public void playerReset() {
		PlayerStatistics.health = PlayerStatistics.maxHealth;
		PlayerStatistics.mana = PlayerStatistics.maxMana;
		stageCount = 0;
	}

	public void jumpingDown() {
			jumpDown = !jumpDown;
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.CompareTag("playeroutofbounds")) {
			location = true;
		}
		if (other.gameObject.CompareTag ("Anvil")) {
			upgradeAvailable = true;
		}
	}
		
	void OnTriggerStay2D(Collider2D other) {
		if (other.gameObject.CompareTag("playeroutofbounds")) {
			location = true;
		}
	}

	void OnTriggerExit2D(Collider2D other) {
		if (other.gameObject.CompareTag("playeroutofbounds")) {
			location = false;
		}
		if (other.gameObject.CompareTag ("Anvil")) {
			upgradeAvailable = false;
		}
	}

	void changeWeapon(bool up) {
		if (up) {
			if (resources.wepEQPD < 4) {
				resources.setActiveFalse (resources.wepEQPD - 1);
				resources.wepEQPD++;
				anim.SetInteger ("weapon equipped", resources.wepEQPD);
				resources.setActiveTrue (resources.wepEQPD - 1);
				resources.UIChange ();
			} else {
				resources.setActiveFalse (resources.wepEQPD - 1);
				resources.wepEQPD = 1;
				anim.SetInteger ("weapon equipped", resources.wepEQPD);
				resources.setActiveTrue (resources.wepEQPD - 1);
				resources.UIChange ();
			}
		} else {
			if (resources.wepEQPD > 1) {
				resources.setActiveFalse (resources.wepEQPD - 1);
				resources.wepEQPD--;
				anim.SetInteger ("weapon equipped", resources.wepEQPD);
				resources.setActiveTrue (resources.wepEQPD - 1);
				resources.UIChange ();
			} else {
				resources.setActiveFalse (resources.wepEQPD - 1);
				resources.wepEQPD = 4;
				anim.SetInteger ("weapon equipped", resources.wepEQPD);
				resources.setActiveTrue (resources.wepEQPD - 1);
				resources.UIChange ();
			}

		}
		if (resources.wepEQPD == 1 || resources.wepEQPD == 3 || resources.wepEQPD == 4) {
			wepState = 1;
			anim.SetInteger ("weapon state", wepState);
		} else {
			wepState = 2;
			anim.SetInteger ("weapon state", wepState);
		}
	}

		void flip() {
			facing = !facing;
			Vector3 charscale = transform.localScale;
			charscale.x *= -1;
			transform.localScale = charscale;
		}

		public void resetKills() {
				killCount = 0;
				killedBoss = false;
		}

		public void delete() {
			Destroy (gameObject);
		}


		
}
	
	
	
