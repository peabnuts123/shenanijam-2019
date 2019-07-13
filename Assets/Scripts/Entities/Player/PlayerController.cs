using UnityEngine;

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

    // Public config
    public float speed = 5F;

    // Private state
    private bool isAutopilot = false;
    private Vector2 autopilotDirection;

    // Update is called once per frame
    void Update()
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
}
