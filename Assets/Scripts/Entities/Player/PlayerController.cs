using System.Collections.Generic;
using UnityEngine;
using Zenject;


public class PlayerController : MonoBehaviour
{

    // Constants
    public enum Direction
    {
        Right = 0,
        Down = 1,
        Left = 2,
        Up = 3,
    }

    // Public references
    [NotNull]
    public Animator spriteAnimator;
    [NotNull]
    public Rigidbody2D rigidBody;
    [NotNull]
    public MagicProjectile MagicProjectilePrefab;
    [NotNull]
    public MagicAttack MagicAttackPrefab;
    [NotNull]
    public StatsBarController statsBarController;
    [NotNull]
    public StatusBarController statusBarController;
    [NotNull]
    public CompareScreenController compareScreenController;

    // Public config
    public float attack1RateOfFire = 2.3F;
    public float attack2RateOfFire = 10F;

    // Private references
    [Inject]
    private Damageable damageable;
    [Inject]
    private PlayerStats stats;

    // Private state
    private bool isAutopilot = false;
    private Vector2 autopilotDirection;
    private Helix currentInteractingHelix;
    private float attack1Timer = 0;
    private float attack2Timer = 0;
    private List<Helix> collectedHelixes;


    void Start()
    {
        this.damageable.OnDeath += this.OnDeath;
        this.damageable.OnDamage += this.OnDamage;

        this.collectedHelixes = new List<Helix>();
        RefreshStatsBar();
        this.statusBarController.HelixCount = this.collectedHelixes.Count;
    }

    void Update()
    {
        UpdateMovement();

        if (this.IsInteractingWithHelix)
        {
            UpdateHelixInteraction();
        }
        else
        {
            UpdateAttacks();
        }
    }

    private void UpdateMovement()
    {
        float playerSpeed = this.stats.Speed;

        if (!this.isAutopilot)
        {
            this.rigidBody.velocity = new Vector2(Input.GetAxis("Horizontal") * playerSpeed, Input.GetAxis("Vertical") * playerSpeed);
        }
        else
        {
            // Player is on autopilot
            this.rigidBody.velocity = this.autopilotDirection * playerSpeed;
        }

        var deltaX = this.rigidBody.velocity.x;
        var deltaY = this.rigidBody.velocity.y;

        if (Mathf.Abs(deltaY) > Mathf.Abs(deltaX))
        {
            // Moving more vertically than horizontally
            if (deltaY < 0)
            {
                this.spriteAnimator.SetInteger("Direction", (int)Direction.Down);
            }
            else if (deltaY > 0)
            {
                this.spriteAnimator.SetInteger("Direction", (int)Direction.Up);
            }
        }
        else
        {
            // Moving more horizontally than vertically
            if (deltaX < 0)
            {
                this.spriteAnimator.SetInteger("Direction", (int)Direction.Left);
            }
            else if (deltaX > 0)
            {
                this.spriteAnimator.SetInteger("Direction", (int)Direction.Right);
            }
        }
    }

    private void UpdateAttacks()
    {
        if (this.attack1Timer <= 0)
        {
            if (Input.GetButtonDown("Action1"))
            {
                MagicAttack attack = Instantiate(MagicAttackPrefab, this.transform.position, Quaternion.AngleAxis(Random.Range(0, 360), Vector3.forward));
                float damageModifier = Mathf.Max(0.5F, this.stats.Attack1Strength / 5F);
                float sizeModifier = Mathf.Max(0.5F, this.stats.Attack1Size / 10F);
                attack.Initialise(damageModifier, sizeModifier);

                // Reset attack timer
                this.attack1Timer = 1.0F / this.attack1RateOfFire;
                Debug.Log($"Set attack1 timer to {this.attack1Timer}");
            }
        }
        else
        {
            // Count down attack timer
            this.attack1Timer -= Time.deltaTime;
        }

        if (this.attack2Timer <= 0)
        {
            if (Input.GetButtonDown("Action2"))
            {
                // Find direction based on player's current facing direction
                float projectileSpeed = 15F;
                Direction projectileDirection = (Direction)this.spriteAnimator.GetInteger("Direction");
                Vector2 baseVelocity = Vector2.zero;
                if (projectileDirection == Direction.Right)
                {
                    baseVelocity = Vector2.right * projectileSpeed;
                }
                else if (projectileDirection == Direction.Down)
                {
                    baseVelocity = Vector2.down * projectileSpeed;
                }
                else if (projectileDirection == Direction.Left)
                {
                    baseVelocity = Vector2.left * projectileSpeed;
                }
                else if (projectileDirection == Direction.Up)
                {
                    baseVelocity = Vector2.up * projectileSpeed;
                }

                // Create N projectiles scattered across a range
                float damageModifier = Mathf.Max(0.5F, this.stats.Attack2Strength / 5F);
                int numProjectiles = this.stats.Attack2NumberOfProjectiles;

                // Scatter range is wider for more projectiles
                float scatterAngle = Mathf.Min(90, 9F * numProjectiles);

                for (int i = 0; i < numProjectiles; i++)
                {
                    MagicProjectile projectile = Instantiate(MagicProjectilePrefab, this.transform.position, Quaternion.AngleAxis(Random.Range(0, 360), Vector3.forward));

                    float angle = Random.Range(-scatterAngle / 2, scatterAngle / 2);
                    projectile.Initialise(Quaternion.AngleAxis(angle, Vector3.forward) * baseVelocity, damageModifier);
                }

                // Reset attack timer
                this.attack2Timer = 1.0F / this.attack2RateOfFire;
            }
        }
        else
        {
            // Count down attack timer 
            this.attack2Timer -= Time.deltaTime;
        }
    }

