using UnityEngine;
using UnityEngine.EventSystems;

public class MovementBlocker : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public void OnPointerEnter(PointerEventData eventData)
    {
        UIManager.Instance.Hovering = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        UIManager.Instance.Hovering = false;
    }
}
