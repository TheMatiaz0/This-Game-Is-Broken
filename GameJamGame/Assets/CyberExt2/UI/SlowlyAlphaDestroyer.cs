using Cyberevolver.Unity;
using UnityEngine;
using UnityEngine.UI;


namespace Cyberevolver.Unity
{
	public class SlowlyAlphaDestroyer : MonoBehaviourPlus
	{
		[Auto]
		private Graphic Graphic { get; set; }

		[Auto]
		private SpriteRenderer Sprite { get; set; }

		[Range(0, 100), SerializeField] private float speed = 1;

		public float Speed
		{
			get => speed;
			set => speed = value;
		}

		private void Update()
		{
			CheckNull();
		}

		private void CheckNull ()
		{
			if (Graphic != null)
			{
				ColorChanger(Graphic);
			}

			if (Sprite != null)
			{
				ColorChanger(Sprite);
			}

			else
			{
				return;
			}
		}

		private void ColorChanger(Graphic graphic)
		{
			graphic.color -= new Color(0, 0, 0, Time.deltaTime * 0.1f * speed);
			if (graphic.color.a <= 0)
			{
				Destroy(this.gameObject);
			}
		}

		private void ColorChanger(SpriteRenderer renderer)
		{
			renderer.color -= new Color(0, 0, 0, Time.deltaTime * 0.1f * speed);
			if (renderer.color.a <= 0)
			{
				Destroy(this.gameObject);
			}
		}
	}
}

