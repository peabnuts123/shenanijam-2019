using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public enum RoomTransitionDirection
{
    Right = 0,
    Down = 1,
    Left = 2,
    Up = 3,
}

public class DungeonManager : MonoBehaviour
{
    // Constants
    public static readonly int DUNGEON_SEED = 20190713;


    // Public references
    [NotNull(IgnorePrefab = true)]
    public PlayerController player;
    [NotNull(IgnorePrefab = true)]
    public new Camera camera;

    // Public config
    [NotNull]
    public RewardRoom RewardRoomPrefab;
    [NotNull]
    public MonsterRoom MonsterRoomPrefab;

    public int DIMENSION_WIDTH_UNITS = 32;
    public int DIMENSION_HEIGHT_UNITS = 24;
    public float SPAWN_AREA_WIDTH_UNITS = 18;
    public float SPAWN_AREA_HEIGHT_UNITS = 14.5F;


    public float rewardRoomFrequency = 0.2F;


    // Private state
    private IDictionary<int, IDictionary<int, IDungeonRoomTemplate>> roomDictionary;
    private List<DungeonRoom> loadedRooms;
    private Vector2Int currentCoordinate;

    // Private references
    [Inject]
    private DiContainer Container;
    [Inject]
    private ValueAnimator valueAnimator;


    void Start()
    {
        this.roomDictionary = new Dictionary<int, IDictionary<int, IDungeonRoomTemplate>>();
        this.loadedRooms = new List<DungeonRoom>();

        // Load initial rooms
        LoadRoomAtCoordinate(new Vector2Int(0, 0));
        LoadRoomAtCoordinate(new Vector2Int(0, 1));
        LoadRoomAtCoordinate(new Vector2Int(1, 0));
        LoadRoomAtCoordinate(new Vector2Int(0, -1));
        LoadRoomAtCoordinate(new Vector2Int(-1, 0));

        GetRoomWithCoordinate(Vector2Int.zero).areTriggersEnabled = true;
    }

