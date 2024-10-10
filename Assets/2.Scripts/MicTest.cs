using System.Runtime.InteropServices;
using UnityEngine;

public class MicTest : MonoBehaviour
{
    AudioSource audioSource;
    private float sensitivity = 100f;
    private float loudness = 0f;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();

        audioSource.clip = Microphone.Start(null, true, 10, 44100);
        audioSource.loop = true;
        audioSource.mute = false;
        while (!(Microphone.GetPosition(null) > 0)) { }
        audioSource.Play();
    }

    private void Update()
    {
        loudness = GetAveragedVolume() * sensitivity;
        Debug.Log(loudness);
    }

    float GetAveragedVolume()
    {
        float[] data = new float[256];
        float a = 0;
        audioSource.GetOutputData(data, 0);
        foreach (float s in data)
        {
            a += Mathf.Abs(s);
        }
        return a / 256;
    }
}
