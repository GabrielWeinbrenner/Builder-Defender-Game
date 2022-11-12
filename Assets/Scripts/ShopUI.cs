using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopUI : MonoBehaviour
{
    private TextMeshProUGUI balanceText;
    public void Awake() {
        balanceText = transform.Find("balance").GetComponent<TextMeshProUGUI>(); 
        
    }
    public void Start() {
        ShopManager.OnBalanceChanged += ShopManager_OnBalanceChanged;
        SetBalanceText();
    }

    private void ShopManager_OnBalanceChanged(object sender, EventArgs e) {
        SetBalanceText();
    }
    public void SetBalanceText() {
        balanceText.SetText("Balance $"+ShopManager.Instance.GetBalance().ToString());
    }
}
