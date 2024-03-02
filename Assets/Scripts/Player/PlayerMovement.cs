using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //Has a reference to a singleton Instance of InputManager
    [SerializeField] private Rigidbody2D rb;

    [SerializeField] private float moveSpeed = 1f;

    private void Update()
    {
        MovePlayer();
    }

    private void MovePlayer()
    {
        Vector2 moveDir = InputManager.Instance.MoveInput.normalized;

        Vector2 moveVector = moveDir * moveSpeed;

        rb.velocity = moveVector;
    }
}
