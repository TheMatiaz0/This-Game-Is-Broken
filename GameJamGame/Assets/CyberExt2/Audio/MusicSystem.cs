using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Audio;

namespace Cyberevolver.Unity
{
    class MusicSystem
    {
        private static CancellationTokenSource cancelation = new CancellationTokenSource();
        private enum VolumeDirection
        {
            Up = 0,
            Down = 1,
        }

        public static AudioSource AudioSourceInstance { get; set; }
        private static VolumeClip _Actual;
        public static VolumeClip Actual
        {
            get => _Actual;
            set
            {
               
                AudioSourceInstance.clip = value?.AudioClip;
                _Actual = value;
            }
        }
	
		public static float FadeEffectSpeed { get; set; } = 1;

		private static void IfInstanceNullCreate()
		{


			if (AudioSourceInstance is null)
			{
                GameObject gameObject = new GameObject
                {
                    name = "MusicSource"
                };
                AudioSourceInstance = gameObject.AddComponent<AudioSource>();
			

				MonoBehaviour.DontDestroyOnLoad(gameObject);
			}
		}
		private static async Task SlowlyChangeMusicVolume(VolumeDirection dir, VolumeClip vClip, CancellationToken token)
		{
            
			while (token.IsCancellationRequested == false && ((dir == VolumeDirection.Down) ? AudioSourceInstance.volume > 0 : AudioSourceInstance.volume < vClip.Volume.AsFloatValue - 0.05f))
			{
                await new WaitForEndOfFrame();

                AudioSourceInstance.volume += ((dir == VolumeDirection.Down) ? -0.5f : 0.5f) * Time.unscaledDeltaTime * vClip.Volume.AsFloatValue * (FadeEffectSpeed);
				
			}


		}

		public static async Task ChangeMusicAsync(VolumeClip vClip)
		{
            
            Cancel();
            IfInstanceNullCreate();
           
            if (vClip == null)
			{
              
                await SlowlyMute();
              
                Actual = null;
				return;
			}
            await Async.NextFrame;
            Actual = vClip;
            CancellationToken token = cancelation.Token;
			await new WaitForEndOfFrame();
		
			if (Actual != null)
				await SlowlyChangeMusicVolume(VolumeDirection.Down, vClip, token);
			else
				AudioSourceInstance.volume = 0;
			
			AudioSourceInstance.Play();
			await SlowlyChangeMusicVolume(VolumeDirection.Up, vClip, token);

		}
		public static void ChangeInThisMoment(VolumeClip vClip)
		{
			Cancel();
            IfInstanceNullCreate();
           
            Actual = vClip;
			AudioSourceInstance.volume = vClip.Volume.AsFloatValue;
		
			AudioSourceInstance.Play();


		}
        public static async Task SlowlyUnMute()
        {
            Cancel();
            IfInstanceNullCreate();
          
            await SlowlyChangeMusicVolume(VolumeDirection.Up, new VolumeClip(Actual.AudioClip, Actual.Volume), cancelation.Token);
        }
		private static void Cancel()
		{
			cancelation?.Cancel();
			cancelation = new CancellationTokenSource();
		}
		public static async Task SlowlyMute()
		{
			Cancel();
            IfInstanceNullCreate();


            if (Actual == null)
                return;
            await SlowlyChangeMusicVolume(VolumeDirection.Down, new VolumeClip(Actual.AudioClip, AudioSourceInstance.volume), cancelation.Token);
         
        }


	}
}

