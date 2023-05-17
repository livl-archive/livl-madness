using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class StoreItemsController : MonoBehaviour
{
    public List<GameObject> items = new List<GameObject>();
    public List<ShelfController> shelves = new List<ShelfController> ();

    void Start()
    {
        //    Debug.Log("STARTING STORE ITEM CONTROLLER");
        //    this.loadItems();
        //    Debug.Log("ITEMS LOADED COUNT : " + this.items.Count);

        this.items.ForEach(item =>
        {
            int rndItemIdx;

            do
            {
                rndItemIdx = UnityEngine.Random.Range(0, shelves.Count);
            } while (!this.shelves[rndItemIdx].isEmptySpaceAvailable());

            this.shelves[rndItemIdx].setItem(item);
        });
    }
}
