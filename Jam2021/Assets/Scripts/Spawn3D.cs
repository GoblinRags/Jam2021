using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Spawn3D : MonoBehaviour
{
    public GameObject Prefab;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 300; i++)
        {
            Vector3 pos = Random.insideUnitSphere * 25f;
            Instantiate(Prefab, pos, Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha0))
        {
            SceneManager.LoadScene("Sandbox1");
        }
    }
}
