using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Player
{
    [Header("Movements")]
    //Has a reference to a singleton Instance of InputManager
    [SerializeField] private Rigidbody2D rb;

    [SerializeField] private float moveSpeed = 1f;

    private void MovePlayer()
    {
        Vector2 moveDir = InputManager.Instance.MoveInput.normalized;

        Vector2 moveVector = moveDir * moveSpeed;

        rb.velocity = moveVector;
    }
}
