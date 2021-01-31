using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnComets : MonoBehaviour
{

    public GameObject CometPrefab;
    public Transform CenterPoint;

    public Vector2 SpawnSize = new Vector2(1f, 1f);
    public float MinSpawnTime = 5f;
    public float MaxSpawnTime = 20f;
    public float SpawnTimer = 0f;

    public float SpawnInterval = 5f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (SpawnTimer > SpawnInterval)
        {
            //random location
            Vector2 newLoc = Vector2.zero;
            newLoc.x = Random.Range(-SpawnSize.x, SpawnSize.x);
            newLoc.y = Random.Range(-SpawnSize.y, SpawnSize.y);
            //spawn comet
            GameObject comet = Instantiate(CometPrefab, newLoc, Quaternion.identity, transform);
            SpriteRenderer sr = comet.GetComponent<SpriteRenderer>();
            sr.flipX = Random.Range(0, 2) == 0;
            
            //set color
            Color color = sr.color;
            color.a = Random.Range(.3f, 1f);
            sr.color = color;
            
            SpawnTimer = 0f;
        }
        
        SpawnTimer += Time.deltaTime;
    }
    
}
