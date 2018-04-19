using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIDebugPanel : MonoBehaviour {

	[Header ("Debug Settings")]
	public GameObject debugTarget;

	[Header ("Debug UI References")]
	public Text localRotateX;
	public Text localRotateY;
	public Text localRotateZ;
	public Text accX;
	public Text accY;
	public Text accZ;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (debugTarget != null) {
			localRotateX.text = debugTarget.transform.rotation.x.ToString ();
			localRotateY.text = debugTarget.transform.rotation.y.ToString ();
			localRotateZ.text = debugTarget.transform.rotation.z.ToString ();

			accX.text = Input.acceleration.x.ToString ();
			accY.text = Input.acceleration.y.ToString ();
			accZ.text = Input.acceleration.z.ToString ();
		}
	}
}
