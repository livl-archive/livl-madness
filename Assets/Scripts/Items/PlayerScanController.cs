using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerScanController : MonoBehaviour
{
    
    private static System.Random random = new System.Random();
    
    [Header("Components")]
    [SerializeField] private StoreItemsController storeItemsController;
    [SerializeField] private ProductListController productListController;
    
    [Header("Configuration")]
    [SerializeField] private int scanListSize = 4;

    private Queue<GameObject> scanList;
    private List<GameObject> scannedObjects = new List<GameObject>();

    public void Start()
    {
        // Find store items controller
        if (storeItemsController == null)
        {
            storeItemsController = FindObjectOfType<StoreItemsController>();
        }
        
        // Find product list controller
        if (productListController == null)
        {
            productListController = FindObjectOfType<ProductListController>();
        }


        var storeItems = storeItemsController.GetItems()
            .OrderBy(a => random.Next());

        scanList = new Queue<GameObject>(storeItems);
     
        productListController.SetProducts(GetScanListNames());
    }

    public List<GameObject> GetScanList()
    {
        var scanList = new List<GameObject>();
        for (int i = 0; i < scanListSize; i++)
        {
            if (this.scanList.Count > 0)
            {
                scanList.Add(this.scanList.Dequeue());
            }
        }

        return scanList;
    }

    public Queue<GameObject> ScanItem(GameObject item)
    {
        var itemIndex = scanList.ToList().FindIndex(a => a == item);
        scannedObjects.Add(item);
        scanList = new Queue<GameObject>(scanList.Where(a => a != item));
        productListController.CheckAndReplace(itemIndex, GetScanListNames());
        return scanList;
    }
    
    public List<GameObject> GetScannedObjects()
    {
        return new List<GameObject>(scannedObjects);
    }

    public List<String> GetScanListNames()
    {
        return GetScanList()
            .Select(a => a.name)
            .ToList();
    }

}
