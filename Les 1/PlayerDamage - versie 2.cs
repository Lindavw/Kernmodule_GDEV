using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDamage : MonoBehaviour {

	public Text healthPercentage;
	public Image healthBarP;

	public float startHealthP = 100f;
	public float playerHealth;

	public float packHealth;

	public PauseMenu pauseMenu;

	private void Start() {

		healthPercentage.text = ("100%");
		playerHealth = startHealthP;
	}

	private void Update() {

		healthBarP.fillAmount = playerHealth / startHealthP;
		healthPercentage.text = playerHealth.ToString("f0") + ("%");
	}

	private void OnTriggerEnter(Collider other) {
		
		if (playerHealth <= startHealthP - packHealth) {
			if (other.tag == "Health") {
				playerHealth += packHealth;
				Destroy (other.gameObject);
				AudioManager.instance.PlaySound ("HealthPack", transform.position);
			}
		}
	}

	public void TakeDamage(float damount) {

		AudioManager.instance.PlaySound ("PlayerDamaged", transform.position);
		playerHealth -= damount;

		if (playerHealth <= 0f) {
			healthPercentage.text = ("0%");
			playerHealth = 0;
			Die ();
		}
	}

	private void Die() {

		pauseMenu.DeathMenu ();
	}
}
