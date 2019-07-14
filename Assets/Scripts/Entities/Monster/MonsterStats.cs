using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class MonsterStats : MonoBehaviour
{
    [Inject]
    private Damageable damageable;

    [SerializeField]
    private int _damage = 10;
    [SerializeField]
    private int _speed = 10;
    [SerializeField]
    private int _hitpoints = 10;

    public float difficulty;

    public int GetUpdatedStat(int current, float modifier)
    {
        return Mathf.Max(1, Mathf.RoundToInt(current * modifier));
    }

    public int Damage
    {
        get { return this._damage; }
        set { this._damage = value; }
    }
    public int Speed
    {
        get { return this._speed; }
        set { this._speed = value; }
    }
    public int Hitpoints
    {
        get { return this._hitpoints; }
        set
        {
            this._hitpoints = value;
            // @NOTE inform our damageable, too
            this.damageable.healthPoints = value;
        }
    }
}
