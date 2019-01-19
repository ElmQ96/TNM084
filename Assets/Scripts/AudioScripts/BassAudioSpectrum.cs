using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BassAudioSpectrum : MonoBehaviour
{

    //Size of the array
    int size = 64;
    //Custom amplifier to change when tracks volume is too low
    public int _Amplifier = 100;
    //Array with spectrum values
    public float[] m_audioSpectrum;

    private void Start()
    {
        // initialize the array
        m_audioSpectrum = new float[size];
    }

    private void Update()
    {
        //Locate the sound from the Audiosource within the object
        AudioSource m_MyAudioSource = GetComponent<AudioSource>();
        //Do a spectrum analysis, saving it in an array, using the right stereo channel,
        //with the Hamming window function
        m_MyAudioSource.GetSpectrumData(m_audioSpectrum, 0, FFTWindow.Hamming);

        //Check that the spectrum data function works
        if (m_audioSpectrum != null && m_audioSpectrum.Length > 0)
        {
            //Loop through all the significant spectrum values
            //(The intentsity wears off from the starting value)
            //This represents the volume of the track
            for (int i = 0; i <= (size / 2); i++)
            { spectrumValue += m_audioSpectrum[i] * _Amplifier; }

            //Divide the number of samples to create an average value
            spectrumValue = spectrumValue / (size / 2);

            //Debug function to monitor the values
            //Debug.Log("Specktrum Value: " + spectrumValue);
        }
    }
    // This value is sent to AudioSyncer to create a beat
    public static float spectrumValue { get; private set; }
}
