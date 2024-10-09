using UnityEngine;

public class MicTest : MonoBehaviour
{
    AudioSource audioSource;
    private float volumeLevel;
    private float[] audioData = new float[512];

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();

        if(Microphone.devices.Length > 0)
        {
            string micName = Microphone.devices[0];
            audioSource.clip = Microphone.Start(micName, true, 10, 16000);

            audioSource.loop = true;
            while(!(Microphone.GetPosition(micName) > 0)) { }
            audioSource.Play();
        }
    }

    private void Update()
    {
        audioSource.clip.GetData(audioData, audioSource.timeSamples);

        float sum = 0;
        for(int i = 0; i < audioData.Length; i++)
        {
            sum += audioData[i] * audioData[i];
        }

        volumeLevel = Mathf.Sqrt(sum / audioData.Length);
        volumeLevel *= 100f;

        Debug.Log(volumeLevel);
    }
}
