using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSyncer : MonoBehaviour
{
    //Variable for the switch case
    public int _track = 1;
    //Which bias the function should have
    public float bias;
    //Maximum time it can be between two individual beats
    public float timeStep;
    //The time it takes to reach the beat value when a beat has been registered
    public float timeToBeat;
    //The time it takes to reach the rest value afer a beat has been registered
    public float restSmoothTime;

    //Save previous audio value to determine bias
    private float m_previousAudioValue;
    //Audio values from the Audio spectrum class
    private float m_audioValue;
    //A timer based on time
    private float m_timer;

    //Boolean which triggers when a beat is registered
    protected bool m_isBeat;

    //Beat function, it is however never triggered in this class,
    //instead it is overridden in the noise-functions
    public virtual void OnBeat()
    {
	    Debug.Log("beat");
	    m_timer = 0;
	    m_isBeat = true;
    }

	public virtual void OnUpdate()
	{

        m_previousAudioValue = m_audioValue;

        //Switch case to choose which track of the song the function should be triggered by
        switch (_track)
        {
            case 1:
                m_audioValue = KickAudioSpectrum.spectrumValue;
                break;
            case 2:
                m_audioValue = DrumsAudioSpectrum.spectrumValue;
                break;
            case 3:
                m_audioValue = BassAudioSpectrum.spectrumValue;
                break;
            case 4:
                m_audioValue = GuitarAudioSpectrum.spectrumValue;
                break;
            case 5:
                m_audioValue = VocalsAudioSpectrum.spectrumValue;
                break;
            case 6:
                m_audioValue = KeysAudioSpectrum.spectrumValue;
                break;
            case 7:
                m_audioValue = OtherAudioSpectrum.spectrumValue;
                break;
            default:
                m_audioValue = KickAudioSpectrum.spectrumValue;
                break;
        }

		//If audio value went below the bias during a frame
		if (m_previousAudioValue > bias &&
			m_audioValue <= bias)
		{
			//If minimum beat interval is reached
			if (m_timer > timeStep)
				OnBeat();
		}

		//If audio value went above the bias during a frame
		if (m_previousAudioValue <= bias &&
			m_audioValue > bias)
		{
			//If minimum beat interval is reached
			if (m_timer > timeStep)
				OnBeat();
		}

        //Update the timer
		m_timer += Time.deltaTime;
	}
}