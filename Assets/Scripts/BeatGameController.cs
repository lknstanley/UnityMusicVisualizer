using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BeatGameController : MonoBehaviour {

	[Header ("Button References")]
	public Button leftBeatButton;
	public Button rightBeatButton;

	[Header ("Score Label Reference")]
	public Text scoreLbl;
	private const string scoreFormat = "Score: {0}";
	private int curScore = 0;

	[Header ("Beat Game Settings")]
	[Range (0.1f, 100f)]
	public float triggerValue = 5f;
	[Range (1f, 100f)]
	public float sensitivity = 1f;
	[Range (0.03f, 1f)]
	public float delayToSwitch = 0.1f;
	[Range (0, 7)]
	public int focusOnBand = 0;

	private bool isLeft = true;
	private bool isClicked = false;

	// Use this for initialization
	void Start () {

		InvokeRepeating ("CheckToSwitch", delayToSwitch, 1f);
	}
	
	// Update is called once per frame
	void Update () {

	}

	void CheckToSwitch () {
		if (AudioAnalyzer.feqs [focusOnBand] * sensitivity >= triggerValue) {
			isLeft = !isLeft;
			leftBeatButton.interactable = isLeft;
			rightBeatButton.interactable = !isLeft;
			isClicked = false;
		}
	}

	#region UI Callbacks
	public void OnLeftClick () {
		isClicked = true;
		curScore ++;
		scoreLbl.text = string.Format (scoreFormat, curScore);
	}

	public void OnRightClick () {
		isClicked = true;
		curScore ++;
		scoreLbl.text = string.Format (scoreFormat, curScore);
	}
	#endregion
}
