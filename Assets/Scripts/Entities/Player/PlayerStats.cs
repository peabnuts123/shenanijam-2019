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
        this.Hitpoints = this.Hitpoints;
    }

    public void ApplyHelix(Helix helix)
    {
        this.Attack1Strength = Mathf.Max(1, Mathf.RoundToInt(this.Attack1Strength * helix.Attack1StrengthModifier));
        this.Attack1Size = Mathf.Max(1, Mathf.RoundToInt(this.Attack1Size * helix.Attack1SizeModifier));

        this.Attack2Strength = Mathf.Max(1, Mathf.RoundToInt(this.Attack2Strength * helix.Attack2StrengthModifier));
        this.Attack2NumberOfProjectiles = Mathf.Max(1, Mathf.RoundToInt(this.Attack2NumberOfProjectiles * helix.Attack2NumberOfProjectilesModifier));

        this.Speed = Mathf.Max(1, Mathf.RoundToInt(this.Speed * helix.SpeedModifier));

        this.Hitpoints = Mathf.Max(1, Mathf.RoundToInt(this.Hitpoints * helix.HitpointsModifier));
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