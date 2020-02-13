using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementBug : GlitchEffect
{
	public override string Description => "Left/Right Vector2 Movement not working";


	public override void FixedUpdate()
	{
		PlayerController.Instance.Rgb.velocity = new Vector2(6.5f * Vector2.right.x, PlayerController.Instance.Rgb.velocity.y);
	}
}
