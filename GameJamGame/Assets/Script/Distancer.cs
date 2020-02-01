using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Distancer : GlitchEffect
{
	public override string Description => "INT_OVERFLOW_EXCEPTION";

	protected override void OnCancel()
	{
		WhenCollect();
	}

	public override void WhenCollect()
	{
		CinemachineVirtualCamera virtualCamera = Camera.main.GetComponentInChildren<Cinemachine.CinemachineVirtualCamera>();

		CinemachineComponentBase componentBase = virtualCamera.GetCinemachineComponent(CinemachineCore.Stage.Body);
		if (componentBase is CinemachineFramingTransposer)
		{
			// (componentBase as CinemachineFramingTransposer).m_CameraDistance = Mathf.Cos();
		}
	}
	public override void Update()
	{
		base.Update();
	}
}
