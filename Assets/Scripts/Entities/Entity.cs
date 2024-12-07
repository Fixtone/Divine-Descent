
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))] //Make it kinematic
public class Entity : MonoBehaviour
{
    [SerializeField]
    protected SpriteRenderer spriteRenderer;

    protected virtual void Start() { }

    public virtual void Draw(Color color) { }
}