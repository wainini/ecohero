using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Player
{
    [Header("Movements")]
    //Has a reference to a singleton Instance of InputManager
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator animator;
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private float moveSpeed = 1f;

    private void MovePlayer()
    {
        if (GameManager.Instance.GameMode != GameMode.CollectTrash)
        {
            animator.SetBool("IsWalking", false);
            return;
        }

        Vector2 moveDir = InputManager.Instance.MoveInput.normalized;

        Vector2 moveVector = moveDir * moveSpeed;

        if (moveVector.x  != 0)
        {
            sr.flipX = moveVector.x < 0;
        }

        animator.SetBool("IsWalking", moveVector != Vector2.zero);
        rb.velocity = moveVector;
    }
}
