using UnityEngine;

public class Actor : Entity, IActor, IScheduleable
{
    private string _name;
    private int _speed;

    public Actor()
    {
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

    public int Time { get { return Speed; } }

    protected override void Start()
    {
        base.Start();
    }
}
