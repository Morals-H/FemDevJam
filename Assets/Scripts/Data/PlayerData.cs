using UnityEngine;

public class PlayerData : MonoBehaviour
{
    //Audio
    public static float masterVolume;
    public static float musicVolume;
    public static float ambienceVolume;
    public static float sfxVolume;

    //controls
    public static float xSens;
    public static float ySens;
    public static bool xInvert;
    public static bool yInvert;

    //keep this around
    private void Start()
    {
        LoadSettings();
        DontDestroyOnLoad(this.gameObject);
    }

    // saving settings to json
    public void SaveSettings()
    {
        //saving audio
        PlayerPrefs.SetFloat("masterVolume", masterVolume);
        PlayerPrefs.SetFloat("musicVolume", musicVolume);
        PlayerPrefs.SetFloat("ambienceVolume", ambienceVolume);
        PlayerPrefs.SetFloat("sfxVolume", sfxVolume);

        //saving controls
        PlayerPrefs.SetFloat("xSens", xSens);
        PlayerPrefs.SetFloat("ySens", ySens);
        PlayerPrefsX.SetBool("xInvert", xInvert);
        PlayerPrefsX.SetBool("yInvert", yInvert);

        //changing all audio devices
        BroadcastMessage("loadSettings");
    }

    // loading settings from json
    public void LoadSettings()
    {
        //loading audio
        masterVolume = PlayerPrefs.GetFloat("masterVolume");
        musicVolume = PlayerPrefs.GetFloat("musicVolume");
        ambienceVolume = PlayerPrefs.GetFloat("ambienceVolume");
        sfxVolume = PlayerPrefs.GetFloat("sfxVolume");

        //loading controls
        xSens = PlayerPrefs.GetFloat("xSens");
        ySens = PlayerPrefs.GetFloat("ySens");
        xInvert = PlayerPrefsX.GetBool("xInvert");
        yInvert = PlayerPrefsX.GetBool("yInvert");
    }
}
