using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProductListController : MonoBehaviour
{

    [Header("Components")] 
    [SerializeField] private int productCount = 4;
    [SerializeField] private GameObject productItemsList;
    
    private List<ProductItemController> productItems = new List<ProductItemController>();
    
    // Start is called before the first frame update
    void Start()
    {
        
        // Get all the products controller in the list
        foreach (Transform child in productItemsList.transform)
        {
            productItems.Add(child.GetComponent<ProductItemController>());
        }

        // TODO : Remove test
        List<string> names = new List<string>();
        names.Add("Test1");
        names.Add("Test2");
        names.Add("Test3");
        names.Add("Test4");

        setProducts(names);

        replaceProduct(0, "Nouveau produit");
        
    }

    public void setProducts(List<string> names)
    {
        
        if (names.Count != productCount)
        {
            Debug.LogError("ProductListController: setProducts: names count is not equal to productCount");
            return;
        }

        for (int i = 0; i < productCount; i++)
        {
            productItems[i].setText(names[i]);
            productItems[i].setOutOfStock(false);
            productItems[i].setChecked(false);
        }
    }
    
    public void updateProductStock(int productIndex, bool isOutOfStock)
    {
        productItems[productIndex].setOutOfStock(isOutOfStock);
    }

    public void replaceProduct(int index, string name, bool isOutOfStock = false)
    {
        productItems[index].setText(name);
        productItems[index].setOutOfStock(false);
        productItems[index].setChecked(isOutOfStock);
    }

    private IEnumerator delayedProductReplacement(int index, string name)
    {
        yield return new WaitForSeconds(1);
        replaceProduct(index, name);
    }
    
    public void checkAndReplaceProduct(int index, string nextProduct) 
    {
        productItems[index].setChecked(true);
        StartCoroutine(delayedProductReplacement(index, nextProduct));
    }
    
}
