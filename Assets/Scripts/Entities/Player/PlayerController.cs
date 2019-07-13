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

    // Public config
    public float speed = 3F;
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
    private float attack1Timer = 0;
    private float attack2Timer = 0;

    void Start()
    {
        this.damageable.OnDeath += this.OnDeath;
    }

    void Update()
    {
        UpdateMovement();
        UpdateAttacks();
    }

    private void UpdateMovement()
    {
        float playerSpeed = this.speed + this.stats.Speed;
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
                float damageModifier = 1 + this.stats.Attack1Strength / 5F;
                float sizeModifier = 1 + this.stats.Attack1Size / 10F;
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
                float damageModifier = 1 + this.stats.Attack2Strength / 5F;
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

    void OnDeath()
    {
        Debug.Log("YOU LOSE!");
        // @TODO @DEBUG
        Destroy(this.gameObject);
    }
}
