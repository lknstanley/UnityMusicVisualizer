using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioVisualizer : MonoBehaviour {

	public float maxScale = 5000f;
	public Transform[] cubeTransform;

	// Use this for initialization
	void Start () {
		
    }
	
	// Update is called once per frame
	void Update () {
		for (int i = 0; i < 8; i++) {
			cubeTransform[i].localScale = new Vector3(cubeTransform[i].localScale.x, (AdvancedAudioAnalyzer.bufferFeqs [i]) * maxScale, cubeTransform[i].localScale.z);
		}
	}
}
