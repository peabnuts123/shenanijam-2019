using System;
using UnityEngine;
using Zenject;

public class DungeonRoom : MonoBehaviour
{
    // Public references
    public bool areTriggersEnabled;

    // Public config
    [NonSerialized]
    public Vector2Int roomCoordinate;
    [NonSerialized]
    public IDungeonRoomTemplate dungeonRoomTemplate;

    [Inject]
    private DungeonManager dungeonManager;

    void Start()
    {
        if (this.dungeonRoomTemplate is RewardRoom)
        {
            RewardRoom roomRemplate = this.dungeonRoomTemplate as RewardRoom;
            // @TODO instantiate all the stuff that's needed
        }
        else if (this.dungeonRoomTemplate is MonsterRoom)
        {
            MonsterRoom roomRemplate = this.dungeonRoomTemplate as MonsterRoom;
            // @TODO instantiate all the stuff that's needed
        }
    }
}