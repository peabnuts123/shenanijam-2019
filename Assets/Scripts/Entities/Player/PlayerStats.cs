using UnityEngine;
using Zenject;


public class PlayerStats : MonoBehaviour
{
    // Private references
    [Inject]
    Damageable damageable;

    [SerializeField]
    private int _attack1Strength = 1;
    [SerializeField]
    private int _attack1Size = 1;
    [SerializeField]
    private int _attack2Strength = 1;
    [SerializeField]
    private int _attack2NumberOfProjectiles = 1;
    [SerializeField]
    private int _speed = 1;
    [SerializeField]
    private int _hitpoints = 10;

    void Start()
    {
        // Force update to hitpoints
        FullHeal();
    }

    public void ApplyHelix(Helix helix)
    {
        this.Attack1Strength = GetUpdatedStat(this.Attack1Strength, helix.Attack1StrengthModifier);
        this.Attack1Size = GetUpdatedStat(this.Attack1Size, helix.Attack1SizeModifier);

        this.Attack2Strength = GetUpdatedStat(this.Attack2Strength, helix.Attack2StrengthModifier);
        this.Attack2NumberOfProjectiles = GetUpdatedStat(this.Attack2NumberOfProjectiles, helix.Attack2NumberOfProjectilesModifier);

        this.Speed = GetUpdatedStat(this.Speed, helix.SpeedModifier);

        this.Hitpoints = GetUpdatedStat(this.Hitpoints, helix.HitpointsModifier);
    }

    public int GetUpdatedStat(int current, float modifier)
    {
        return Mathf.Max(1, Mathf.RoundToInt(current * modifier));
    }

    public void FullHeal()
    {
        this.Hitpoints = this.Hitpoints;
    }


    // Properties
    public int Attack1Strength
    {
        get { return this._attack1Strength; }
        private set { this._attack1Strength = value; }
    }
    public int Attack1Size
    {
        get { return this._attack1Size; }
        private set { this._attack1Size = value; }
    }
    public int Attack2Strength
    {
        get { return this._attack2Strength; }
        private set { this._attack2Strength = value; }
    }
    public int Attack2NumberOfProjectiles
    {
        get { return this._attack2NumberOfProjectiles; }
        private set { this._attack2NumberOfProjectiles = value; }
    }
    public int Speed
    {
        get { return this._speed; }
        private set { this._speed = value; }
    }
    public int Hitpoints
    {
        get { return this._hitpoints; }
        private set
        {
            this._hitpoints = value;
            // @NOTE when setting hitpoints - also set value on
            //  damageable - "Healing" the player
            this.damageable.healthPoints = value;
        }
    }
}