using Cyberevolver.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundTest : MonoBehaviour
{
	[SerializeField] private Transform objectPosition = null;
	[SerializeField] private AudioClip clip = null;
	[SerializeField] private SerializeTimeSpan timeSpan;

	protected void Start()
	{
		StartCoroutine(PlaySound());
	}

	private IEnumerator PlaySound()
	{
		while (true)
		{
			FastAudio.PlayAtPoint(objectPosition.position, clip);
			Vector3 vec3 = objectPosition.transform.position;
			vec3.x += 2;
			vec3.y -= 1;
			vec3.z = 0;
			objectPosition.transform.position = vec3;
			yield return Async.Wait(timeSpan.TimeSpan);
		}

	}
}
