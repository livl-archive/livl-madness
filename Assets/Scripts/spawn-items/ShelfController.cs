using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShelfController : MonoBehaviour
{
    public List<GameObject> spawnPoints;
    private List<int> usedSpawnPointsIdx;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setItem(GameObject gameObject) {

        if(usedSpawnPointsIdx.Count == spawnPointsIdx.Count)
        {
            return;
        }

        int rndItemIdx;
        do
        {
            rndItemIdx = Random.Range(0, spawnPoints.Count);
        } while(usedSpawnPointsIdx.Contains(rndItemIdx));


    }
}
