using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements.Experimental;
using UnityEngine.Windows;

public partial class Player
{
    [Header("Movements")]
    //Has a reference to a singleton Instance of InputManager
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator animator;
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private PlayerInputHandler inputHandler;
    private void MovePlayer()
    {
        if (GameManager.Instance.GameMode != GameMode.CollectTrash)
        {
            rb.velocity = Vector2.zero;
            animator.SetBool("IsWalking", false);
            return;
        }
        Vector2 moveDir = inputHandler.MovementInput;
        Vector2 moveVector = moveDir * moveSpeed;

        UpdateFlip(moveDir.x);

        animator.SetBool("IsWalking", moveVector != Vector2.zero);
        rb.velocity = moveVector;
    }

    public void UpdateFlip(float xInput)
    {
        if (xInput == 0) return;

        if (xInput < 0)
        {
            rb.transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
        }
        if (xInput > 0)
        {
            rb.transform.rotation = Quaternion.Euler(Vector3.zero);
        }
    }
}
