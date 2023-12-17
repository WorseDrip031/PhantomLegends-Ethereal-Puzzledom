using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class FinAPairGame : MonoBehaviour
{
    public const int gridRows = 3;
    public const int gridCols = 4;
    public const float offsetX = 0.3f;
    public const float offsetY = 0.4f;

    [SerializeField] MainCard originalCard;
    [SerializeField] Sprite[] sprites;
    [SerializeField] Slider slider;
    [SerializeField] float TTL;

    private MainCard[,] cardsGrid = new MainCard[gridCols, gridRows];
    private MainCard selectedCard;

    private Door door;
    private PlayerInput playerInput;
    private float winTTL = 0;
    private bool isWin = false;
    private float timeLived;

    void Awake()
    {
        slider.maxValue = TTL;
    }

    void Start()
    {
        Vector3 startPos = originalCard.transform.position;

        int[] numbers = { 0, 0, 1, 1, 2, 2, 3, 3, 4, 4, 5, 5 };
        numbers = ShuffleArray(numbers);

        for (int i = 0; i < gridCols; i++)
        {
            for (int j = 0; j < gridRows; j++)
            {
                MainCard card;
                if (i == 0 && j == 0)
                {
                    card = originalCard;
                }
                else
                {
                    card = Instantiate(originalCard, this.transform);
                }

                int index = j * gridCols + i;
                int id = numbers[index];

                card.ChangeSprite(id, sprites[id]);

                float posX = (offsetX * i) + startPos.x;
                float posY = (offsetY * j) + startPos.y;
                card.transform.position = new Vector3(posX, posY, startPos.z);

                cardsGrid[i, j] = card;
            }
        }

        timeLived = 0;
        movement = new Vector2(1, 0);
    }

    public void SetDoor(Door door)
    {
        this.door = door;
    }

    public void SetPlayerInput(PlayerInput playerInput)
    {
        this.playerInput = playerInput;
    }

    private int[] ShuffleArray(int[] numbers)
    {
        int[] newArray = numbers.Clone() as int[];
        for (int i = 0; i < newArray.Length; i++)
        {
            int tmp = newArray[i];
            int r = Random.Range(i, newArray.Length);
            newArray[i] = newArray[r];
            newArray[r] = tmp;
        }
        return newArray;
    }

    //--------------------------------------------------------------- movement


    private Vector2 movement;

    void FixedUpdate()
    {
        int horizontal = (int)movement.x;
        int vertical = (int)movement.y;

        if (horizontal != 0 || vertical != 0)
        {
            if (selectedCard != null)
            {
                selectedCard.Unselect();
            }

            int currentCol = -1;
            int currentRow = -1;

            for (int i = 0; i < gridCols; i++)
            {
                for (int j = 0; j < gridRows; j++)
                {
                    if (cardsGrid[i, j] == selectedCard)
                    {
                        currentCol = i;
                        currentRow = j;
                        break;
                    }
                }
            }

            int col = Mathf.Clamp(currentCol + horizontal, 0, gridCols - 1);
            int row = Mathf.Clamp(currentRow + vertical, 0, gridRows - 1);

            selectedCard = cardsGrid[col, row];
            selectedCard.Select();

            movement = new Vector2(0, 0);

        }
    }

    void Update()
    {
        if (score == 6)
        {
            isWin = true;
            winTTL += Time.deltaTime;
            if (winTTL > 1f)
            {
                playerInput.SwitchCurrentActionMap("Player");
                door.PuzzleSolved();
                Destroy(gameObject);
            }
        }

        if (!isWin)
        {
            timeLived += Time.deltaTime;
            slider.value = TTL - timeLived;

            if (timeLived > TTL)
            {
                playerInput.SwitchCurrentActionMap("Player");
                Destroy(gameObject);
            }
        }
    }

    void OnUp()
    {
        movement = new Vector2(0, 1);
    }

    void OnDown()
    {
        movement = new Vector2(0, -1);
    }
    void OnLeft()
    {
        movement = new Vector2(-1, 0);
    }

    void OnRight()
    {
        movement = new Vector2(1, 0);
    }

    void OnSubmit()
    {
        selectedCard.Reveal();
    }

    void OnBack()
    {
        playerInput.SwitchCurrentActionMap("Player");
        Destroy(gameObject);
    }

    //--------------------------------------------------------------- revealing

    private MainCard firstRevealed;
    private MainCard secondRevealed;
    public int score = 0;

    public bool canReveal
    {
        get { return secondRevealed == null; }
    }

    public void CardRevealed(MainCard card)
    {
        if (firstRevealed == null)
        {
            firstRevealed = card;
        }
        else
        {
            secondRevealed = card;
            StartCoroutine(CheckMatch());
        }
    }

    private IEnumerator CheckMatch()
    {
        if (firstRevealed.GetId() != secondRevealed.GetId())
        {
            yield return new WaitForSeconds(0.5f);

            firstRevealed.Unreveal();
            secondRevealed.Unreveal();
        }
        else
        {
            score++;
        }
        firstRevealed = null;
        secondRevealed = null;
    }
}