    public void BeginTransitionRoom(RoomTransitionDirection transitionDirection)
    {
        var oldCoordinate = this.currentCoordinate;
        if (transitionDirection == RoomTransitionDirection.Right)
        {
            // Load new rooms
            this.currentCoordinate += Vector2Int.right;
            LoadRoomAtCoordinate(this.currentCoordinate + Vector2Int.right);
            LoadRoomAtCoordinate(this.currentCoordinate + Vector2Int.up);
            LoadRoomAtCoordinate(this.currentCoordinate + Vector2Int.down);

            // Unload un-needed rooms
            UnloadRoomAtCoordinate(oldCoordinate + Vector2Int.up);
            UnloadRoomAtCoordinate(oldCoordinate + Vector2Int.left);
            UnloadRoomAtCoordinate(oldCoordinate + Vector2Int.down);

            // Trigger player autopilot
            player.BeginAutopilot(Vector2.right);

        }
        else if (transitionDirection == RoomTransitionDirection.Down)
        {
            // Load new rooms
            this.currentCoordinate += Vector2Int.down;
            LoadRoomAtCoordinate(this.currentCoordinate + Vector2Int.down);
            LoadRoomAtCoordinate(this.currentCoordinate + Vector2Int.right);
            LoadRoomAtCoordinate(this.currentCoordinate + Vector2Int.left);

            // Unload un-needed rooms
            UnloadRoomAtCoordinate(oldCoordinate + Vector2Int.up);
            UnloadRoomAtCoordinate(oldCoordinate + Vector2Int.left);
            UnloadRoomAtCoordinate(oldCoordinate + Vector2Int.right);

            // Trigger player autopilot
            player.BeginAutopilot(Vector2.down);
        }
        else if (transitionDirection == RoomTransitionDirection.Left)
        {
            // Load new rooms
            this.currentCoordinate += Vector2Int.left;
            LoadRoomAtCoordinate(this.currentCoordinate + Vector2Int.left);
            LoadRoomAtCoordinate(this.currentCoordinate + Vector2Int.down);
            LoadRoomAtCoordinate(this.currentCoordinate + Vector2Int.up);

            // Unload un-needed rooms
            UnloadRoomAtCoordinate(oldCoordinate + Vector2Int.up);
            UnloadRoomAtCoordinate(oldCoordinate + Vector2Int.down);
            UnloadRoomAtCoordinate(oldCoordinate + Vector2Int.right);

            // Trigger player autopilot
            player.BeginAutopilot(Vector2.left);
        }
        else if (transitionDirection == RoomTransitionDirection.Up)
        {
            // Load new rooms
            this.currentCoordinate += Vector2Int.up;
            LoadRoomAtCoordinate(this.currentCoordinate + Vector2Int.up);
            LoadRoomAtCoordinate(this.currentCoordinate + Vector2Int.left);
            LoadRoomAtCoordinate(this.currentCoordinate + Vector2Int.right);

            // Unload un-needed rooms
            UnloadRoomAtCoordinate(oldCoordinate + Vector2Int.left);
            UnloadRoomAtCoordinate(oldCoordinate + Vector2Int.right);
            UnloadRoomAtCoordinate(oldCoordinate + Vector2Int.down);

            // Trigger player autopilot
            player.BeginAutopilot(Vector2.up);
        }

        // Animate camera
        var currentCameraPosition = this.camera.transform.position;
        var newCameraPosition = ConvertRoomCoordinateToWorldCoordinate(this.currentCoordinate);
        // Animate X
        this.valueAnimator.Animate(currentCameraPosition.x, newCameraPosition.x, 1, (newX) => this.camera.transform.position = this.camera.transform.position.WithX(newX), new EaseInOut2());
        // Animate Y
        this.valueAnimator.Animate(currentCameraPosition.y, newCameraPosition.y, 1, (newY) => this.camera.transform.position = this.camera.transform.position.WithY(newY), new EaseInOut2());
    }

    public void EndTransitionRoom()
    {
        // Disable ALL triggers
        this.loadedRooms.ForEach((loadedRoom) => loadedRoom.areTriggersEnabled = false);


        // Enable the ones in the current room
        DungeonRoom room = GetRoomWithCoordinate(this.currentCoordinate);
        room.areTriggersEnabled = true;
        
        player.EndAutopilot();
    }

    void LoadRoomAtCoordinate(Vector2Int roomCoordinate)
    {
        IDungeonRoomTemplate roomTemplate = GetOrGenerateRoomTemplateForCoordinate(roomCoordinate);
        var position = ConvertRoomCoordinateToWorldCoordinate(roomCoordinate);

        GameObject loadedRoom;
        DungeonRoom dungeonRoom;
        if (roomTemplate is RewardRoomTemplate)
        {
            var template = roomTemplate as RewardRoomTemplate;

            loadedRoom = Container.InstantiatePrefab(RewardRoomPrefab);
            RewardRoom room = loadedRoom.GetComponent<RewardRoom>();
            dungeonRoom = room;
        }
        else if (roomTemplate is MonsterRoomTemplate)
        {
            var template = roomTemplate as MonsterRoomTemplate;

            loadedRoom = Container.InstantiatePrefab(MonsterRoomPrefab);
            MonsterRoom room = loadedRoom.GetComponent<MonsterRoom>();
            dungeonRoom = room;

            room.player = this.player;
        }
        else
        {
            throw new System.NotImplementedException($"Unimplemented room template of type {roomTemplate.GetType()}");
        }

        dungeonRoom.transform.transform.position = position;

        dungeonRoom.dungeonRoomTemplate = roomTemplate;
        dungeonRoom.roomCoordinate = roomCoordinate;

        this.loadedRooms.Add(dungeonRoom);
    }

