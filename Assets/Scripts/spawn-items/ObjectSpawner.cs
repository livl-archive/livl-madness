using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public List<GameObject> items;
    public List<GameObject> spawnPoints;

    private List<int> spawnIdxOccupied = new List<int>();

    void Start()
    {
        spawnPoints.ForEach(spawnPoint =>
        {
            int rndItemIdx = Random.Range(0, items.Count);
            Vector3 randomSpawnPosition = new Vector3(spawnPoint.transform.position.x, spawnPoint.transform.position.y, spawnPoint.transform.position.z);

            Instantiate(items[rndItemIdx], randomSpawnPosition, Quaternion.identity);
        });
    }
}
