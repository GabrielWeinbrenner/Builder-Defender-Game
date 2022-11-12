using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TooltipTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    private string header;
    [SerializeField]
    private string content;

    public void OnMouseEnter() {
        TooltipManager.Instance.Show(content, header);
    }
    public void OnMouseExit() {
        TooltipManager.Instance.Hide();
    }
    public void OnPointerEnter(PointerEventData eventData) {
        TooltipManager.Instance.Show(content, header);
    }

    public void OnPointerExit(PointerEventData eventData) {
        TooltipManager.Instance.Hide();
    }

}
