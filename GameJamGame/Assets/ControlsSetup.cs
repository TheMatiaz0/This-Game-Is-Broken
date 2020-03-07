using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlsSetup : MonoBehaviour
{
    [SerializeField]
    private GameObject joystickPrefab = null;

    [SerializeField]
    private GameObject arrowsPrefab = null;

    private void OnEnable()
    {
        GameObject t = ReturnObjectFromControls();

        if (t != null)
        Instantiate(t, this.transform);
    }

    private GameObject ReturnObjectFromControls ()
    {
        switch (OptionsManager.CurrentConfig?.ControlsSetup)
        {
            case OptionsManager.ControlsSetup.Arrows:
                return arrowsPrefab;

            default:
                return joystickPrefab;
        }
    }
}
