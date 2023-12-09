using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ExpText : MonoBehaviour
{
    [SerializeField] float timeToLive;
    [SerializeField] float riseSpeed;

    [SerializeField] TextMeshProUGUI textMeshPro;
    [SerializeField] RectTransform rectTransform;

    private float timeElapsed = 0f;
    private Vector3 riseDirection = new Vector3(0, 1, 0);
    private Color startingColor;

    // Start is called before the first frame update
    void Start()
    {
        startingColor = textMeshPro.color;
    }

    // Update is called once per frame
    void Update()
    {
        timeElapsed += Time.deltaTime;

        rectTransform.position += riseDirection * riseSpeed * Time.deltaTime;

        textMeshPro.color = new Color(startingColor.r, startingColor.g, startingColor.b, (1 - timeElapsed / timeToLive));

        if (timeElapsed > timeToLive )
        {
            Destroy(gameObject);
        }
    }

    public void setText(string text)
    {
        textMeshPro.text = text;
    }
}
