using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class MonsterRoom : DungeonRoom
{
    // Public references
    [NotNull]
    public MonsterController MonsterPrefab;
    [NonSerialized]
    public PlayerController player;


    // Private references
    [Inject]
    DiContainer Container;

    // Private state
    List<MonsterController> monsters;

    void Start()
    {
        this.monsters = new List<MonsterController>();

        foreach (SpawnInfo spawnInfo in this.RoomTemplate.spawnInfo)
        {
            var spawnPosition = (Vector3)spawnInfo.position + this.transform.position;
            MonsterController monster = Container.InstantiatePrefab(MonsterPrefab, spawnPosition, Quaternion.identity, this.transform).GetComponent<MonsterController>();
            monster.target = this.player.transform;

            MonsterStats stats = monster.GetComponent<MonsterStats>();
            stats.Damage = stats.GetUpdatedStat(stats.Damage, spawnInfo.damageMultiplier);
            stats.Speed = stats.GetUpdatedStat(stats.Speed, spawnInfo.speedMultiplier);
            stats.Hitpoints = stats.GetUpdatedStat(stats.Hitpoints, spawnInfo.hitpointsMultiplier);
            stats.difficulty = spawnInfo.difficulty;

            monster.UpdateDifficultyColor();

            this.monsters.Add(monster);
        }
    }

    private MonsterRoomTemplate RoomTemplate
    {
        get { return this.dungeonRoomTemplate as MonsterRoomTemplate; }
    }
}