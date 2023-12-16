using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogOfWarSpawner : MonoBehaviour
{
    [SerializeField] FogOfWar fogOfWarPrefab;
    [SerializeField] float increment;
    [SerializeField] float mapWidth;
    [SerializeField] float mapHeight;

    void Awake()
    {
        for (float i = 0; i < mapWidth; i += increment)
        {
            for (float j = 0; j < mapHeight; j += increment)
            {
                Instantiate(fogOfWarPrefab, new Vector3(transform.position.x + i, transform.position.y + j, 0), Quaternion.identity);
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
