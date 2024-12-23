using UnityEngine;

public class Actor : Entity, IActor, IScheduleable
{
    public enum SubType
    {
        Animal,
        Demon,
        Dragon,
        Humanoid,
        Insect,
        Jelly,
        Plant,
        Undead
    }

    private SubType _subType;

    public SubType subType
    {
        get
        {
            return _subType;
        }
        set
        {
            _subType = value;
        }
    }

    private int _speed;

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

    public Actor()
    {
    }

    protected override void Start()
    {
        base.Start();
    }
}
