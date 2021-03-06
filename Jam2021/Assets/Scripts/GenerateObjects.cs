using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateObjects : MonoBehaviour
{
    public GameObject[] SpaceObjPrefabs;
    
    //public Sprite[] SpaceSprites;
    //public GameObject SpaceObjPrefab;

    public Transform ObjHolder;
    
    public Vector2 TopLeftCorner = new Vector2(-500, 500);

    public Vector2 Size = new Vector2(1000f, 100f);

    public Vector2 ChunkSize = new Vector2(50f, 50f);

    
    // Start is called before the first frame update
    void Start()
    {
        SpawnObj();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnObj()
    {
        Vector2 start = new Vector2(0f, 0f);

        while (start.y < Size.y)
        {
            while (start.x < Size.x)
            {
                Vector2 newPos = new Vector2(Random.Range(0f, ChunkSize.x), Random.Range(0f, ChunkSize.y)) +
                                 TopLeftCorner;
                newPos.x += start.x;
                newPos.y -= start.y;
                    
                    
                //spawn object at newPos
                GameObject spaceObj = Instantiate(SpaceObjPrefabs[Random.Range(0, SpaceObjPrefabs.Length)], newPos, Quaternion.identity);

                Color newColor = Color.white;
                newColor.a = Random.Range(0.2f, 1f);
                float newSize = Random.Range(.6f, 1.4f);
                Vector2 size = new Vector2(newSize, newSize);
                spaceObj.transform.localScale = size;
                spaceObj.GetComponent<SpriteRenderer>().color = newColor; 
                
                start.x += ChunkSize.x;
            }

            start.x = 0f;
            start.y += ChunkSize.y;
        }
    }
}
