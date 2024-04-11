using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Canvas))]
public class CanvasGetCamera : MonoBehaviour
{
    private Canvas canvas;

    private void Awake()
    {
        SetCanvasCamera();
    }

    private void SetCanvasCamera()
    {
        canvas.worldCamera = Camera.main;
    }
}
