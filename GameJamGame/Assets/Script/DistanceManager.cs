using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Cyberevolver.Unity;

public class DistanceManager : AutoInstanceBehaviour<DistanceManager>
{
	public float MetersTravelled { get { return _MetersTravelled; } private set { _MetersTravelled = value; OnMetersChange(_MetersTravelled); } }
	private float _MetersTravelled;
	private float startX;

	private int lastMetersReward = 0;

	private const int meterMaxForReward = 8;

	protected void Start()
	{
		startX = PlayerController.Instance.transform.position.x;
	}

	public void OnMetersChange (float meters)
	{
		int m = (int)meters;

		if (m / meterMaxForReward > lastMetersReward && m % meterMaxForReward == 0)
		{
			GameManager.Instance.AddScore(1);
			lastMetersReward++;
		}
	}


	public int GetMeters ()
	{
		MetersTravelled = Math.Max(MetersTravelled, PlayerController.Instance.transform.position.x - startX);
		return (int)MetersTravelled;
	}
}
