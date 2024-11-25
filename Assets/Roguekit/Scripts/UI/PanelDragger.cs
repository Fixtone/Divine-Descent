using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PanelDragger : MonoBehaviour, IDragHandler, IPointerEnterHandler, IPointerExitHandler, IPointerUpHandler, IPointerDownHandler
{
    [SerializeField] private Canvas canvas;
    [SerializeField] private RectTransform rectToDrag;

    public void Start()
    {
        if (canvas == null) canvas = transform.root.GetComponent<Canvas>();
        if (rectToDrag == null) rectToDrag = transform.parent.GetComponent<RectTransform>();
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectToDrag.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {

    }

    public void OnPointerExit(PointerEventData eventData)
    {

    }

    public void OnPointerDown(PointerEventData eventData)
    {
        UIManager.Instance.Hovering = true;
        rectToDrag.SetAsLastSibling();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        UIManager.Instance.Hovering = false;
    }

   
}
