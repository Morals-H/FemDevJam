using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSourceData : MonoBehaviour
{
    public string audioType;
    public float DesiredAudio;
    private AudioSource aud;

    private void Start()
    {
        DesiredAudio = aud.volume;
        aud = GetComponent<AudioSource>();
        loadSettings();
    }

    //load
    public void loadSettings()
    {
        if (audioType == "Ambience")
        {
            aud.volume = (PlayerData.masterVolume * PlayerData.ambienceVolume) * DesiredAudio;
        }
        else if (audioType == "Music")
        {
            aud.volume = (PlayerData.masterVolume * PlayerData.ambienceVolume) * DesiredAudio;
        }
        else
        {
            aud.volume = (PlayerData.masterVolume * PlayerData.sfxVolume) * DesiredAudio;
        }
    }
}
