using UnityEngine;
using Zenject;

public class DungeonRoomDoorTrigger : MonoBehaviour
{
    // Public references
    [NotNull]
    public DungeonRoom dungeonRoom;

    // Public config
    public RoomTransitionDirection transitionDirection;

    // Private references
    [Inject]
    private DungeonManager dungeonManager;


    void OnTriggerEnter2D(Collider2D other)
    {
        if (this.AreTriggersEnabled && other.tag == "Player")
        {
            this.dungeonManager.BeginTransitionRoom(this.transitionDirection);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            this.dungeonManager.EndTransitionRoom();
        }
    }

    public bool AreTriggersEnabled
    {
        get { return this.dungeonRoom.areTriggersEnabled; }
    }
}