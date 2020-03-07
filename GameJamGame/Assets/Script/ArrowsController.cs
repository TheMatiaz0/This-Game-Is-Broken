using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowsController : MonoBehaviour
{
	public void LeftArrowClick ()
	{
		PlayerController.Instance.LeftBtnClick();
	}

	public void RightArrowClick ()
	{
		PlayerController.Instance.RightBtnClick();
	}

	public void ResetMovement ()
	{
		PlayerController.Instance.ResetMovement();
	}
}
