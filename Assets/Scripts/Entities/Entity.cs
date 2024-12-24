
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))] //Make it kinematic
public class Entity : MonoBehaviour
{
    public enum Type
    {
        Actor = 0,
        MapObject = 1
    }

    private Type _type;
    public Type type
    {
        get
        {
            return _type;
        }
        set
        {
            _type = value;
        }
    }

    private string _name;
    public string Name
    {
        get
        {
            return _name;
        }
        set
        {
            _name = value;
        }
    }

    public GameObject scriptableObject;

    [SerializeField]
    protected SpriteRenderer spriteRenderer;

    protected virtual void Start() { }

    public virtual void Draw(Color color) { }
}