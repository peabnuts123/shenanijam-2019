using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Helix : MonoBehaviour
{
    // Public references
    [NotNull]
    public RewardRoom rewardRoom;

    // Private references
    [Inject]
    private DungeonManager dungeonManager;

    // Private state
    [SerializeField]
    private float attack1StrengthModifier = 1;
    [SerializeField]
    private float attack1SizeModifier = 1;
    [SerializeField]
    private float attack2StrengthModifier = 1;
    [SerializeField]
    private float attack2NumberOfProjectilesModifier = 1;
    [SerializeField]
    private float speedModifier = 1;
    [SerializeField]
    private float hitpointsModifier = 10;

    void Start()
    {
        // var difficulty = dungeonManager.GetDifficultyScoreForRoomCoordinate(rewardRoom.roomCoordinate);

        // I DIDN'T DESIGN THIS VERY WELL
        //  AND NOW THESE NUMBERS ARE ALL MAGIC

        // // 5 = double
        // // 10 = triple
        // // etc.
        // // (x ^ 1.5) / 5 from 20% to 100%
        // // 10 = getting interesting ~= 6.3 = 2.2x
        // // 30 = getting pretty sweet ~= 32.9 = 7.6x
        // // 50 = crazy as shit ~= 70.7 = 15.1x
        // float attack1StrengthCoefficient = RollCoefficient(random, min: 0.2F, max: 1F);
        // float attack1StrengthCurve = ExponentialCurve(difficulty, exponent: 1.5F, scalar: 0.2F);
        // this.attack1Strength = Mathf.Max(1, Mathf.RoundToInt(attack1StrengthCurve * attack1StrengthCoefficient));

        // // 10 = double
        // // 20 = triple
        // // etc.
        // // (x ^ 1.3) / 3 from 20% to 100%
        // // 10 = getting interesting ~= 5 = 1.5x
        // // 30 = getting pretty sweet ~= 20 = 3x
        // // 50 = crazy as shit ~= 40 = 5x
        // float attack1SizeCoefficient = RollCoefficient(random, min: 0.2F, max: 1F);
        // float attack1SizeCurve = ExponentialCurve(difficulty, exponent: 1.3F, scalar: 0.333F);
        // this.attack1Size = Mathf.Max(1, Mathf.RoundToInt(attack1SizeCurve * attack1SizeCoefficient));

        // // 5 = double
        // // 10 = triple
        // // etc.
        // // (x ^ 1.5) / 5 from 20% to 100%
        // // 10 = getting interesting ~= 6.3 = 2.2x
        // // 30 = getting pretty sweet ~= 32.9 = 7.6x
        // // 50 = crazy as shit ~= 70.7 = 15.1x
        // float attack2StrengthCoefficient = RollCoefficient(random, min: 0.2F, max: 1F);
        // float attack2StrengthCurve = ExponentialCurve(difficulty, exponent: 1.5F, scalar: 0.2F);
        // this.attack2Strength = Mathf.Max(1, Mathf.RoundToInt(attack2StrengthCurve * attack2StrengthCoefficient));

        // // 1 = 1 projectile
        // // 2 = 2 projectiles
        // // etc.
        // // (x ^ 1.5) / 10 from 20% to 100%
        // // 10 = getting interesting ~= 3
        // // 30 = getting pretty sweet ~= 16
        // // 50 = crazy as shit ~= 35
        // float attack2NumberOfProjectilesCoefficient = RollCoefficient(random, min: 0.2F, max: 1F);
        // float attack2NumberOfProjectilesCurve = ExponentialCurve(difficulty, exponent: 1.5F, scalar: 0.1F);
        // this.attack2NumberOfProjectiles = Mathf.Max(1, Mathf.RoundToInt(attack2NumberOfProjectilesCurve * attack2NumberOfProjectilesCoefficient));

        // // 5 = double
        // // 10 = triple
        // // etc.
        // // (x ^ 1.3) / 4 from 20% to 100%
        // // 10 = getting interesting ~= 5.0 = 2.0x
        // // 30 = getting pretty sweet ~= 20.8 = 5.2x
        // // 50 = crazy as shit ~= 40.4 = 9.1x
        // float speedCoefficient = RollCoefficient(random, min: 0.2F, max: 1F);
        // float speedCurve = ExponentialCurve(difficulty, exponent: 1.3F, scalar: 0.25F);
        // this.speed = Mathf.Max(1, Mathf.RoundToInt(speedCurve * speedCoefficient));

        // // 20 = 20HP
        // // 40 = 40HP
        // // etc.
        // // (x ^ 1.3) / 4 from 20% to 100%
        // // 10 = getting interesting ~= 30
        // // 30 = getting pretty sweet ~= 100
        // // 50 = crazy as shit ~= 40.4 = 9.1x
        // float hitpointsCoefficient = RollCoefficient(random, min: 0.2F, max: 1F);
        // float hitpointsCurve = ExponentialCurve(difficulty, exponent: 1.3F, scalar: 0.25F);
        // this.hitpoints = Mathf.Max(1, Mathf.RoundToInt(hitpointsCurve * hitpointsCoefficient));

        this.attack1StrengthModifier = RollModifier();
        this.attack1SizeModifier = RollModifier();
        this.attack2StrengthModifier = RollModifier();
        this.attack2NumberOfProjectilesModifier = RollModifier();
        this.speedModifier = RollModifier();
        this.hitpointsModifier = RollModifier();
    }

    // Shamelessly nabbed from http://answers.unity.com/answers/615120/view.html
    private float RollModifier()
    {
        float u, v, S;

        do
        {
            u = 2.0F * Random.Range(0F, 1F) - 1.0F;
            v = 2.0F * Random.Range(0F, 1F) - 1.0F;
            S = u * u + v * v;
        }
        while (S >= 1.0F);

        float fac = Mathf.Sqrt(-2F * Mathf.Log(S) / S);

        float statMean = 1.2F;
        float statStdDev = 0.5F;
        float statValue = statMean + ((u * fac) * statStdDev);

        // Clamp to 0.1 and 3σ
        return Mathf.Clamp(statValue, 0.1F, 1 + statStdDev * 3);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            this.Player.BeginInteractingWithHelix(this);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            this.Player.EndInteractingWithHelix();
        }
    }

    public void Consume()
    {
        rewardRoom.hasRewardBeenTaken = true;
        this.gameObject.SetActive(false);
        this.Player.EndInteractingWithHelix();
    }


    // private float RollCoefficient(System.Random random, float min, float max)
    // {
    //     return (float)(random.NextDouble() * (max - min) + min);
    // }

    // private float ExponentialCurve(int difficulty, float exponent, float scalar = 1)
    // {
    //     return Mathf.Pow(difficulty, exponent) * scalar;
    // }

    public PlayerController Player
    {
        get { return this.dungeonManager.Player; }
    }

    public float Attack1StrengthModifier
    {
        get { return this.attack1StrengthModifier; }
    }
    public float Attack1SizeModifier
    {
        get { return this.attack1SizeModifier; }
    }
    public float Attack2StrengthModifier
    {
        get { return this.attack2StrengthModifier; }
    }
    public float Attack2NumberOfProjectilesModifier
    {
        get { return this.attack2NumberOfProjectilesModifier; }
    }
    public float SpeedModifier
    {
        get { return this.speedModifier; }
    }
    public float HitpointsModifier
    {
        get { return this.hitpointsModifier; }
    }
}
