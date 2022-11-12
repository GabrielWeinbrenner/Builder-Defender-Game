using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextBubble : MonoBehaviour {
    void OnMouseDown() {
        // Setup deal with customer 
        CustomerUI.Instance.Show();
    }
}
