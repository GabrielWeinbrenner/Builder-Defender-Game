using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopItem : MonoBehaviour {
    private ProductData productData = null;

    private string header;
    private string content;
   
    [SerializeField]
    private Material itemMaterial;

    private void Awake() {
        itemMaterial = transform.Find("sprite").gameObject.GetComponent<SpriteRenderer>().material;
    }
    public void SetProductData(ProductData productData) {
        this.productData = productData;
        if(this.productData != null) {
            //   itemMaterial.SetColor("_ReplacedColor", productData.replacedColor);
            //itemMaterial.SetColor("_Color", productData.color);
            header = productData.nameString;
            content = "$" + productData.price.ToString();
        }
    }
    public ProductData GetProduct() {
        return productData;
    }
    public void OnMouseDown() {
        if(productData == null) { return; }
        ShopManager.Instance.RemoveItem(1000, productData);
    }
    public void OnMouseEnter() {
        if(productData == null) { return; }
               
        TooltipManager.Instance.Show(content, header);
    }
    public void OnMouseExit() {
        if(productData == null) { return; }
        TooltipManager.Instance.Hide();
    }
}
