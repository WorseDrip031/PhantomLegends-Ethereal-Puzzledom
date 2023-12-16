using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] Player player;
    [SerializeField] Animator animator;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] AudioManager audioManager;

    [SerializeField] GameObject InteractHitbox;

    private Vector2 movement;
    private bool canMove = true;
    private float interactTTL;
    private bool isInteractActive = false;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    void FixedUpdate()
    {
        if (canMove)
        {
            rb.MovePosition(rb.position + movement * player.getPlayerSpeed() * Time.fixedDeltaTime);
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
            audioManager.PlayRunning();
        }
        else
        {
            animator.SetBool("IsIdle", true);
            audioManager.StopRunning();
        }
    }

    public void LockPlayerMovement()
    {
        //canMove = false;
    }

    public void UnlockPlayerMovement()
    {
        canMove = true;
    }

    void OnInteract()
    {
        InteractHitbox.SetActive(true);
        isInteractActive = true;
        interactTTL = 0f;
        Debug.Log("Interact Started");
    }

    void Update()
    {
        if (isInteractActive)
        {
            interactTTL += Time.deltaTime;
            if (interactTTL > 0.1f)
            {
                InteractHitbox.SetActive(false);
                isInteractActive = false;
                Debug.Log("Interact Ended");
            }
        }
    }
}