    void UnloadRoomAtCoordinate(Vector2Int roomCoordinate)
    {
        DungeonRoom room = GetRoomWithCoordinate(roomCoordinate);
        if (room != null)
        {
            loadedRooms.Remove(room);
            Destroy(room.gameObject);
        }
    }

    DungeonRoom GetRoomWithCoordinate(Vector2Int roomCoordinate)
    {
        return this.loadedRooms.First((room) => room.roomCoordinate == roomCoordinate);
    }

    IDungeonRoomTemplate GetOrGenerateRoomTemplateForCoordinate(Vector2Int roomCoordinate)
    {
        // Attempt to find an existing entry
        if (this.roomDictionary.ContainsKey(roomCoordinate.x))
        {
            var columnDictionary = this.roomDictionary[roomCoordinate.x];

            if (columnDictionary.ContainsKey(roomCoordinate.y))
            {
                return columnDictionary[roomCoordinate.y];
            }
        }

        // Entry not found - generate one
        var roomTemplate = GenerateRoomTemplateForCoordinate(roomCoordinate);
        if (!this.roomDictionary.ContainsKey(roomCoordinate.x))
        {
            this.roomDictionary[roomCoordinate.x] = new Dictionary<int, IDungeonRoomTemplate>();
        }
        this.roomDictionary[roomCoordinate.x][roomCoordinate.y] = roomTemplate;

        return roomTemplate;
    }

    IDungeonRoomTemplate GenerateRoomTemplateForCoordinate(Vector2Int roomCoordinate)
    {
        int x = roomCoordinate.x;
        int y = roomCoordinate.y;

        // Set up RNG seeded by arbitrary number + also room coordinate
        //  so that every room is always generated the same but is unique
        //  for every coordinate
        float cantorCoordinate = (0.5F * ((5*x) + (5*y)) * ((5*x) + (5*y) + 1)) + (5*y);
        int seed = Mathf.RoundToInt(DUNGEON_SEED + cantorCoordinate);
        var random = new System.Random(seed);
        Debug.Log($"Random seed for coordinate ({x}, {y}): {seed} (Cantor: {cantorCoordinate})");

        if (random.NextDouble() < rewardRoomFrequency)
        {
            // Reward room $$
            var roomTemplate = new RewardRoomTemplate();

            return roomTemplate;
        }
        else
        {
            // Monster room >:E 
            // Difficulty is distance from center in number of rooms, and always at least 1
            int difficulty = Mathf.Max(1, Mathf.RoundToInt(Mathf.Sqrt(x * x + y * y)));
            // @TODO difficulty curve
            int numMonsters = difficulty;
            Debug.Log($"Generating monster room at ({x}, {y}) with {numMonsters} monsters");

            // Generate monsters
            SpawnInfo[] spawnInfo = new SpawnInfo[numMonsters];
            for (int i = 0; i < spawnInfo.Length; i++)
            {
                // Spawn within SPAWN_AREA bounds, around the center
                float spawnX = (float)(random.NextDouble() * SPAWN_AREA_WIDTH_UNITS - (SPAWN_AREA_WIDTH_UNITS / 2F));
                float spawnY = (float)(random.NextDouble() * SPAWN_AREA_HEIGHT_UNITS - (SPAWN_AREA_HEIGHT_UNITS / 2F));

                Debug.Log($"Spawning monster at position ({spawnX}, {spawnY})");

                spawnInfo[i] = new SpawnInfo
                {
                    position = new Vector2(spawnX, spawnY),
                };
            }

            var roomTemplate = new MonsterRoomTemplate
            {
                spawnInfo = spawnInfo
            };

            return roomTemplate;
        }
    }

    private Vector3 ConvertRoomCoordinateToWorldCoordinate(Vector2Int roomCoordinate)
    {
        return new Vector3(roomCoordinate.x * DIMENSION_WIDTH_UNITS, roomCoordinate.y * DIMENSION_HEIGHT_UNITS);
    }

}