using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowNote : MonoBehaviour {

	[Header ("Settings")]
	[Range (1f, 10f)]
	public float speed = 1f;

	[Header ("Debug")]
	[SerializeField] private BandType noteType = BandType.Band1;
	[SerializeField] private bool isInit = false;
	[SerializeField] private Transform target;

	public void InitNote (BandType type, Transform start, Transform target) {
		this.target = target;
		transform.position = start.position;
		noteType = type;
		isInit = true;
	}
	public void InitNote (BandType type, Transform start, Transform target, float speed) {
		this.speed = speed;
		this.target = target;
		transform.position = start.position;
		noteType = type;
		isInit = true;
	}
	
	// Update is called once per frame
	void Update () {
		if (isInit) {
			// movement
			transform.position = Vector3.Lerp (transform.position, target.position, speed * Time.deltaTime);
		}
	}

	/// <summary>
	/// OnTriggerEnter is called when the Collider other enters the trigger.
	/// </summary>
	/// <param name="other">The other Collider involved in this collision.</param>
	void OnTriggerEnter(Collider other) {
		if (other.gameObject.name == "Destroyer") {
			Destroy (gameObject);
		}
	}
}
