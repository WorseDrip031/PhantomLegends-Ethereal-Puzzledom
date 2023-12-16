using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FogOfWar : MonoBehaviour
{
    [SerializeField] float timeToLive;

    private Color startingColor;
    private bool fadingStarted = false;
    private float timeElapsed = 0f;

    void Start()
    {
        startingColor = gameObject.GetComponent<SpriteRenderer>().color;
    }

    void Update()
    {
        if (fadingStarted)
        {
            timeElapsed += Time.deltaTime;
            gameObject.GetComponent<SpriteRenderer>().color = new Color(startingColor.r, startingColor.g, startingColor.b, (1 - timeElapsed / timeToLive));

            if (timeElapsed > timeToLive)
            {
                Destroy(gameObject);
            }
        }
    }

    public void StartFading()
    {
        fadingStarted = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "PlayerLineOfSight")
        {
            StartFading();
        }
    }
}
