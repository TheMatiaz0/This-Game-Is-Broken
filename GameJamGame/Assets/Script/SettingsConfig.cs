using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Audio;

public class SettingsConfig
{
    public Resolution Resolution { get; set; }
    public uint Quality { get; set; }
    public bool VSync { get; set; }
    public float MasterVolume { get; set; }
    public float MusicVolume { get; set; }
    public float SfxVolume { get; set; }
    public FullScreenMode FullScreenMode { get; set; }
    // private AudioMixer mixer = (Resources.Load("Audio") as AudioMixer);

    public void Accept()
    {
        Screen.SetResolution(Resolution.width, Resolution.height, FullScreenMode, Resolution.refreshRate);
        QualitySettings.SetQualityLevel((int)Quality);
        QualitySettings.vSyncCount = Convert.ToInt32(VSync);
        // mixer.SetFloat("masterVolume", MasterVolume);
        // mixer.SetFloat("musicVolume", MusicVolume);
        // mixer.SetFloat("sfxVolume", SfxVolume);
      
    }
    public SettingsConfig()
    {
       
       
    }
    public void LoadDefault()
    {
        Resolution = Screen.currentResolution;
        Quality = (uint)QualitySettings.GetQualityLevel();
        VSync = QualitySettings.vSyncCount == 1;
        MasterVolume = 1f;
        MusicVolume = 1f;
        SfxVolume = 1f;
    }
  

}