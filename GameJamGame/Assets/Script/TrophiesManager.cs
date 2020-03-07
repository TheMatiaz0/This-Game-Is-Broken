using Cyberevolver.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameJolt.API;

public enum Trophy
{
	ThankYou = 117989,
	BigOOF = 117993,
	IsItSomethingWrong = 117990,
	ICantSeeAnything = 117992,
	TheWinner = 117991
}

public class TrophiesManager
{
	public static void UnlockTrophy (Trophy trophyToUnlock)
	{
		Trophies.TryUnlock((int)trophyToUnlock);
	}
}
