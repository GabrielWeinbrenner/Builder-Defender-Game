using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProductUISprite : MonoBehaviour {
    private ProductData productData;
    public void pointerEnter() {
        TooltipManager.Instance.Show(productData.price.ToString(), productData.nameString);
    }
    public void pointerExit() {
        TooltipManager.Instance.Hide();
    }
    public void setProductData(ProductData pd) {
        this.productData = pd; 
    }
}
