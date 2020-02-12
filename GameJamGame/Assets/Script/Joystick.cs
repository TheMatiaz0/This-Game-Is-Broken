using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Joystick : MonoBehaviour
{
	[SerializeField]
	private GameObject dot, circle;

	private Touch oneTouch;

	private Vector2 touchPosition;

	public Vector2 Movement { get; private set; }

	protected void Start()
	{
		SwitchUI(false);
	}

	private void SwitchUI (bool isActive)
	{
		dot.SetActive(isActive);
		circle.SetActive(isActive);
	}

	protected void Update()
	{
		if (Input.touchCount > 0)
		{
			oneTouch = Input.GetTouch(0);

			touchPosition = Camera.main.ScreenToWorldPoint(oneTouch.position);

			switch (oneTouch.phase)
			{
				case TouchPhase.Began:
					SwitchUI(true);
					circle.transform.position = touchPosition;
					dot.transform.position = touchPosition;
					break;

				case TouchPhase.Stationary:
				case TouchPhase.Moved:
					Move();
					break;

				case TouchPhase.Ended:
					SwitchUI(false);
					break;
			}
		}
	}

	private void Move ()
	{
		Transform dotTransform = dot.transform;
		Transform circleTransform = circle.transform;

		dotTransform.position = touchPosition;

		dotTransform.position = new Vector2(Mathf.Clamp(dotTransform.position.x, circleTransform.position.x - 0.8f, circleTransform.position.x + 0.8f),
			Mathf.Clamp(dotTransform.position.y, circleTransform.position.y - 0.8f, circleTransform.position.y + 0.8f));

		Movement = (dotTransform.position - circleTransform.position).normalized;
	}
}
