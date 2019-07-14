using UnityEngine;

public class RewardRoom : DungeonRoom
{
    // Public references
    [NotNull]
    public Helix helix;
    
    // Public state
    public bool hasRewardBeenTaken;

    
    void Start()
    {
        // Hide reward if it has been taken
        this.hasRewardBeenTaken = this.RoomTemplate.hasRewardBeenTaken;
        this.helix.gameObject.SetActive(!this.hasRewardBeenTaken);
    }

    private RewardRoomTemplate RoomTemplate
    {
        get { return this.dungeonRoomTemplate as RewardRoomTemplate; }
    }
}