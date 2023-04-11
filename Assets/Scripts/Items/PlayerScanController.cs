using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerScanController : MonoBehaviour
{
    
    private static System.Random random = new System.Random();
    
    [Header("Components")]
    [SerializeField] StoreItemsController storeItemsController;
    [SerializeField] PhoneController phoneController;
    
    [Header("Configuration")]
    [SerializeField] int scanListSize = 4;

    private Queue<GameObject> objectsToScan;
    private List<GameObject> scannedObjects = new List<GameObject>();

    public void Start()
    {
        var storeItems = storeItemsController.GetItems()
            .OrderBy(a => random.Next());

        objectsToScan = new Queue<GameObject>(storeItems);
        
    }

    public List<GameObject> GetScanList()
    {
        var scanList = new List<GameObject>();
        for (int i = 0; i < scanListSize; i++)
        {
            if (objectsToScan.Count > 0)
            {
                scanList.Add(objectsToScan.Dequeue());
            }
        }

        return scanList;
    }

    public Queue<GameObject> ScanItem(GameObject item)
    {
        scannedObjects.Add(item);
        objectsToScan = new Queue<GameObject>(objectsToScan.Where(a => a != item));
        return objectsToScan;
    }
    
    public List<GameObject> GetScannedObjects()
    {
        return new List<GameObject>(scannedObjects);
    }

}
