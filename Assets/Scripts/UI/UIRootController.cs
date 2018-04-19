using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIRootController : MonoBehaviour {

	[Header ("GameObject Controller References")]
	[SerializeField] private AudioSource  audioPlayer;
	[SerializeField] private GunShooter gunShooter;
	[SerializeField] private GameObject visualizer;

	[Header ("UI References")]
	public GameObject mainPanel;
	public GameObject ingamePanel;
	public UIAutoCounter autoCounter;

	public static UIRootController instance;

	void Awake() {
		instance = this;		
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	// ==============================
	// |		uGUI Callbacks		|
	// ==============================
	#region uGUI Callbacks
	public void OnStartClicked () {
		mainPanel.SetActive (false);
		ingamePanel.SetActive (true);
		if (audioPlayer != null && gunShooter != null && visualizer != null) {
			visualizer.SetActive (true);
			gunShooter.StartShooting ();
			audioPlayer.Play ();
		}
	}
	#endregion

	// ==========================================
	// |		Incode UI Update Methods		|
	// ==========================================
	#region Incode UI Update Methods
	public void UpdateScoreText () {
		autoCounter.UpdateText ();
	}
	#endregion
}
