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
    public float speed = 5F;
    public float damageModifier = 1F;

    // Private references
    [Inject]
    private Damageable damageable;

    // Private state
    private bool isAutopilot = false;
    private Vector2 autopilotDirection;

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
        if (!this.isAutopilot)
        {
            this.rigidBody.velocity = new Vector2(Input.GetAxis("Horizontal") * this.speed, Input.GetAxis("Vertical") * this.speed);
        }
        else
        {
            // Player is on autopilot
            this.rigidBody.velocity = this.autopilotDirection * this.speed;
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
        if (Input.GetButtonDown("Action1"))
        {
            /* MagicAttack attack =  */
            Instantiate(MagicAttackPrefab, this.transform.position, Quaternion.AngleAxis(Random.Range(0, 360), Vector3.forward));
        }

        if (Input.GetButtonDown("Action2"))
        {
            MagicProjectile projectile = Instantiate(MagicProjectilePrefab, this.transform.position, Quaternion.AngleAxis(Random.Range(0, 360), Vector3.forward));
            Direction projectileDirection = (Direction)this.spriteAnimator.GetInteger("Direction");
            float projectileSpeed = 15F;

            if (projectileDirection == Direction.Right)
            {
                projectile.Initialise(Vector2.right * projectileSpeed, this.damageModifier);
            }
            else if (projectileDirection == Direction.Down)
            {
                projectile.Initialise(Vector2.down * projectileSpeed, this.damageModifier);
            }
            else if (projectileDirection == Direction.Left)
            {
                projectile.Initialise(Vector2.left * projectileSpeed, this.damageModifier);
            }
            else if (projectileDirection == Direction.Up)
            {
                projectile.Initialise(Vector2.up * projectileSpeed, this.damageModifier);
            }
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
