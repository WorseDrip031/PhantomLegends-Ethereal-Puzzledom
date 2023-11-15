using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public Animator animator;
    public Rigidbody2D rb;
    public float moveSpeed = 1f;

    private Vector2 movement;
    private bool canMove = true;

    void FixedUpdate()
    {
        if (canMove)
        {
            rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
        }
    }

    void OnMove(InputValue movementValue)
    {
        movement = movementValue.Get<Vector2>();
        if (movement != Vector2.zero)
        {
            animator.SetBool("IsIdle", false);
            animator.SetFloat("Horizontal", movement.x);
            animator.SetFloat("Vertical", movement.y);
        }
        else
        {
            animator.SetBool("IsIdle", true);
        }
    }

    void OnAttack()
    {
        animator.SetTrigger("Attack");
    }

    public void LockPlayerMovement()
    {
        //canMove = false;
    }

    public void UnlockPlayerMovement()
    {
        canMove = true;
    }
}
