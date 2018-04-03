using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AdvancedAudioAnalyzer : MonoBehaviour
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
    private int[] feqSampleCounts = new int[8] { 2, 4, 8, 33, 23, 24, 46, 372 };

    [Header ("Beat Game Settings")]
    [Range (0.1f, 100f)]
    public float sensitivity = 0.5f;
    [Range (0.03f, 1f)]
    public float cooltimeSetting = 0.1f;
    private bool band1HandingCooldown = false;
    private bool band2HandingCooldown = false;
    private bool band3HandingCooldown = false;
    private bool band4HandingCooldown = false;
    private bool band5HandingCooldown = false;
    private bool band6HandingCooldown = false;
    private bool band7HandingCooldown = false;
    private bool band8HandingCooldown = false;

    // Action Callback Methods
    public static float[] previousFeqs;
    public static Action onBassTrigger;
    public static Action onBand2Trigger;
    public static Action onBand3Trigger;
    public static Action onBand4Trigger;
    public static Action onBand5Trigger;
    public static Action onBand6Trigger;
    public static Action onBand7Trigger;
    public static Action onBand8Trigger;

    // buffer feqs
    public static float[] bufferFeqs;
    float[] decreaseBufferFeqs;
    
    void Awake () {
        audioSource = GetComponent<AudioSource>();
    }

	// Use this for initialization
	void Start () {
        feqs = new float[feqBandCount];
        previousFeqs = new float [feqBandCount];
        bufferFeqs = new float [feqBandCount];
        decreaseBufferFeqs = new float [feqBandCount];
        samples = new float[sampleCount];
        feqDivider = (int)(sampleCount / feqBandCount);
    }
	
	// Update is called once per frame
	void Update () {
        GetSpectrum ();
        CreateFeqArray ();
        VisualizeMusic ();
        CreateBufferFeq ();
    }

    #region Audio Analzyer
    void GetSpectrum () {
        audioSource.GetSpectrumData (samples, 0, FFTWindow.BlackmanHarris);
    }

    void CreateFeqArray () {
        previousFeqs = feqs;
        feqs = new float[feqBandCount];

        /*
        We have 512 samples and each sameple represent (22050Hz / 512) ~= 43Hz
        20 - 50 Hz (Sub bass)
        60 - 250 Hz (Bass)
        250 - 500 Hz (Low Midrange)
        500 - 2000 Hz (Midrange)
        2000 - 3000 Hz (1st Upper Midrange)
        3000 - 4000 Hz (2nd Upper Midrange)
        4000 - 6000 Hz (Presence)
        6000 - 20000 Hz (Brilliance)

        How can we get the fequency band from samples like we listed above?
        The result is we are going to use N sample(s) to fill one band.
        20 - 50 Hz needs 2 samples to cover the fequency range (43Hz * 2 = 86Hz > 50Hz) 0~86Hz
        60 - 250 Hz needs 4 samples to cover the fequency range (86Hz + 43Hz * 4 = 258Hz > 250Hz) 87~258Hz
        250 - 500 Hz needs 8 samples to cover the fequency range (258Hz + 43Hz * 8 = 602Hz > 500Hz) 259~602Hz
        500 - 2000 Hz needs 33 samples to cover the fequency range (602Hz + 43Hz * 33 = 2021Hz > 2000Hz) 603~2021Hz
        2000 - 3000 Hz needs 23 samples to cover the fequency range (2021Hz + 43Hz * 23 = 3010Hz > 3000Hz) 2022~3010Hz
        3000 - 4000 Hz needs 24 samples to cover the fequency range (3010Hz + 43Hz * 24 = 4042Hz > 4000Hz) 3011~4042Hz
        4000 - 6000 Hz needs 46 samples to cover the fequency range (4042Hz + 43Hz * 46 = 6020Hz > 6000Hz) 4043~6020Hz
        6000 - 20000Hz needs 326 samples to cover the fequency range (6020Hz + 43Hz * 326 = 20038Hz > 20000Hz) 6021~20038Hz
        *remain 46 samples -> add to the last band*
         */

        // for (int i = 0; i < samples.Length; i++) {
        //     int tempIndex = Mathf.Max(0, i / feqDivider);
        //     feqs[tempIndex] += samples[i] * scaleTo;
        // }
        int sampleLength = 0;
        int count = 0;
        float average = 0f;
        for (int i = 0; i < feqBandCount; i++) {
            average = 0f;
            sampleLength = feqSampleCounts[i];
            for (int j = 0; j < sampleLength; j++) {
                average += samples[count] * (count + 1);
                count++;
            }
            average /= count;
            feqs[i] = average * 10f;
        }

        for (int i = 0; i < feqBandCount; i++) {
            if (previousFeqs[i] > feqs[i]) {
                continue;
            } else if (feqs[i] - previousFeqs[i] >= sensitivity) {
                switch (i) {
                    case 0:
                        if (!band1HandingCooldown) {
                            StartCoroutine (ShowBand1Note ());
                        }
                    break;
                    case 1:
                        if (!band2HandingCooldown) {
                            StartCoroutine (ShowBand2Note ());
                        }
                    break;
                    case 2:
                        if (!band3HandingCooldown) {
                            StartCoroutine (ShowBand3Note ());
                        }
                    break;
                    case 3:
                        if (!band4HandingCooldown) {
                            StartCoroutine (ShowBand4Note ());
                        }
                    break;
                    case 4:
                        if (!band5HandingCooldown) {
                            StartCoroutine (ShowBand5Note ());
                        }
                    break;
                    case 5:
                        if (!band6HandingCooldown) {
                            StartCoroutine (ShowBand6Note ());
                        }
                    break;
                    case 6:
                        if (!band7HandingCooldown) {
                            StartCoroutine (ShowBand7Note ());
                        }
                    break;
                    case 7:
                        if (!band8HandingCooldown) {
                            StartCoroutine (ShowBand8Note ());
                        }
                    break;
                }
            }
        }
    }

    void CreateBufferFeq () {
        for (int i = 0; i < feqBandCount; i++) {
            if (feqs[i] > bufferFeqs[i]) {
                bufferFeqs[i] = feqs[i];
                decreaseBufferFeqs[i] = 0.005f;
            } else if (feqs[i] < bufferFeqs[i]) {
                bufferFeqs[i] -= decreaseBufferFeqs[i];
                decreaseBufferFeqs[i] *= 1.2f;
            }
        }
    }
    #endregion

    #region Visualize Music
    void VisualizeMusic () {

    }
    #endregion

    #region Trigger Events
    IEnumerator ShowBand1Note () {
        band1HandingCooldown = true;
        if (onBassTrigger != null) {
            onBassTrigger ();
        }
        yield return new WaitForSeconds (cooltimeSetting);
        band1HandingCooldown = false;
    }
    IEnumerator ShowBand2Note () {
        band2HandingCooldown = true;
        if (onBand2Trigger != null) {
            onBand2Trigger ();
        }
        yield return new WaitForSeconds (cooltimeSetting);
        band2HandingCooldown = false;
    }
    IEnumerator ShowBand3Note () {
        band3HandingCooldown = true;
        if (onBand3Trigger != null) {
            onBand3Trigger ();
        }
        yield return new WaitForSeconds (cooltimeSetting);
        band3HandingCooldown = false;
    }
    IEnumerator ShowBand4Note () {
        band4HandingCooldown = true;
        if (onBand4Trigger != null) {
            onBand4Trigger ();
        }
        yield return new WaitForSeconds (cooltimeSetting);
        band4HandingCooldown = false;
    }
    IEnumerator ShowBand5Note () {
        band5HandingCooldown = true;
        if (onBand5Trigger != null) {
            onBand5Trigger ();
        }
        yield return new WaitForSeconds (cooltimeSetting);
        band5HandingCooldown = false;
    }
    IEnumerator ShowBand6Note () {
        band6HandingCooldown = true;
        if (onBand6Trigger != null) {
            onBand6Trigger ();
        }
        yield return new WaitForSeconds (cooltimeSetting);
        band6HandingCooldown = false;
    }
    IEnumerator ShowBand7Note () {
        band7HandingCooldown = true;
        if (onBand7Trigger != null) {
            onBand7Trigger ();
        }
        yield return new WaitForSeconds (cooltimeSetting);
        band7HandingCooldown = false;
    }
    IEnumerator ShowBand8Note () {
        band8HandingCooldown = true;
        if (onBand8Trigger != null) {
            onBand8Trigger ();
        }
        yield return new WaitForSeconds (cooltimeSetting);
        band8HandingCooldown = false;
    }
    #endregion
}