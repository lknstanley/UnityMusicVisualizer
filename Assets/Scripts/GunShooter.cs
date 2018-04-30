using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunShooter : MonoBehaviour {

	[Header ("Particle System References")]
	public ParticleSystem normalParticle;
	public ParticleSystem shotGunParticle;
	public ParticleSystem machineGunParticle;

	[Header ("Gun Settings")]
	[Range (5f, 30f)]
	public float normalMax = 20f;
	[Range (5f, 200f)]
	public float normalSpeed = 5f;
	[Range (1f, 5f)]
	public float normalSpeedScale = 2f;

	[Header ("SFX References")]
	public AudioClip mechineGunSfx;
	public AudioClip shortGunSfx;


	// Use this for initialization
	void Start () {
		AdvancedAudioAnalyzer.onBand2Trigger += OnShotGunTrigger;
		AdvancedAudioAnalyzer.onBand4Trigger += OnNormalGunTrigger;
		AdvancedAudioAnalyzer.onBand6Trigger += OnMachineGunTrigger;
	}
	
	// Update is called once per frame
	void Update () {
		// if (Input.touchCount > 0) {
		// 	normalParticle.Play ();
		// }
	}

	#region Controller
	public void StartShooting () {
		normalParticle.Play ();
	}
	#endregion

	#region Band Trigger
	// band 4 -> increase emission only
	public void OnNormalGunTrigger () {
		StartCoroutine (TriggerNormalGun ());
	}

	// band 2 -> trigger one shot
	public void OnShotGunTrigger () {
		// AudioSource.PlayClipAtPoint (shortGunSfx, Camera.main.gameObject.transform.position);
		shotGunParticle.Play ();
	}

	// band 6 -> trigger one time
	public void OnMachineGunTrigger () {
		// AudioSource.PlayClipAtPoint (mechineGunSfx, Camera.main.gameObject.transform.position);
		machineGunParticle.Play ();
	}
	#endregion

	#region Corotine Methods
	IEnumerator TriggerNormalGun () {
		var emission = normalParticle.emission;
		var rate = emission.rateOverTime;
		float orgRate = rate.constant;
		rate.constant = normalMax;
		normalParticle.startSpeed *= normalSpeedScale;
		yield return new WaitForSeconds (0.5f);
		rate.constant = orgRate;
		normalParticle.startSpeed = normalSpeed;
	}
	#endregion
}
