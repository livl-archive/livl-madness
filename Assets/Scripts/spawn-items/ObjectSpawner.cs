using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public List<GameObject> items;
    public List<GameObject> spawnPoints;

    private List<int> spawnIdxOccupied = new List<int>();

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            int rndItemIdx = Random.Range(0, items.Count);

            GameObject rndSpawnPoint = spawnPoints[getRandomSpawnPointIdx()];
            Vector3 randomSpawnPosition = new Vector3(rndSpawnPoint.transform.position.x, rndSpawnPoint.transform.position.y, rndSpawnPoint.transform.position.z);

            Instantiate(items[rndItemIdx], randomSpawnPosition, Quaternion.identity);
        }
    }

    int getRandomSpawnPointIdx()
    {
        int rndIdx = Random.Range(0, spawnPoints.Count);
        while(spawnIdxOccupied.Contains(rndIdx) && spawnIdxOccupied.Count < spawnPoints.Count) 
        {
            rndIdx = Random.Range(0, spawnPoints.Count);
        }
        spawnIdxOccupied.Add(rndIdx);
        return rndIdx;
    }
}
