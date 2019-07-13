using UnityEngine;

public class MonsterRoom : MonoBehaviour
{
    public IDungeonRoomTemplate dungeonRoomTemplate;

    void Start()
    {
        MonsterRoom roomRemplate = this.dungeonRoomTemplate as MonsterRoom;
        // @TODO instantiate all the stuff that's needed
    }
}