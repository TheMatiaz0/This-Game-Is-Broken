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

	private readonly static XmlSerializer xmlWriter = new XmlSerializer(typeof(SettingsConfig));

	private static readonly string path = "Settings.config";

	protected virtual void OnDisable()
	{
		CurrentConfig.Accept();
		using (StreamWriter writer = new StreamWriter(path))
		{
			xmlWriter.Serialize(writer, CurrentConfig);
		}
		inited = false;
	}

	private void UpdateConfig()
	{
		CurrentConfig.Accept();
	}

	public static SettingsConfig CurrentConfig { get; private set; }

	[RuntimeInitializeOnLoadMethod]
	public async static void Init()
	{
		if (File.Exists(path))
		{
			using (StreamReader s = new StreamReader(path))
			{
				CurrentConfig = (SettingsConfig)xmlWriter.Deserialize(s);
			}
		}
		else
		{
			Debug.Log("Can't find Settings.config file");
			CurrentConfig = new SettingsConfig();
			CurrentConfig.LoadDefault();

		}
		await Async.NextFrame;
		CurrentConfig.Accept();

	}

	private bool inited;

	protected virtual void OnEnable()
	{
		qualityDropdown.SetValueWithoutNotify(QualitySettings.GetQualityLevel());
		vSyncToggle.SetIsOnWithoutNotify(CurrentConfig.VSync);
		fullscreenToggle.SetIsOnWithoutNotify(CurrentConfig.FullScreenMode == FullScreenMode.FullScreenWindow);
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
