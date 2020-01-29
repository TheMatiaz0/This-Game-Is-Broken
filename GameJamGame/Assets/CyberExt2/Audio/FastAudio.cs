using Cyberevolver;
using Cyberevolver.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Audio;

namespace Cyberevolver.Unity
{
	public static class FastAudio
	{
		public static void PlayAtPoint(Vector2 point, AudioClip clip, AudioMixerGroup mixer = null)
		{
			GameObject newGameObject = new GameObject();
			newGameObject.transform.position = (Vector2)point;
			AudioSource source = newGameObject.AddComponent<AudioSource>();
			source.spatialBlend = 1.0f;
			source.clip = clip;

			if (mixer != null)
			{
				source.outputAudioMixerGroup = mixer;
			}


			source.Play();
			source.gameObject.AddComponent<MonoBehaviourPlus>().StartCoroutine(Cor(source));
		}

		private static IEnumerator Cor(AudioSource audioSource)
		{
			yield return Async.NextFrame;
			yield return Async.Until(() => !audioSource.isPlaying);
			UnityEngine.Object.Destroy(audioSource.gameObject);

		}
	}
}

