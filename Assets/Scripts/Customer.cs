using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Customer : MonoBehaviour {
    private GameObject textBubble;
    private float bubbleTimer = 2f;
    // To change to privat
    private CustomerData customerData;

    

    private void Awake() {
        textBubble = transform.Find("pfTextBubble").gameObject;
        // Creates random customer data
        customerData = new CustomerData();
        textBubble.SetActive(false);
    }
    private void Update() {
        if (CustomerManager.Instance.GetCurrentCustomer() == gameObject) {
            if (bubbleTimer > 0) {
                bubbleTimer -= Time.deltaTime;
            }
            else {
                textBubble.SetActive(true);

            }
        }
        // Check if the shop has the product
    }
    public void setCustomerData() {
        this.customerData = new CustomerData(); 
    }
    public CustomerData getCustomerData() {
        return this.customerData;
    }

}
