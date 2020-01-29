using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Cyberevolver.Unity
{
#pragma warning disable IDE0044
    public class ColorFlickable : MonoBehaviourPlus
    {
        [SerializeField] private Color32[] colors = null;

        [Auto]
        private Graphic ImageSwitcher { get; set; }

        [SerializeField] [Range(0f, 5f)] private float delay = 3;
        protected void Start()
        {
            StartCoroutine(Flash());
        }
        private void SwitchColors(Color32 color)
        {
            ImageSwitcher.color = color;
        }
        private IEnumerator Flash()
        {
            while (true)
            {
                foreach (Color32 item in colors)
                {
                    SwitchColors(item);
                    yield return Async.WaitForSeconds(delay);
                }
            }
        }
    }
}
