using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CustomerManager : MonoBehaviour {
    [SerializeField]
    private Transform spawnPosition;
    [SerializeField]
    private GameObject pfCustomer;


    public static CustomerManager Instance = null;
    
    private Sprite currentSprite;
    private Sprite[] allSprites;
    private List<GameObject> currentCustomers;


    private float spawnTimer = 4f;
    private void Awake() {
        if(Instance == null) {
            Instance = this;
        }
        else {
            Destroy(gameObject);
        }
        currentCustomers = new List<GameObject>();
        allSprites = Resources.LoadAll("Characters", typeof(Sprite)).Cast<Sprite>().ToArray();
        Spawn();
    }
    private void Update() {
        if (spawnTimer > 0) {
            spawnTimer -= Time.deltaTime;
        }
        else {
            spawnTimer += UnityEngine.Random.Range(2, 4);
            Spawn();
        }
        if (Input.GetKeyDown(KeyCode.I)) {
            RemoveCustomer();
        }
    }

    private void Spawn() {
        
        // Get the spawn Position
        Vector3 customerPosition = spawnPosition.position;
        // Set the offset
        customerPosition.x = customerPosition.x - (customerPosition.x * (currentCustomers.Count));
        Vector3 cameraCustomerPosition = Camera.main.WorldToViewportPoint(customerPosition);
        if(cameraCustomerPosition.x > 0) {
            // Get a random sprite
            currentSprite = allSprites[UnityEngine.Random.Range(0, allSprites.Length)];
            // Set the sprite 
            pfCustomer.GetComponent<SpriteRenderer>().sprite = currentSprite;
            // Based on how many customers spawn it in a position
            // Set the height and width of the customer
            Transform customerTransform = pfCustomer.GetComponent<Transform>();
            // customerTransform.localScale = new Vector3(.3f, .3f,1);
            // Intantiate the customer 
            GameObject newCustomer = Instantiate(pfCustomer, customerPosition, spawnPosition.rotation);
            currentCustomers.Add(newCustomer);
        }
    }
    private void ResetLine() {
        int index = 0;
        foreach(GameObject customer in currentCustomers) {
            // Quick fix for when shop items change 
            customer.GetComponent<Customer>().setCustomerData();
            Vector3 customerPosition = spawnPosition.position;
            customerPosition.x = customerPosition.x - (customerPosition.x * index);
            customer.transform.SetPositionAndRotation(customerPosition,Quaternion.identity.normalized);
            index++;
        } 
    }
    public void RemoveCustomer() {
        Destroy(GetCurrentCustomer());
        currentCustomers.Remove(GetCurrentCustomer());
        ResetLine();
    }
    public GameObject GetCurrentCustomer() {
        return currentCustomers.First();  
    }



}
