using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PortalController : MonoBehaviour {
	public int nextScene;
	bool near = false;
	public Transform target;
	Player player;
	public TextMesh control;
	public PlayerMusic playermusic;

	public bool finalBoss = false;
	public bool boss = false;

	public bool finishTutorial = false;
	public bool tutorial = false;

	public bool sent = false;

	public bool testing = false;
	public int test;

	void Awake() {
		target = GameObject.FindWithTag ("Player").transform;
		player = target.GetComponent<Player> ();
		playermusic = GameObject.Find ("Audio").GetComponent<PlayerMusic> ();
	}

	void Start () {
		control = GetComponentInChildren<TextMesh> ();
	}
		
	void Update () {
		
		if (Input.GetKey(KeyCode.W)) {
			if (Time.timeScale != 0) {
				if (near) {
					player.resetKills ();
					if (player.dupeSpeed) {
						player.dupeSpeed = false;
						player.speed /= 1.5f;
						player.jumpSpeed /= 1.1f;
					}

					if (tutorial) {
						if (!finishTutorial) {
							SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex + 1, LoadSceneMode.Single);
						} else {
							player.delete ();
							PlayerStatistics.reset ();
							SceneManager.LoadScene (1, LoadSceneMode.Single);
						}
					} else {

						if (!finalBoss) {
						
							if (player.stageCount == 4) {
								if (!sent) {
									sent = true;
									SceneManager.LoadScene (Random.Range (4, 8), LoadSceneMode.Single);
									playermusic.BM ();
								}
							} else if (player.stageCount == 5) {
								if (!sent) {
									sent = true;
									SceneManager.LoadScene (1, LoadSceneMode.Single);
									playermusic.TM ();
								}
							} else {
								if (!sent) {
									sent = true;
									if (!testing) {
										int next = Random.Range (8, 25);
										SceneManager.LoadScene (next, LoadSceneMode.Single);
									} else {
										SceneManager.LoadScene (test, LoadSceneMode.Single);
									}
									if (player.stageCount == 0) {
										playermusic.DM ();
									}
								}
							}
						} else {
							if (PlayerStatistics.level >= 20) {
								SceneManager.LoadScene (nextScene, LoadSceneMode.Single);
								playermusic.BM ();
							}
						}
					}
					player.anim.SetBool ("attacking", false);
					player.anim.SetBool ("walking", false);
				}
			}
		}
	}

	public void deactivate() {
		this.gameObject.SetActive (false);
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.CompareTag ("Player")) {
			near = true;
			if (!boss) {
				control.text = "W";
			} else {
				control.text = "W \nFight the final boss\nRequires level 20 or above";
			}
		}
	}

	void OnTriggerExit2D(Collider2D other) {
		if (other.gameObject.CompareTag ("Player")) {
			near = false;
			control.text  = "";
		}
	}

	public void setNextScene(int input) {
		nextScene = input;
	}
}
