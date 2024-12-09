
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))] //Make it kinematic
public class Entity : MonoBehaviour
{
    private string _prefabPath;
    public string PrefabPath
    {
        get
        {
            return _prefabPath;
        }
        set
        {
            _prefabPath = value;
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

    [SerializeField]
    protected SpriteRenderer spriteRenderer;

    protected virtual void Start() { }

    public virtual void Draw(Color color) { }
}