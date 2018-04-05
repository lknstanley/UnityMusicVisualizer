using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunWatchPointer : MonoBehaviour {

	public Vector2 mousePos;
	public Vector3 screenPos;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		mousePos = Input.mousePosition;
		screenPos = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, transform.position.z - Camera.main.transform.position.z));
		// transform.eulerAngles = new Vector3 (transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, Mathf.Atan2((screenPos.y - transform.position.y), (screenPos.x - transform.position.x))*Mathf.Rad2Deg);
		transform.eulerAngles = new Vector3 (transform.rotation.eulerAngles.x, Mathf.Atan2((transform.position.y - screenPos.y), (transform.position.x - screenPos.x))*Mathf.Rad2Deg * -1, transform.rotation.eulerAngles.z);
	}
}
