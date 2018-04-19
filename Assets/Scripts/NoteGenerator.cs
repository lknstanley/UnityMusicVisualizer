using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BandType {
	Band1 = 1,
	Band2 = 2,
	Band3 = 3,
	Band4 = 4,
	Band5 = 5,
	Band6 = 6,
	Band7 = 7,
	Band8 = 8,
}

public class NoteGenerator : MonoBehaviour {

	[Header ("Note Prefabs")]
	public GameObject[] notePrefabs;
	public float speed = 1f;
	public bool useLerp = true;
	public float boostedTimeScale = 5f;
	public float boostedDuringTime = 0.5f;

	[Header ("Transform References")]
	public Transform notesParent;

	private Transform startPointsParent;
	[SerializeField] private Transform[] startPoints;
	private Transform endPointsParent;
	[SerializeField] private Transform[] endPoints;


	// for gameplay
	public static int totalNode = 0;

	/// <summary>
	/// Awake is called when the script instance is being loaded.
	/// </summary>
	void Awake () {
		startPointsParent = GameObject.FindGameObjectWithTag ("StartPoints").transform;
		startPoints = startPointsParent.GetComponentsInChildren<Transform> (true);
		endPointsParent = GameObject.FindGameObjectWithTag ("EndPoints").transform;
		endPoints = endPointsParent.GetComponentsInChildren<Transform> (true);

		AdvancedAudioAnalyzer.onBassTrigger += OnBassTrigger;
		AdvancedAudioAnalyzer.onBand2Trigger += OnBand2Trigger;
		AdvancedAudioAnalyzer.onBand3Trigger += OnBand3Trigger;
		AdvancedAudioAnalyzer.onBand4Trigger += OnBand4Trigger;
		AdvancedAudioAnalyzer.onBand5Trigger += OnBand5Trigger;
		AdvancedAudioAnalyzer.onBand6Trigger += OnBand6Trigger;
		AdvancedAudioAnalyzer.onBand7Trigger += OnBand7Trigger;
		AdvancedAudioAnalyzer.onBand8Trigger += OnBand8Trigger;
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	#region Audio Analyzer Callbacks
	public void OnBassTrigger () {
		// Debug.Log ("Bass");
		// Debug.Log ("X: " + startPoints[(int) BandType.Band1].localPosition.x);
		// Debug.Log ("Y: " + startPoints[(int) BandType.Band1].localPosition.y);
		// Debug.Log ("Z: " + startPoints[(int) BandType.Band1].localPosition.z);
		GameObject go = Instantiate (notePrefabs[0]);
		go.name = "Band1Note";
		go.transform.SetParent (notesParent);
		go.GetComponent<FlowNote> ().InitNote (BandType.Band1, startPoints[(int) BandType.Band1], endPoints[(int) BandType.Band1], speed, useLerp);
		totalNode ++;
	}

	public void OnBand2Trigger () {
		// Debug.Log ("2");
		GameObject go = Instantiate (notePrefabs[1]);
		go.name = "Band2Note";
		go.transform.SetParent (notesParent);
		go.GetComponent<FlowNote> ().InitNote (BandType.Band2, startPoints[(int) BandType.Band2], endPoints[(int) BandType.Band2], speed, useLerp);
		if (!isBoostingUp) {
			StartCoroutine (NoteBoostUp ());
		}
		totalNode ++;
	}

	public void OnBand3Trigger () {
		// Debug.Log ("3");
		GameObject go = Instantiate (notePrefabs[2]);
		go.name = "Band2Note";
		go.transform.SetParent (notesParent);
		go.GetComponent<FlowNote> ().InitNote (BandType.Band3, startPoints[(int) BandType.Band3], endPoints[(int) BandType.Band3], speed, useLerp);
		totalNode ++;
	}

	public void OnBand4Trigger () {
		// Debug.Log ("4");
		GameObject go = Instantiate (notePrefabs[3]);
		go.name = "Band2Note";
		go.transform.SetParent (notesParent);
		go.GetComponent<FlowNote> ().InitNote (BandType.Band4, startPoints[(int) BandType.Band4], endPoints[(int) BandType.Band4], speed, useLerp);
		totalNode ++;
	}

	public void OnBand5Trigger () {
		// Debug.Log ("5");
		GameObject go = Instantiate (notePrefabs[4]);
		go.name = "Band2Note";
		go.transform.SetParent (notesParent);
		go.GetComponent<FlowNote> ().InitNote (BandType.Band5, startPoints[(int) BandType.Band5], endPoints[(int) BandType.Band5], speed, useLerp);
		totalNode ++;
	}

	public void OnBand6Trigger () {
		// Debug.Log ("6");
		GameObject go = Instantiate (notePrefabs[5]);
		go.name = "Band2Note";
		go.transform.SetParent (notesParent);
		go.GetComponent<FlowNote> ().InitNote (BandType.Band6, startPoints[(int) BandType.Band6], endPoints[(int) BandType.Band6], speed, useLerp);
		totalNode ++;
	}

	public void OnBand7Trigger () {
		// Debug.Log ("7");
		GameObject go = Instantiate (notePrefabs[6]);
		go.name = "Band2Note";
		go.transform.SetParent (notesParent);
		go.GetComponent<FlowNote> ().InitNote (BandType.Band7, startPoints[(int) BandType.Band7], endPoints[(int) BandType.Band7], speed, useLerp);
		totalNode ++;
	}

	public void OnBand8Trigger () {
		// Debug.Log ("8");
		GameObject go = Instantiate (notePrefabs[7]);
		go.name = "Band2Note";
		go.transform.SetParent (notesParent);
		go.GetComponent<FlowNote> ().InitNote (BandType.Band8, startPoints[(int) BandType.Band8], endPoints[(int) BandType.Band8], speed, useLerp);
		totalNode ++;
	}
	#endregion

	#region Notes Boost Up
	bool isBoostingUp = false;
	IEnumerator NoteBoostUp () {
		isBoostingUp = true;
		Time.timeScale = boostedTimeScale;
		yield return new WaitForSeconds (boostedDuringTime);
		Time.timeScale = 1f;
		isBoostingUp = false;
	}
	#endregion
}
