using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Audio;
using System.Xml.Serialization;
using System.Xml;
using System.IO;

public sealed class SettingsConfig
{
    public static string Path => System.IO.Path.Combine(Application.dataPath, "Settings.config");
    public Resolution Resolution { get; set; }
    public uint Quality { get; set; }
    public bool VSync { get; set; }
    public float MasterVolume { get; set; }
    public float MusicVolume { get; set; }
    public float SfxVolume { get; set; }
  
    public bool FirstTime { get; set; } = true;

   
    public FullScreenMode FullScreenMode { get; set; }
    // private AudioMixer mixer = (Resources.Load("Audio") as AudioMixer);

    private readonly static XmlSerializer xmlWriter = new XmlSerializer(typeof(SettingsConfig));

    public void Accept()
    {
        Screen.SetResolution(Resolution.width, Resolution.height, FullScreenMode, Resolution.refreshRate);
        QualitySettings.SetQualityLevel((int)Quality);
        QualitySettings.vSyncCount = Convert.ToInt32(VSync);
        // mixer.SetFloat("masterVolume", MasterVolume);
        // mixer.SetFloat("musicVolume", MusicVolume);
        // mixer.SetFloat("sfxVolume", SfxVolume);
      
    }

    public void Save()
    {
        this.Accept();
        using (StreamWriter writer = new StreamWriter(Path))
        {
            xmlWriter.Serialize(writer, this);
        }
    }
    public static SettingsConfig Load()
    {
        using (StreamReader s = new StreamReader(SettingsConfig.Path))
        {
            return  (SettingsConfig)xmlWriter.Deserialize(s);
        }
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