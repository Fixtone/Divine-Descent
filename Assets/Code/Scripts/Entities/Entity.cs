
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))] //Make it kinematic
public class Entity : MonoBehaviour
{
    protected virtual void Start()
    {
    }
}