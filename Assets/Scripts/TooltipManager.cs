using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TooltipManager : MonoBehaviour
{
    public static TooltipManager Instance = null;

    [SerializeField]
    private Tooltip tooltip;
    private void Awake() {

        if(Instance == null) {
            Instance = this;
        } else {
            Destroy(gameObject);
        }
        Hide();
    }
    public void Show(string content, string header = "") {
        tooltip.SetText(content, header);
        Instance.tooltip.gameObject.SetActive(true);
    }
    public void Hide() {
        Instance.tooltip.gameObject.SetActive(false);
    }

}
