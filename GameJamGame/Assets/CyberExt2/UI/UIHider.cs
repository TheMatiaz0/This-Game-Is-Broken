using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Cyberevolver.Unity
{
	/// <summary>
	/// Hides UI camera's culling layer after clicking specific key on keyboard.
	/// </summary>
	public class UIHider : MonoBehaviour
	{
		[SerializeField] private KeyCode keyCodeActive = KeyCode.F1;
		public bool IsHidden { get; private set; } = false;

		protected void Update()
		{
			CullingMaskChange();
		}

		private void CullingMaskChange()
		{
			if (Input.GetKeyDown(keyCodeActive))
			{
				Switch();
			}
		}

		public void Switch ()
		{
			IsHidden = !IsHidden;
			HideUI(IsHidden);
		}

		public void HideUI (bool areUSure)
		{
			if (!areUSure)
			{
				Camera.main.cullingMask = ~(0);
			}

			else
			{
				Camera.main.cullingMask = ~(1 << 5);
			}
		}
	}
}

