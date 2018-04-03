using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioAnalyzer : MonoBehaviour
{
    [Header("Visualizer Settings")]
    public float scaleTo = 100f;
    public int feqBandCount = 8;

    [Header ("Sample Settings")]
    [SerializeField]
    private int sampleCount = 512;
    public static float[] feqs;
    public static float[] samples;
    private AudioSource audioSource;
    private int feqDivider = 0;
    
    void Awake () {
        audioSource = GetComponent<AudioSource>();
    }

	// Use this for initialization
	void Start () {
        feqs = new float[feqBandCount];
        samples = new float[sampleCount];
        feqDivider = (int)(sampleCount / feqBandCount);
    }
	
	// Update is called once per frame
	void Update () {
        GetSpectrum();
        CreateFeqArray();
        VisualizeMusic();
    }

    #region Audio Analzyer
    void GetSpectrum ()
    {
        audioSource.GetSpectrumData(samples, 0, FFTWindow.Blackman);
    }

    void CreateFeqArray ()
    {
        feqs = new float[feqBandCount];
        for (int i = 0; i < samples.Length; i++)
        {
            int tempIndex = Mathf.Max(0, i / feqDivider);
            feqs[tempIndex] += samples[i] * scaleTo;
        }
    }
    #endregion

    #region Visualize Music
    void VisualizeMusic ()
    {

    }
    #endregion
}