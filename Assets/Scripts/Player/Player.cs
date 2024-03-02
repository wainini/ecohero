using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Player : MonoBehaviour
{
    private void Update()
    {
        MovePlayer();
        SearchInteractables();
    }

    private void OnEnable()
    {
        InputManager.Instance.OnInteractInput += InteractWithNearest;
    }

    private void OnDisable()
    {
        InputManager.Instance.OnInteractInput -= InteractWithNearest;
    }
}
