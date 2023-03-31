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

    // Start is called before the first frame update
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

    //private void loadItems()
    //{
    //    string foodFolderPath = Application.dataPath + "\\Assets\\Food";
    //    string[] subFolders = Directory.GetDirectories(foodFolderPath);

    //    foreach (string subFolder in subFolders)
    //    {
    //        string[] fbxFiles = Directory.GetFiles(subFolder, "*.fbx", SearchOption.AllDirectories);

    //        foreach (string fbxFile in fbxFiles)
    //        {
    //            //GameObject fbxObject = Resources.Load<GameObject>(fbxFile);

    //            GameObject fbxObject = AssetDatabase.LoadAssetAtPath<GameObject>("/food/apple");

    //            if (fbxObject != null)
    //            {
    //                this.items.Add(fbxObject);
    //            } else
    //            {
    //                Debug.Log("Item loaded is null " + fbxFile);
    //            }
    //        }
    //    }
    //}

    // Update is called once per frame
    void Update()
    {
        
    }
}
