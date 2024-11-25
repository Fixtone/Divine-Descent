using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class MoveButton : MonoBehaviour, IPointerUpHandler, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
{
    private bool isDown = false;
    [SerializeField] private Direction directionToMove;

    void Start()
    {
        
    }


    void Update()
    {
        if(isDown)
        {
            Player.Instance.MoveInDirection(directionToMove);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        UIManager.Instance.Hovering = true;
        isDown = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        UIManager.Instance.Hovering = false;
        isDown = false;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        UIManager.Instance.Hovering = true;
        if(Input.GetMouseButton(0)) isDown = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        UIManager.Instance.Hovering = false;
        isDown = false;
    }
}
