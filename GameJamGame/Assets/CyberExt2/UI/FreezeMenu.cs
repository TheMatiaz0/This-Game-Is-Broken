using Cyberevolver;
using Cyberevolver.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Cyberevolver.Unity
{

	#pragma warning disable IDE0044
	/// <summary>
	/// Creates a menu that can freeze the game by key press. A menu can be created by two methods in this script - first: GameObject Initialization, second: GameObject Set Active (both controlled by bool <c>isInitialization</c>).
	/// </summary>
	public class FreezeMenu : AutoInstanceBehaviour<FreezeMenu>
	{
		[SerializeField] private KeyCode keyCodeToOpen = KeyCode.A;
		[SerializeField] private GameObject objectToOpen = null;
		[SerializeField] private FreezeMenu[] blockOtherFreezes = null;
		[SerializeField] private bool isInitialization = true;

		[SerializeField, EnableWhen(nameof(isInitialization), Equaler.Equal, true)] private Transform parent = null;
		public bool IsPaused { get; private set; } = false;

		[SerializeField] private UnityEvent afterBeingPaused = null;
		[SerializeField] private UnityEvent afterBeingUnpaused = null;

		private GameObject tempChild = null;

		public object o;

		protected void Update()
		{
			WaitingForInput();
		}

		private void WaitingForInput()
		{
			if (Input.GetKeyDown(keyCodeToOpen))
			{
				MenuOpen();
			}
		}

		public void MenuOpen() => EnableMenuWithPause(!IsPaused);


		public void EnableMenuWithPause(bool to)
		{
			foreach (FreezeMenu item in blockOtherFreezes)
			{
				item.enabled = !to;
			}

			IsPaused = to;

			if (to)
			{
				afterBeingPaused.Invoke();
			}

			else
			{
				afterBeingUnpaused.Invoke();
			}

			if (isInitialization)
			{
				if (TimeManipulate(to) == true)
				{
					tempChild = Instantiate(objectToOpen, parent);
				}

				else if (TimeManipulate(to) == false)
				{
					Destroy(tempChild);
				}

			}

			else
			{
				tempChild = objectToOpen;
				tempChild.SetActive(to);
				TimeManipulate(to);
			}

			// LeanTween.alpha(tempChild, 1f, 25f);
		}

		private bool TimeManipulate(bool isTrue)
		{
			if (isTrue)
			{
				GlobalTime.Current.AddLockers(o);
			}

			else
			{
				GlobalTime.Current.RemoveLocker(o);
			}

			return isTrue;
		}
	}
}


