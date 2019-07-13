using UnityEngine;

public class RewardRoom : MonoBehaviour
{
    public IDungeonRoomTemplate dungeonRoomTemplate;

    void Start()
    {
        RewardRoom roomRemplate = this.dungeonRoomTemplate as RewardRoom;
        // @TODO instantiate all the stuff that's needed
    }
}