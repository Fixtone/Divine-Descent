using System.Collections.Generic;
using NoaDebugger;
using UnityEngine;
public class MapCategoryDebugCommands : DebugCategoryBase
{
    const string FIELD_OF_VIEW_GROUP_NAME = "Field Of View";
    const string STAIRS_GROUP_NAME = "Stairs";
    const string STAIRS_DISPLAY_NAME = "Jump To Stairs";

    private bool fieldOfView = true;

    [CommandGroup(FIELD_OF_VIEW_GROUP_NAME), DisplayName(FIELD_OF_VIEW_GROUP_NAME), CommandOrder(1)]

    public bool FieldOfView
    {
        get
        {
            return fieldOfView;
        }

        set
        {
            fieldOfView = value;
            MapManager.Instance.currentMap.Draw();
        }
    }

    [CommandGroup(STAIRS_GROUP_NAME), DisplayName(STAIRS_DISPLAY_NAME), CommandOrder(1)]
    public void JumpToStairs()
    {
        if (stairsPositionIndex >= stairsPositions.Count)
        {
            stairsPositionIndex = 0;
        }

        Vector3 position = new Vector3((int)stairsPositions[stairsPositionIndex].x, (int)stairsPositions[stairsPositionIndex].y, 0);

        MapManager.Instance.currentMap.SetActorPosition(GameManager.Instance.player.GetComponent<Player>(), (int)position.x, (int)position.y);
        MapManager.Instance.currentMap.Draw();
        CameraManager.Instance.UpdateCamera();

        stairsPositionIndex++;
    }

    private List<Vector3> stairsPositions = new List<Vector3>();
    private int stairsPositionIndex = 0;

    public void AddStairsPosition(Vector3 position)
    {
        stairsPositions.Add(position);
    }


}
