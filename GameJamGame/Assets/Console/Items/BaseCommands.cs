using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
#pragma warning disable IDE0060
[CommandContainer]
public static class BaseCommands
{
	[Command("Test")]
	public static string Test(string[] args)
	{

		StringBuilder builder = new StringBuilder();
		foreach (var item in args)
		{
			builder.Append(item);
		}
		return $"This is a test! Args are: {builder.ToString()}";
	}

}