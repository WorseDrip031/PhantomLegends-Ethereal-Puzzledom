using UnityEngine;

public class MainCard : MonoBehaviour
{
    [SerializeField] GameObject cardBack;
    [SerializeField] FinAPairGame gameScript;

    private int id;
    private float normalScale;

    void Start()
    {
        normalScale = transform.localScale.x;
    }

    public int GetId()
    {
        return id;
    }

    public void ChangeSprite(int id, Sprite image)
    {
        this.id = id;
        GetComponent<SpriteRenderer>().sprite = image;
    }

    public void Reveal()
    {
        if (cardBack.activeSelf && gameScript.canReveal)
        {
            cardBack.SetActive(false);
            gameScript.CardRevealed(this);
        }
    }

    public void Unreveal()
    {
        cardBack.SetActive(true);
    }

    public void Select()
    {
        transform.localScale = Vector3.one * (normalScale * 1.1f);
    }

    public void Unselect()
    {
        transform.localScale = Vector3.one * normalScale;
    }
}
