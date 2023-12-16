using UnityEngine;
using UnityEngine.InputSystem;

public class Door : MonoBehaviour
{
    [SerializeField] BoxCollider2D doorCollider;
    [SerializeField] Animator animator;
    [SerializeField] GameScript puzzle;
    [SerializeField] FinAPairGame findAPairGame;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Interact")
        {
            if (puzzle == null && findAPairGame == null)
            {
                animator.SetBool("isDoorOpen", true);
            }
            else if (findAPairGame != null)
            {
                openPuzzle(other, "FAP");
            }
            else
            {
                openPuzzle(other, "STP");
            }
        }
    }

    public void openTheDoor()
    {
        doorCollider.enabled = false;
    }

    void openPuzzle(Collider2D other, string name)
    {
        if (name == "STP")
        {
            PlayerInput playerInput = other.GetComponentInParent<PlayerInput>();
            playerInput.SwitchCurrentActionMap("Puzzle");
            GameScript gs = Instantiate(puzzle, other.transform.position, Quaternion.identity);
            gs.SetDoor(this);
            gs.SetPlayerInput(playerInput);
        }
        else
        {
            PlayerInput playerInput = other.GetComponentInParent<PlayerInput>();
            playerInput.SwitchCurrentActionMap("Puzzle");
            FinAPairGame gs = Instantiate(findAPairGame, other.transform.position, Quaternion.identity);
            gs.SetDoor(this);
            gs.SetPlayerInput(playerInput);
        }
    }

    public void PuzzleSolved()
    {
        animator.SetBool("isDoorOpen", true);
    }
}
