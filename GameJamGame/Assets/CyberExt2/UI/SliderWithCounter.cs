using UnityEngine;
using UnityEngine.UI;

#pragma warning disable IDE0044
/// <summary>
/// Kiedy potrzebujesz Slidera z aktywnym tekstem Counter, użyj tego skryptu.
/// </summary>
[RequireComponent(typeof(Slider))]
public abstract class SliderWithCounter : MonoBehaviour
{
    [Header("Remember to set On Value Changed to OnDrag method.")]

    protected Slider slider;
    [SerializeField] private Text textCounter = null;

    public Text TextCounter => textCounter;
  
    protected void Awake()
    {
        slider = GetComponent<Slider>();
    }

    /// <summary>
    /// Tą metodę podpisz jako zmienną w HandleDrag (w Inspectorze oczywiście).
    /// </summary>
    public abstract void OnDrag();
}