    private void UpdateHelixInteraction()
    {
        if (Input.GetButtonDown("Action1"))
        {
            this.collectedHelixes.Add(this.currentInteractingHelix);
            this.currentInteractingHelix.Consume();
            this.statusBarController.HelixCount = this.collectedHelixes.Count;
        }
        else if (Input.GetButtonDown("Action2"))
        {
            Debug.Log("Stats Before:");
            // @TODO
            DumpStats();
            this.stats.ApplyHelix(this.currentInteractingHelix);
            this.currentInteractingHelix.Consume();
            Debug.Log("Stats After:");
            DumpStats();

            // Heal
            this.stats.FullHeal();

            RefreshStatsBar();
        }
    }

    void DumpStats()
    {
        Debug.Log("Attack1Strength: " + this.stats.Attack1Strength);
        Debug.Log("Attack1Size: " + this.stats.Attack1Size);
        Debug.Log("Attack2Strength: " + this.stats.Attack2Strength);
        Debug.Log("Attack2NumberOfProjectiles: " + this.stats.Attack2NumberOfProjectiles);
        Debug.Log("Speed: " + this.stats.Speed);
        Debug.Log("Hitpoints: " + this.stats.Hitpoints);
    }

    void RefreshStatsBar()
    {
        this.statsBarController.BlastStrength = this.stats.Attack1Strength;
        this.statsBarController.BlastSize = this.stats.Attack1Size;
        this.statsBarController.OrbStrength = this.stats.Attack2Strength;
        this.statsBarController.OrbCount = this.stats.Attack2NumberOfProjectiles;
        this.statsBarController.Speed = this.stats.Speed;
        this.statsBarController.MaxHealth = this.stats.Hitpoints;
        this.statsBarController.CurrentHealth = Mathf.CeilToInt(this.damageable.healthPoints);
    }

    public void BeginAutopilot(Vector2 direction)
    {
        this.isAutopilot = true;
        this.autopilotDirection = direction.normalized;
    }

    public void EndAutopilot()
    {
        this.isAutopilot = false;
        this.autopilotDirection = Vector2.zero;
    }

    public void BeginInteractingWithHelix(Helix helix)
    {
        this.currentInteractingHelix = helix;

        // Populate compare screen
        this.compareScreenController.BlastPowerCurrent = this.stats.Attack1Strength;
        this.compareScreenController.BlastPowerMultiplier = helix.Attack1StrengthModifier;
        this.compareScreenController.BlastPowerResult = this.stats.GetUpdatedStat(this.stats.Attack1Strength, helix.Attack1StrengthModifier);

        this.compareScreenController.BlastSizeCurrent = this.stats.Attack1Size;
        this.compareScreenController.BlastSizeMultiplier = helix.Attack1SizeModifier;
        this.compareScreenController.BlastSizeResult = this.stats.GetUpdatedStat(this.stats.Attack1Size, helix.Attack1SizeModifier);

        this.compareScreenController.OrbPowerCurrent = this.stats.Attack2Strength;
        this.compareScreenController.OrbPowerMultiplier = helix.Attack2StrengthModifier;
        this.compareScreenController.OrbPowerResult = this.stats.GetUpdatedStat(this.stats.Attack2Strength, helix.Attack2StrengthModifier);

        this.compareScreenController.OrbCountCurrent = this.stats.Attack2NumberOfProjectiles;
        this.compareScreenController.OrbCountMultiplier = helix.Attack2NumberOfProjectilesModifier;
        this.compareScreenController.OrbCountResult = this.stats.GetUpdatedStat(this.stats.Attack2NumberOfProjectiles, helix.Attack2NumberOfProjectilesModifier);

        this.compareScreenController.SpeedCurrent = this.stats.Speed;
        this.compareScreenController.SpeedMultiplier = helix.SpeedModifier;
        this.compareScreenController.SpeedResult = this.stats.GetUpdatedStat(this.stats.Speed, helix.SpeedModifier);

        this.compareScreenController.HealthCurrent = this.stats.Hitpoints;
        this.compareScreenController.HealthMultiplier = helix.HitpointsModifier;
        this.compareScreenController.HealthResult = this.stats.GetUpdatedStat(this.stats.Hitpoints, helix.HitpointsModifier);


        this.compareScreenController.gameObject.SetActive(true);
        this.statsBarController.gameObject.SetActive(false);
    }

    public void EndInteractingWithHelix()
    {
        this.currentInteractingHelix = null;

        this.compareScreenController.gameObject.SetActive(false);
        this.statsBarController.gameObject.SetActive(true);
    }

    void OnDeath()
    {
        Debug.Log("YOU LOSE!");
        // @TODO @DEBUG
        Destroy(this.gameObject);
    }

    void OnDamage()
    {
        this.spriteAnimator.SetTrigger("Damage");
        RefreshStatsBar();
    }

    private bool IsInteractingWithHelix
    {
        get { return this.currentInteractingHelix != null; }
    }
}
