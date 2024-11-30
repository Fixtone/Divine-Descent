using UnityEngine;

public class Actor : Entity, IActor, IScheduleable
{
    private string _name;
    private int _speed;

    public Actor()
    {

    }

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

    public int Speed
    {
        get
        {
            return _speed;
        }
        set
        {
            _speed = value;
        }
    }

    // Ischeduleable
    public int Time { get { return Speed; } }

    protected override void Start()
    {
        base.Start();
    }
}
