using UnityEngine;

public class CommandManager : MonoBehaviour
{
    public static CommandManager Instance;

    private void Awake()
    {
        if (Instance != null)
            Destroy(gameObject);
        else
            Instance = this;

        DontDestroyOnLoad(gameObject);
    }

    public bool MoveActor(Actor actor, Direction direction)
    {
        Vector3 actorPosition = actor.transform.localPosition;

        int x = (int)actorPosition.x;
        int y = (int)actorPosition.y;

        switch (direction)
        {
            case Direction.Up:
                {
                    y = (int)actorPosition.y + 1;
                    break;
                }
            case Direction.Down:
                {
                    y = (int)actorPosition.y - 1;
                    break;
                }
            case Direction.Left:
                {
                    x = (int)actorPosition.x - 1;
                    break;
                }
            case Direction.Right:
                {
                    x = (int)actorPosition.x + 1;
                    break;
                }
            case Direction.UpLeft:
                {
                    x = (int)actorPosition.x - 1;
                    y = (int)actorPosition.y + 1;
                    break;
                }
            case Direction.UpRight:
                {
                    x = (int)actorPosition.x + 1;
                    y = (int)actorPosition.y + 1;
                    break;
                }
            case Direction.DownLeft:
                {
                    x = (int)actorPosition.x - 1;
                    y = (int)actorPosition.y - 1;
                    break;
                }
            case Direction.DownRight:
                {
                    x = (int)actorPosition.x + 1;
                    y = (int)actorPosition.y - 1;
                    break;
                }
            default:
                {
                    return false;
                }
        }

        if (MapManager.Instance.currentMap.SetActorPosition(actor, x, y))
        {
            return true;
        }

        return false;
    }
}
