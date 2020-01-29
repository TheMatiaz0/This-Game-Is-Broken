using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

namespace Cyberevolver.Unity
{
	public class InputInvoker : MonoBehaviourPlus
	{
		[SerializeField] private KeyCode key = KeyCode.None;
		[SerializeField] private UnityEvent onPress = null;
		[SerializeField] private UnityEvent onDown = null;
		[SerializeField] private UnityEvent onUp = null;
		protected virtual void Update()
		{
			if (Input.GetKey(key))
				onPress.Invoke();
			if (Input.GetKeyDown(key))
				onDown.Invoke();
			if (Input.GetKeyUp(key))
				onUp.Invoke();
		}
	}
}