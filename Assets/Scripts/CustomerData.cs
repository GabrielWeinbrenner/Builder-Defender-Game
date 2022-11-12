using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
// [System.Serializable]
public class CustomerData {
	public string nameString;
	public float persuadibility;
	public int age;
	public ProductData product;
	public bool isSelling = UtilsClass.GetRandomBool();

	public CustomerData () {
		nameString = "Bob";
		persuadibility = UnityEngine.Random.Range(0f, 1.0f);
		age = UnityEngine.Random.Range(10, 50);
		if (ShopManager.Instance.GetItemCount() <= 0) {
			isSelling = true;
		}
		if (isSelling) {
			product = new ProductData();
		} else {
			product = ShopManager.Instance.GetRandomItem();
		}
	}

	public int GetAgreedPrice () {
		int agreedPrice = 0;
		if (isSelling) {
			// m((0.8p)+0.2(0.01a))
			agreedPrice = UtilsClass.RoundToHundreths((int)(product.price * ((0.8 * persuadibility) + (0.2 * (0.01 * age)))));
		} else {
			agreedPrice = UtilsClass.RoundToHundreths((int)(product.price * (0.6 + persuadibility)));
		}
		return agreedPrice;
	}
	public int GetInitialPrice () {
		int gl = GetAgreedPrice();
		int initialPrice = 0;
		if (isSelling) {
			initialPrice = UtilsClass.RoundToHundreths((int)(gl + gl * persuadibility));
		} else {
			initialPrice = UtilsClass.RoundToHundreths((int)(gl - gl * (1 - persuadibility)));
		}
		return initialPrice;
	}
}
