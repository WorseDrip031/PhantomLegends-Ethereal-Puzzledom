using UnityEngine;
using UnityEngine.InputSystem;

public class GameScript : MonoBehaviour
{
    [SerializeField] Transform emptySpace = null;
    [SerializeField] TilesScript[] tiles;

    private Door door;

    private int emptySpaceIndex = 8;
    private int selectedIndex = 0;

    private float winTTL;
    private bool isWin = false;

    private float gameTTL = 0f;

    private PlayerInput playerInput;

    private float swappedTTL = 0f;
    private bool canSwap = true;

    void Start()
    {
        Shuffle();
        tiles[selectedIndex].isSelected = true;
    }

    void Update()
    {
        gameTTL += Time.deltaTime;
        if (gameTTL > 2f)
        {
            if ((correctTiles() == tiles.Length - 1) && !isWin)
            {
                win();
            }

            if (isWin)
            {
                winTTL += Time.deltaTime;
                if (winTTL > 1f)
                {
                    playerInput.SwitchCurrentActionMap("Player");
                    door.PuzzleSolved();
                    Destroy(gameObject);
                }
            }
        }

        swappedTTL += Time.deltaTime;
        if (swappedTTL > 0.2f)
        {
            canSwap = true;
        }
    }

    public void SetDoor(Door door)
    {
        this.door = door;
    }

    public void SetPlayerInput(PlayerInput playerInput)
    {
        this.playerInput = playerInput;
    }

    void OnUp()
    {
        ChangeSelection(Vector2.up);
    }

    void OnDown()
    {
        ChangeSelection(Vector2.down);
    }
    void OnLeft()
    {
        ChangeSelection(Vector2.left);
    }

    void OnRight()
    {
        ChangeSelection(Vector2.right);
    }

    void OnSubmit()
    {
        if (canSwap)
        {
            swappedTTL = 0;
            canSwap = false;
            SwapWithEmpty();
            Debug.Log("Yeahn");
        }
    }

    void OnBack()
    {
        playerInput.SwitchCurrentActionMap("Player");
        Destroy(gameObject);
    }

    void SwapWithEmpty()
    {
        if (tiles[selectedIndex] != null && Vector2.Distance(emptySpace.position, tiles[selectedIndex].transform.position) < 0.4f)
        {
            Vector2 lastEmptySpacePosition = emptySpace.position;
            TilesScript thisTile = tiles[selectedIndex].transform.GetComponent<TilesScript>();
            emptySpace.position = thisTile.targetPosition;
            thisTile.targetPosition = lastEmptySpacePosition;

            selectedIndex = emptySpaceIndex;

            int tileIndex = findIndex(thisTile);
            tiles[emptySpaceIndex] = tiles[tileIndex];
            tiles[tileIndex] = null;
            emptySpaceIndex = tileIndex;
            Debug.Log("Changed position");
        }
    }

    void win()
    {
        foreach (var tile in tiles)
        {
            if (!tile)
                continue;
            tile.win = true;
        }
        isWin = true;
        winTTL = 0;
    }

    void ChangeSelection(Vector2 direction)
    {
        int row = selectedIndex / 3;
        int col = selectedIndex % 3;

        if (direction == Vector2.up) row--;
        else if (direction == Vector2.down) row++;
        else if (direction == Vector2.left) col--;
        else if (direction == Vector2.right) col++;

        if (row >= 0 && row < 3 && col >= 0 && col < 3)
        {
            int newIndex = row * 3 + col;

            if (tiles[newIndex] == null)
            {
                if (row == 0 && (col == 0 || col == 2))
                {
                    return;
                }
                if (row == 2 && (col == 0 || col == 2))
                {
                    return;
                }
                if (row == 0 && col == 1 && direction == Vector2.up)
                {
                    return;
                }
                if (row == 2 && col == 1 && direction == Vector2.down)
                {
                    return;
                }
                if (row == 1 && col == 0 && direction == Vector2.left)
                {
                    return;
                }
                if (row == 1 && col == 2 && direction == Vector2.right)
                {
                    return;
                }

                if (direction == Vector2.up) row--;
                else if (direction == Vector2.down) row++;
                else if (direction == Vector2.left) col--;
                else if (direction == Vector2.right) col++;

                newIndex = row * 3 + col;
            }

            if (tiles[selectedIndex] != null)
            {
                tiles[selectedIndex].isSelected = false;
            }
            if (tiles[newIndex] != null)
            {
                tiles[newIndex].isSelected = true;
            }

            selectedIndex = newIndex;
        }
    }

    public void Shuffle()
    {
        do
        {
            for (int i = 0; i < 8; i++)
            {
                var lastPos = tiles[i].targetPosition;
                int ranInd = Random.Range(0, 7);
                tiles[i].targetPosition = tiles[ranInd].targetPosition;
                tiles[ranInd].targetPosition = lastPos;

                var tile = tiles[i];
                tiles[i] = tiles[ranInd];
                tiles[ranInd] = tile;
            }
            Debug.Log("Shuffeled a new one");
        } while (inversion() % 2 == 1);
    }

    public int findIndex(TilesScript ts)
    {
        for (int i = 0; i < tiles.Length; i++)
            if (tiles[i] != null)
                if (tiles[i] == ts)
                    return i;
        return -1;
    }

    int inversion()
    {
        int inversionSum = 0;
        for (int i = 0; i < tiles.Length; ++i)
        {
            if (!tiles[i])
                continue;
            for (int j = i; j < tiles.Length; ++j)
            {
                if (!tiles[j])
                    continue;
                if (tiles[j].Number > tiles[i].Number)
                {
                    inversionSum++;
                }
            }
        }
        return inversionSum;
    }

    int correctTiles()
    {
        int sum = 0;
        foreach (var t in tiles)
        {
            if (!t)
                continue;
            if (t.isCorrectPosition)
                sum++;
        }
        return sum;
    }
}
