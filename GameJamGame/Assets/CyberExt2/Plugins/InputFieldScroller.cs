using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
#pragma warning disable
/* Special thanks to SweatyChair (https://answers.unity.com/users/850343/sweatychair.html)
 * https://answers.unity.com/questions/932607/putting-a-multiline-inputfield-in-a-scroll-rect.html */

[ExecuteInEditMode]
[RequireComponent(typeof(InputField))]
public class InputFieldScroller : UIBehaviour
{
    [Tooltip("The default row count in InputField, this will be ignored if a ScrollRect is assigned")]
    [Range(1, 50)] [SerializeField] private int _minRowCount = 1;

    [Tooltip("Scroll rect parent")]
    [SerializeField] private ScrollRect _scrollRect = null;

    private InputField _inputField;
    private RectTransform _rectTransform;

    // Layout
    private LayoutElement _layoutElement;
    private HorizontalOrVerticalLayoutGroup _parentLayoutGroup;

    private Vector2 _scrollRectSize;
    private float _canvasScaleFactor = 1;

    protected override void Awake()
    {
        _inputField = GetComponent<InputField>();
        _inputField.onValueChanged.AddListener(new UnityAction<string>(ResizeInput));
        _rectTransform = GetComponent<RectTransform>();
        CanvasScaler canvasScaler = GetComponentInParent<CanvasScaler>();
        if (canvasScaler)
            _canvasScaleFactor = canvasScaler.scaleFactor;
        _layoutElement = GetComponent<LayoutElement>();
        _parentLayoutGroup = transform.parent.GetComponent<HorizontalOrVerticalLayoutGroup>();
    }

    // Resize input field recttransform
    private void ResizeInput()
    {
        ResizeInput(_inputField.text);
    }

    private void ResizeInput(string text)
    {
        // Current text settings
        TextGenerationSettings settings = _inputField.textComponent.GetGenerationSettings(_inputField.textComponent.rectTransform.rect.size);
        settings.generateOutOfBounds = false;
        settings.scaleFactor = _canvasScaleFactor; // HACK: scale factor of settings not following the global scale factor... make sure it do

        // Get text padding (min max vertical offset for size calculation)
        float vecticalOffset = _inputField.placeholder.rectTransform.offsetMin.y - _inputField.placeholder.rectTransform.offsetMax.y;

        // Preferred text rect height
        float preferredHeight = (new TextGenerator().GetPreferredHeight(text, settings) / _canvasScaleFactor) + vecticalOffset + 10;
        float minHeight;

        // Default text rect height (fit to scroll parent or expand to fit text)
        if (_scrollRect)
            minHeight = _scrollRect.GetComponent<RectTransform>().rect.size.y;
        else
            minHeight = (new TextGenerator().GetPreferredHeight("", settings) * _minRowCount / _canvasScaleFactor) + vecticalOffset;

        // Current text rect height
        float currentHeight = _inputField.textComponent.rectTransform.rect.height;

        // Force resize
        if (Mathf.Abs(currentHeight - preferredHeight) > Mathf.Epsilon)
        {
            float newHeight = Mathf.Max(preferredHeight, minHeight); // At least min height
            if (_parentLayoutGroup && _layoutElement)
                _layoutElement.preferredHeight = newHeight;
            else
                _rectTransform.sizeDelta = new Vector2(_rectTransform.rect.width, newHeight);
        }

        // Scroll to bottom if just added new line
        if (gameObject.activeInHierarchy && _inputField.caretPosition == _inputField.text.Length && _inputField.text.Length > 0 && _inputField.text[_inputField.text.Length - 1] == '\n')
            StartCoroutine(ScrollToBottomCoroutine());
    }

    // Update scroll rect position (after Layout was rebuilt)
    private IEnumerator ScrollToBottomCoroutine()
    {
        yield return new WaitForEndOfFrame();
        if (_scrollRect != null)
            _scrollRect.verticalNormalizedPosition = 0;
    }

}