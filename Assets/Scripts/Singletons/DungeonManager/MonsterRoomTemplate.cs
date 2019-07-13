using UnityEngine;

public class MonsterRoomTemplate : IDungeonRoomTemplate
{
    public SpawnInfo[] spawnInfo;
}
public struct SpawnInfo {
    public Vector2 position;
}