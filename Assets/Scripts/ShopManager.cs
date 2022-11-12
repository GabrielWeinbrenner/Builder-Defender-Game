using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    public static ShopManager Instance = null;
    public static event EventHandler OnBalanceChanged;
    [SerializeField]
    private Transform startPosition;
    /*
        [SerializeField]
        private Transform nextRowPosition;
        [SerializeField]
        private Transform nextShelfPosition;
    */

    [SerializeField]
    private int productMaxSize;
    // Static
    [SerializeField]
    private float productOffset;
    // Next shelf - startPosition
    [SerializeField]
    private float shelfOffset;
    // bottom shelf y  - starPositon y
    [SerializeField]
    private float shelfTakeoff;
    [SerializeField]
    private GameObject pfShopItem;
    
    private List<GameObject> productObjects;
    private List<ProductData> ownedProducts;

    private int balance;
    public void Awake() {
        if(Instance == null) {
            Instance = this;
        }
        else {
            Destroy(gameObject);
        }
        ProductData one = new ProductData();
        ProductData two = new ProductData();
        ProductData three = new ProductData();
        ProductData four = new ProductData();
        ProductData five = new ProductData();
        ProductData six = new ProductData();

        balance = 1000000;
        productObjects = new List<GameObject>();
        ownedProducts = new List<ProductData>();

        SetGridSystem();

        AddItem(1000, one);
        //AddItem(1000, two);
        //AddItem(1000, three);
        //AddItem(1000, four);
        //AddItem(1000, five);
        //AddItem(1000, six);
    }
    private void Update() {
    }
    public void printOwnedProducts() {
        for (int i = 0; i < ownedProducts.Count; i++) {
            Debug.Log(ownedProducts[i].nameString);
	    }
        Debug.Log("----");
     //   for (int i = 0; i < productObjects.Count; i++) {
     //       Debug.Log(productObjects[i]);
	    //}
    }

    public void AddItem(int agreedPrice, ProductData productData) {
        balance -= agreedPrice;
        OnBalanceChanged?.Invoke(this, EventArgs.Empty);
        GameObject productObject = productObjects[ownedProducts.Count];
        ownedProducts.Add(productData);

        productObject.transform.Find("sprite").GetComponent<SpriteRenderer>().sprite = productData.sprite;
        productObject.GetComponent<ShopItem>().SetProductData(productData);
        ResetProducts();
    }
    // Person was already in line for the same item 
    public void RemoveItem(int agreedPrice, ProductData productData) {
        TooltipManager.Instance.Hide();
        balance += agreedPrice;
        OnBalanceChanged?.Invoke(this,EventArgs.Empty);
        for(int i = 0; i < productObjects.Count; i++) {
            GameObject productObject = productObjects[i];
            if(productObject.GetComponent<ShopItem>().GetProduct() == productData) {
                productObject.GetComponent<ShopItem>().SetProductData(null); 
            }
        }
        ownedProducts.Remove(productData);
        ResetProducts();
    }
    public void ResetProducts() {
        for(int i = 0; i < productObjects.Count; i++) {
            productObjects[i].transform.Find("sprite").GetComponent<SpriteRenderer>().sprite = null;
        }
        for(int i = 0; i < ownedProducts.Count; i++) {
            GameObject productObject = productObjects[i];
            productObject.GetComponent<ShopItem>().SetProductData(ownedProducts[i]); 
            productObject.transform.Find("sprite").GetComponent<SpriteRenderer>().sprite = ownedProducts[i].sprite;
        }
        
    }
    public bool HasSpace() {
        if(ownedProducts.Count >= productMaxSize) {
            return false;
        }
        return true;
    }
    public bool CanAfford(int price) {
        return balance > price;
    }
    public int GetBalance() {
        return balance;
    }
    public int GetItemCount() {
        return ownedProducts.Count;
    }
    public ProductData GetRandomItem() {
        return ownedProducts[UnityEngine.Random.Range(0, ownedProducts.Count)];
    }
    public bool hasProduct(ProductData productData) { 
        for(int i = 0; i < productObjects.Count; i++) {
            GameObject productObject = productObjects[i];
            if(productObject.GetComponent<ShopItem>().GetProduct() == productData) {
                return true;
            }
        }
        return false;
    }

    public void SetGridSystem() {
        Vector3 initialPosition = startPosition.position;
        float currentY = initialPosition.y;
        float currentX = initialPosition.x;
        pfShopItem.transform.Find("sprite").GetComponent<SpriteRenderer>().sprite = null;
        for(int i = 0; i < productMaxSize; i++) {
            if(i%6 == 0 && i != 0) {
                currentY = currentY + ((i % 6 + 1) * (shelfTakeoff));
                currentX = initialPosition.x;
            } else if (i%3 == 0 && !(i%6==0) && i != 0) {
                currentX = currentX + (shelfOffset);
            }
            if (i != 0 && i % 6 != 0) {
                currentX += productOffset;
            }
            Vector3 shelfItemPosition = new Vector3(currentX, currentY);
            
            GameObject newShopItem = Instantiate(pfShopItem, shelfItemPosition, Quaternion.identity);
            productObjects.Add(newShopItem);
        }
    }
}
