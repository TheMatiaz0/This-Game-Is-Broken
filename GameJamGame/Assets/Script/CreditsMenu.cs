using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsMenu : MonoBehaviour
{
	#if GAME_JOLT
	protected void OnEnable()
	{
		TrophiesManager.UnlockTrophy(Trophy.ThankYou);
	}
	#endif
}
