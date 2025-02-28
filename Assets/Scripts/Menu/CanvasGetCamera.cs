using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Canvas))]
public class CanvasGetCamera : MonoBehaviour
{
    private Canvas canvas;

    private void Awake()
    {
        canvas = GetComponent<Canvas>();
        SetCanvasCamera();
    }

    private void SetCanvasCamera()
    {
        canvas.worldCamera = Camera.main;
    }
}
