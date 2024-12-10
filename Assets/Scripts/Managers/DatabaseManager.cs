using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DatabaseManager : MonoBehaviour
{
    public static DatabaseManager Instance;

    [SerializeField]
    private List<MapObject> mapsDatabase;

    private void Awake()
    {
        if (Instance != null)
            Destroy(gameObject);
        else
            Instance = this;

        DontDestroyOnLoad(gameObject);
    }

    public MapObject GetMapObjectById(int id)
    {
        return mapsDatabase.Find(mapObject => mapObject.id == id);
    }

}
