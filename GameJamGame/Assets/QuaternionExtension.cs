using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public static class QuaternionExtension
{
	public enum Coordinate
	{
		X = 1,
		Y = 2,
		Z = 3
	}

	public static Quaternion Add (this Quaternion q1, Vector3 degrees)
	{
		return Quaternion.Euler(q1.eulerAngles.x + degrees.x, q1.eulerAngles.y + degrees.y, q1.eulerAngles.z + degrees.z);
	}
}
