using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class CustomerUI : MonoBehaviour {
	public static CustomerUI Instance = null;
	private Transform characterTransform;
	private TextMeshProUGUI price;
	private TextMeshProUGUI customerName;
	private TextMeshProUGUI customerPersuadability;
	private TextMeshProUGUI customerAge;
	private TextMeshProUGUI orderType;
	private TMP_InputField counterInput;
	private CustomerData currentCustomer;

	private int agreedPrice;
	private bool inAgreement = true;

	private Image product;
	private ProductUISprite productUISprite;

	private void Awake () {
		Instance = this;
		transform.Find("denyButton").GetComponent<Button>().onClick.AddListener(() => {
			CustomerManager.Instance.RemoveCustomer();
			Hide();
		});
		transform.Find("dealButton").GetComponent<Button>().onClick.AddListener(() => {
			// Get the item
			// Deduct the price from the balance of the user

			if (inAgreement) {
				if (currentCustomer.isSelling) {
					if (ShopManager.Instance.CanAfford(agreedPrice)) {
						ShopManager.Instance.AddItem(agreedPrice, currentCustomer.product);
						CustomerManager.Instance.RemoveCustomer();
						Hide();
					} else {
						Debug.Log("Haggle down the price");
					}
				} else {
					ShopManager.Instance.RemoveItem(agreedPrice, currentCustomer.product);
					CustomerManager.Instance.RemoveCustomer();
					Hide();
				}
			}
		});
		transform.Find("counterButton").GetComponent<Button>().onClick.AddListener(() => {
			// have the customer decide whether he would accept this offer or not
			// Fix the ai for this
			int counterOffer = int.Parse(GetCounterOffer());
			if (currentCustomer.isSelling) {
				if (counterOffer >= currentCustomer.GetAgreedPrice()) {
					inAgreement = true;
					agreedPrice = counterOffer;
					price.SetText("$" + agreedPrice);
				} else {
					inAgreement = false;
					price.SetText("Gotta\n go higher\n than $" + counterOffer);
				}
			} else {
				if (counterOffer <= currentCustomer.GetAgreedPrice()) {
					inAgreement = true;
					agreedPrice = counterOffer;
					price.SetText("$" + agreedPrice);
				} else {
					inAgreement = false;	
					price.SetText("Gotta\n go lower\n than $" + counterOffer);
				}
			}
		});
		characterTransform = transform.Find("characterSprite");
		price = characterTransform.Find("textBubble").Find("price").GetComponent<TextMeshProUGUI>();

		customerAge = characterTransform.Find("statsBoard").Find("age").GetComponent<TextMeshProUGUI>();
		customerPersuadability = characterTransform.Find("statsBoard").Find("persuasion").GetComponent<TextMeshProUGUI>();
		customerName = characterTransform.Find("statsBoard").Find("name").GetComponent<TextMeshProUGUI>();

		product = transform.Find("productSprite").GetComponent<Image>();
		productUISprite = transform.Find("productSprite").GetComponent<ProductUISprite>();
		orderType = transform.Find("orderType").GetComponent<TextMeshProUGUI>();
		counterInput = transform.Find("higherLowerContainer").Find("counterPrice").GetComponent<TMP_InputField>();

		Hide();
	}
	public void Show () {
		GameObject customerFromManager = CustomerManager.Instance.GetCurrentCustomer();
		gameObject.SetActive(true);
		characterTransform.GetComponent<Image>().sprite = customerFromManager.GetComponent<SpriteRenderer>().sprite;

		currentCustomer = customerFromManager.GetComponent<Customer>().getCustomerData();

		product.sprite = currentCustomer.product.sprite;
		productUISprite.setProductData(currentCustomer.product);
		agreedPrice = currentCustomer.GetInitialPrice();
		price.SetText("$" + agreedPrice);
		counterInput.SetTextWithoutNotify("" + agreedPrice);
		customerName.SetText(currentCustomer.nameString);
		customerAge.SetText("Age \n" + currentCustomer.age.ToString());
		customerPersuadability.SetText("Persuadability \n" + Mathf.RoundToInt(currentCustomer.persuadibility * 100).ToString() + "%");

		orderType.SetText(currentCustomer.isSelling ? "selling" : "buying");

		Color orderTypeColor;
		if (currentCustomer.isSelling) {
			orderTypeColor = new Color(0f, 1f, 0f);
			// orderTypeColor = new Color(65, 196, 88);
		} else {
			orderTypeColor = new Color(1f, 0f, 0f);
			// orderTypeColor = new Color(196, 79, 65);
		}
		orderType.color = orderTypeColor;
	}

	public void Hide () {
		gameObject.SetActive(false);
	}
	private string GetCounterOffer () {
		return counterInput.text;
	}
}
