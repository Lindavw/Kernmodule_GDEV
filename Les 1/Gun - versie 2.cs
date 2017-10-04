using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Gun : MonoBehaviour {

	public bool isAutomatic;
	public float damage = 10f;
	public float range = 100f;
	public float fireRate = 15f;

	public Camera fpsCam;
	public ParticleSystem muzzleFlash;
	public GameObject impactEffect;

	public int maxAmmo = 10;
	public float reloadTime = 1f;
    private int currentAmmo;
    private bool isReloading = false;
    private float nextTimeToFire = 0f;

	public Text ammoText;
	public Animator animator;

	public AudioClip shootAudio;
	public AudioClip reloadAudio;

	private void Start(){

		currentAmmo = maxAmmo;
	}

    private void OnEnable(){

		isReloading = false;
		animator.SetBool ("Reloading", false);
	}

    private void Update() {

		ammoText.text = currentAmmo.ToString("f0") + ("/") + maxAmmo.ToString("f0");

        if (isReloading) {
            return;
        }

		if (currentAmmo <= 0 || Input.GetKeyDown("r")) {
			StartCoroutine(Reload());
			AudioManager.instance.PlaySound (reloadAudio, transform.position);
			return;
		}

		if (isAutomatic == false) {
			if (Input.GetButtonDown ("Fire1") && Time.time >= nextTimeToFire) {
				nextTimeToFire = Time.time + 1f / fireRate;
				Shoot ();
			}
		}

		if (isAutomatic == true) {
			if (Input.GetButton ("Fire1") && Time.time >= nextTimeToFire) {
				nextTimeToFire = Time.time + 1f / fireRate;
				Shoot ();
			}
		}

	}

    private IEnumerator Reload(){

		isReloading = true;
	
		animator.SetBool ("Reloading", true);

		yield return new WaitForSeconds (reloadTime - .25f);
		animator.SetBool ("Reloading", false);
		yield return new WaitForSeconds (.25f);

		currentAmmo = maxAmmo;
		isReloading = false;
	}

    public void Shoot(){

		currentAmmo--;
		
		muzzleFlash.Play ();

		RaycastHit hit;

		if (Physics.Raycast (fpsCam.transform.position, fpsCam.transform.forward, out hit, range)) {
			Enemy enemy = hit.transform.GetComponent<Enemy>();
			if (enemy != null) 
			{
				enemy.TakeDamage (damage);
			}

			GameObject impactGO = Instantiate(impactEffect, hit.point, Quaternion.LookRotation (hit.normal));
			Destroy (impactGO, 2f);
		}

		AudioManager.instance.PlaySound (shootAudio, transform.position);
	}
}
