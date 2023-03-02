using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public GameObject[] items;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            int rndIdx = Random.Range(0, items.Length);
            Vector3 randomSpawnPosition = new Vector3(Random.Range(-10, 11), 0, Random.Range(-10, 11));

            Instantiate(items[rndIdx], randomSpawnPosition, Quaternion.identity);
        }
    }
}
