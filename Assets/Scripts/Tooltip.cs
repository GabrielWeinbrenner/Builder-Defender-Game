using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
[ExecuteInEditMode()]
public class Tooltip : MonoBehaviour{
    public TextMeshProUGUI header;
    public TextMeshProUGUI content;
    public LayoutElement layoutElement;
    public int characterWrapLimit;
    public void SetText(string content = "", string header = "") {
        if(string.IsNullOrEmpty(content) && string.IsNullOrEmpty(header)) {
            return;
        }
        if (string.IsNullOrEmpty(header)) {
            this.header.gameObject.SetActive(false);
        }
        else {
            this.header.gameObject.SetActive(true);
            this.header.text = header;
        }
        this.content.text = content;
    
        int headerLength = this.header.text.Length;
        int contentLength = this.content.text.Length;
        layoutElement.enabled = (headerLength > characterWrapLimit || contentLength > characterWrapLimit) ? true : false;
    }

    private void Update() {
        Vector2 position = Input.mousePosition;
        transform.position = position;
        if (Application.isEditor) {
            int headerLength = header.text.Length;
            int contentLength = content.text.Length;
            layoutElement.enabled = (headerLength > characterWrapLimit || contentLength > characterWrapLimit) ? true : false;
        }
    }
}
