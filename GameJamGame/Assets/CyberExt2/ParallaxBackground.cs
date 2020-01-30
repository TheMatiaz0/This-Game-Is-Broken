using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    [SerializeField]
    private Transform cameraTransform = null;

    [SerializeField]
    private Vector2 parallaxMultiple;

    private Vector3 startCamPosition;

    private void Start()
    {
        SetStartCameraPosition();
    }

    private void LateUpdate()
    {
        Vector3 deltaMovement = cameraTransform.position - startCamPosition;
        this.transform.position += new Vector3(deltaMovement.x * parallaxMultiple.x, deltaMovement.y * parallaxMultiple.y, deltaMovement.z);
        SetStartCameraPosition();
    }

    private void SetStartCameraPosition()
    {
        startCamPosition = cameraTransform.position;
    }
}
