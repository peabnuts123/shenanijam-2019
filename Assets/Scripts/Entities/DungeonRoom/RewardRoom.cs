using UnityEngine;

public class RewardRoom : DungeonRoom
{
    void Start()
    {
        // RewardRoom roomRemplate = this.dungeonRoomTemplate as RewardRoom;
        // @TODO instantiate all the stuff that's needed
    }

    private RewardRoomTemplate RoomTemplate
    {
        get { return this.dungeonRoomTemplate as RewardRoomTemplate; }
    }
}