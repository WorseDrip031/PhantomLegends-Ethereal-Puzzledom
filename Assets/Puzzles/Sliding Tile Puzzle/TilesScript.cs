using UnityEngine;

public class TilesScript : MonoBehaviour
{
    private SpriteRenderer _sprite;

    public int interpolationFramesCount = 10;
    int elapsedFrames = 0;
    public int Number;


    public Vector3 targetPosition;
    private Vector3 correctPosition;
    public bool isCorrectPosition;

    public bool isSelected = false;
    public bool win = false;

    void Awake()
    {
        targetPosition = transform.position;
        correctPosition = transform.position;
        _sprite = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        float interpolationRatio = (float)elapsedFrames / interpolationFramesCount;
        transform.position = Vector3.Lerp(transform.position, targetPosition, 0.3f);
        elapsedFrames = (elapsedFrames + 1) % (interpolationFramesCount + 1);

        isCorrectPosition = AreVectorsEqual(targetPosition, correctPosition);

        setColors();
    }

    bool AreVectorsEqual(Vector3 v1, Vector3 v2)
    {
        v1 = new Vector3((float)System.Math.Round(v1.x, 2),
                         (float)System.Math.Round(v1.y, 2),
                         (float)System.Math.Round(v1.z, 2));

        v2 = new Vector3((float)System.Math.Round(v2.x, 2),
                         (float)System.Math.Round(v2.y, 2),
                         (float)System.Math.Round(v2.z, 2));

        return v1 == v2;
    }

    void setColors()
    {
        if (isSelected)
        {
            _sprite.color = Color.blue;
        }
        else
        {
            _sprite.color = Color.white;
        }
        if (win)
        {
            _sprite.color = Color.green;
        }
    }
}
