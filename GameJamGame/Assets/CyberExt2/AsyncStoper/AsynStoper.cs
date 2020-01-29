using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;
namespace Cyberevolver.Unity
{
	[Serializable]
	public class AsyncStoper : ISerializationCallbackReceiver
	{
		private readonly static List<AsyncStoper> AsyncStoperList = new List<AsyncStoper>();
		[SerializeField]
		private MonoBehaviour _Main = null;
		[SerializeField]
		private SerializeTimeSpan time = new SerializeTimeSpan(TimeSpan.Zero);
		[SerializeField]
		private UnityEvent onEnd = null;
		public static void MakeSimple(MonoBehaviour main, TimeSpan time, EventHandler<EventArgs> @event)
		{
			AsyncStoperList.Add(new AsyncStoper(main, time, @event));
			AsyncStoper stoper = AsyncStoperList[AsyncStoperList.Count - 1];
			stoper.OnEnd += AsyncStoper_OnEnd;
			stoper.Start();
		}
		private static void AsyncStoper_OnEnd(object sender, EventArgs e)
		{
			AsyncStoper asyncStoper = sender as AsyncStoper;
			AsyncStoperList.Remove(asyncStoper);
		}
		public TimeSpan MaxTime { get; private set; }
		public bool End { get; set; }
		public Coroutine Coroutine { get; private set; }

		/// <summary>
		/// Return true if main has been killed before stoper end
		/// </summary>
		public bool IsBroken => IsStoperRunning && Main == null;
		public bool IgnoreTimeScale { get; set; }
		public bool IsStoperRunning { get; private set; }
		public TimeSpan CurrentTime { get; set; }
		public UnityEngine.MonoBehaviour Main => _Main;
		public bool AutoReset
		{
			get
			{
				foreach (EventHandler<EventArgs> item in OnEnd.GetInvocationList())
				{
					if (item == On_End_Reset)
						return true;
				}
				return false;
			}
			set
			{
				if (value == AutoReset)
					return;

				if (value == false)
					OnEnd -= On_End_Reset;
				else
					OnEnd += On_End_Reset;

			}
		}


		public event EventHandler<EventArgs> OnEnd = delegate { };
		public AsyncStoper(UnityEngine.MonoBehaviour main, TimeSpan maxTime, EventHandler<EventArgs> @event = null, bool autoReset = false, bool ignoreTimeScale = false)
		{
			_Main = main;
			MaxTime = maxTime;
			AutoReset = autoReset;
			IgnoreTimeScale = ignoreTimeScale;
			OnEnd += @event;

		}
		public AsyncStoper()
		{

		}
		public void RemoveEvent()
		{
			bool isAutoReset = AutoReset;
			OnEnd = delegate { };
			if (isAutoReset) AutoReset = true;
		}


		public void Start()
		{
			IsStoperRunning = true;
			End = false;
			if (Coroutine != null)
				Main.StopCoroutine(Coroutine);
			Coroutine = Main.StartCoroutine(Working());
		}
		private void On_End_Reset(object sender, EventArgs e) => Start();
		public bool GetEndAndTryReset()
		{
			bool value = End;
			if (End == true) Start();
			return value;
		}
		private IEnumerator Working()
		{


			CurrentTime = MaxTime;
			while (CurrentTime > TimeSpan.Zero)
			{
				CurrentTime -= TimeSpan.FromSeconds(Time.deltaTime);
				if (CurrentTime < TimeSpan.Zero)
					CurrentTime = TimeSpan.Zero;
				yield return Async.NextFrame;

			}



			yield return new WaitForEndOfFrame();
			End = true;
			IsStoperRunning = false;
			OnEnd(this, EventArgs.Empty);
			onEnd?.Invoke();
			yield break;
		}

		void ISerializationCallbackReceiver.OnBeforeSerialize()
		{
			time.TimeSpan = MaxTime;
		}

		void ISerializationCallbackReceiver.OnAfterDeserialize()
		{
			MaxTime = time.TimeSpan;
		}
	}


}
