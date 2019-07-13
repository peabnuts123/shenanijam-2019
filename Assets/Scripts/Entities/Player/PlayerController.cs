using UnityEngine;

public class PlayerController : MonoBehaviour
{

    // Constants
    public readonly static float INPUT_DEAD_ZONE = 0.1F;
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

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        var hAxis = Input.GetAxis("Horizontal");
        var vAxis = Input.GetAxis("Vertical");


        if (vAxis < -INPUT_DEAD_ZONE)
        {
            this.spriteAnimator.SetInteger("Direction", (int)Direction.Down);
        } else if (vAxis > INPUT_DEAD_ZONE)
        {
            this.spriteAnimator.SetInteger("Direction", (int)Direction.Up);
        }

        if (hAxis < -INPUT_DEAD_ZONE)
        {
            this.spriteAnimator.SetInteger("Direction", (int)Direction.Left);
        } else if (hAxis > INPUT_DEAD_ZONE)
        {
            this.spriteAnimator.SetInteger("Direction", (int)Direction.Right);
        }

        this.rigidBody.velocity = new Vector2(hAxis * this.speed, vAxis * this.speed);

    }
}
