using Cyberevolver;
using Cyberevolver.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
public class OptionsManager : MonoBehaviour
{


	public event EventHandler<float> OnMasterVolumeChanged = delegate { };
	private float masterVolumeLvl = 0;
	private float musicVolumeLvl = 0;
	private float sfxVolumeLvl = 0;

	[Header("Video Settings")]
	private Resolution[] resolutions = null;
	[SerializeField] private Dropdown resolutionDropdown = null;

	[SerializeField] private Dropdown qualityDropdown = null;
	[SerializeField] private Toggle vSyncToggle = null;
	[SerializeField] private Toggle fullscreenToggle = null;

	[SerializeField] private Slider master = null, sfx = null, music = null;


   

	protected virtual void OnDisable()
	{
        CurrentConfig.Save();
		inited = false;
	}

	private void UpdateConfig()
	{
		CurrentConfig.Accept();
	}

	public static SettingsConfig CurrentConfig { get; private set; }

#if UNITY_EDITOR

    [UnityEditor.MenuItem("TGIB/Clear Config")]
    public static void ClearConfig()
    {
        File.Delete(SettingsConfig.Path);
    }
#endif

    [RuntimeInitializeOnLoadMethod]
	public async static void Init()
	{
		if (File.Exists(SettingsConfig.Path))
		{
            CurrentConfig= SettingsConfig.Load();
		}
		else
		{
			Debug.Log("Can't find Settings.config file");

			CurrentConfig = new SettingsConfig();
            CurrentConfig.FirstTime = true;
			CurrentConfig.LoadDefault();

		}
		await Async.NextFrame;
		CurrentConfig.Accept();

	}

	private bool inited;

	protected virtual void OnEnable()
	{
		qualityDropdown.SetValueWithoutNotify(QualitySettings.GetQualityLevel());
		vSyncToggle.isOn=(CurrentConfig.VSync);
       
		fullscreenToggle.isOn=(CurrentConfig.FullScreenMode == FullScreenMode.FullScreenWindow);
		master.value = CurrentConfig.MasterVolume;
		music.value = CurrentConfig.MusicVolume;
		sfx.value = CurrentConfig.SfxVolume;

		if (resolutionDropdown != null)
		{
			LoadResolutionPref();
		}
		int? index = resolutions.GetIndex(item => item.Equals(CurrentConfig.Resolution));

		if (index == null)
			Debug.LogWarning("ERROR:: UNKNOWN RESOLUTION");
		else
			resolutionDropdown.value = (int)index;

		inited = true;
	}


	#region Video

	private void LoadResolutionPref()
	{
		resolutions = Screen.resolutions;

		resolutionDropdown.ClearOptions();

		List<string> options = new List<string>();

		int currentResolutionIndex = 0;

		for (int i = 0; i < resolutions.Length; i++)
		{
			string option = $"{resolutions[i].width}x{resolutions[i].height}, {resolutions[i].refreshRate}Hz";

			options.Add(option);

			if (resolutions[i].width == Screen.width && resolutions[i].height == Screen.height && resolutions[i].refreshRate == Screen.currentResolution.refreshRate)
			{
				currentResolutionIndex = i;
			}
		}

		resolutionDropdown.AddOptions(options);
		resolutionDropdown.value = currentResolutionIndex;
		resolutionDropdown.RefreshShownValue();
	}

	public void SetResolution(int resolutionIndex)
	{
		if (inited == false)
			return;

		CurrentConfig.Resolution = resolutions[resolutionIndex];
		UpdateConfig();
	}

	public void SetVSync(bool isOn)
	{
		if (inited == false)
			return;
		CurrentConfig.VSync = isOn;
		UpdateConfig();
	}

	public void SetQuality(int qualityIndex)
	{
		if (inited == false)
			return;
		CurrentConfig.Quality = (uint)qualityIndex;
		UpdateConfig();
	}

	public void SetFullscreen(bool isFullscreen)
	{
		if (inited == false)
			return;
		CurrentConfig.FullScreenMode = isFullscreen ? FullScreenMode.FullScreenWindow : FullScreenMode.Windowed;
		UpdateConfig();
	}

	public void SetMaxFramerate (int choice)
	{
		if (inited == false)
			return;

		switch (choice)
		{
			default:
				Application.targetFrameRate = -1;
				break;

			case 1:
				Application.targetFrameRate = 144;
				break;

			case 2:
				Application.targetFrameRate = 75;
				break;

			case 3:
				Application.targetFrameRate = 60;
				break;

			case 4:
				Application.targetFrameRate = 45;
				break;

			case 5:
				Application.targetFrameRate = 30;
				break;
		}
		UpdateConfig();
	}

	#endregion

	#region Audio

	public void SetMasterVolume(float value) => SetVolume(ref masterVolumeLvl, value, (p, v) => p.MasterVolume = v);
	public void SetMusicVolume(float value) => SetVolume(ref musicVolumeLvl, value, (p, v) => p.MusicVolume = v);
	public void SetSFXVolume(float value) => SetVolume(ref sfxVolumeLvl, value, (p, v) => p.SfxVolume = v);


	public void SetVolume(ref float volumeLevel, float finalValue, Action<SettingsConfig, float> act)
	{
		if (inited == false)
			return;

		volumeLevel = finalValue;
		act(CurrentConfig, finalValue);
		UpdateConfig();

	}


	#endregion
}
