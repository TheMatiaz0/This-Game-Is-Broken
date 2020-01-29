using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
namespace Cyberevolver.Unity
{
	public sealed class InvokerBehaviour : MonoBehaviourPlus
	{
		[SerializeField] private UnityEvent onAwake = null;
		[SerializeField] private UnityEvent onStart = null;
		[SerializeField] private UnityEvent onDestroy = null;
		[SerializeField] private UnityEvent onEnable = null;
		[SerializeField] private UnityEvent onDisable = null;
		[SerializeField] private UnityEvent onUpdate = null;
		[SerializeField] private UnityEvent onLateUpdate = null;
		protected override void Awake()
		{
			base.Awake();
			onAwake.Invoke();
		}
		private void Start()
		{
			onStart.Invoke();
		}
		private void Update()
		{
			onUpdate.Invoke();
		}
		private void OnDestroy()
		{
			onDestroy.Invoke();
		}
		private void LateUpdate()
		{
			onLateUpdate.Invoke();
		}
		private void OnEnable()
		{
			onEnable.Invoke();
		}
		private void OnDisable()
		{
			onDisable.Invoke();
		}
	}
}
