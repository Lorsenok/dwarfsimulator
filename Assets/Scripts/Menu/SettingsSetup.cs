using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SettingsSetup : MonoBehaviour
{
    public static SettingsSetup Instance { get; private set; }

    [SerializeField] private Menu menu;

    [SerializeField] private Slider sound;
    [SerializeField] private Slider music;

    [SerializeField] private Slider power;
    [SerializeField] private Slider fov;

    public void ClearSettings()
    {
        PlayerPrefs.SetFloat("sound", Config.SoundDefault);
        PlayerPrefs.SetFloat("music", Config.MusicDefault);

        PlayerPrefs.SetFloat("power", Config.PostProcessingPowerDefault);
        PlayerPrefs.SetFloat("fov", Config.FOVDefault);

        Load();
    }

    private void Load()
    {
        if (!PlayerPrefs.HasKey("sound"))
        {
            ClearSettings();
            return;
        }

        sound.value = PlayerPrefs.GetFloat("sound");
        music.value = PlayerPrefs.GetFloat("music");

        power.value = PlayerPrefs.GetFloat("power");
        fov.value = PlayerPrefs.GetFloat("fov");
    }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        Load();
    }

    private void Update()
    {
        Config.Sound = sound.value;
        Config.Music = music.value;

        Config.PostProcessingPower = power.value;
        Config.FOV = fov.value;

        if (!menu.Open) return;

        PlayerPrefs.SetFloat("sound", sound.value);
        PlayerPrefs.SetFloat("music", music.value);

        PlayerPrefs.SetFloat("power", power.value);
        PlayerPrefs.SetFloat("fov", fov.value);
    }
}
