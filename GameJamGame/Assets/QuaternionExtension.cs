using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public static class QuaternionExtension
{
	public static Quaternion Add (this Quaternion q1, float degrees)
	{
		return Quaternion.Euler(0,0, q1.eulerAngles.z + degrees);
	}
}
