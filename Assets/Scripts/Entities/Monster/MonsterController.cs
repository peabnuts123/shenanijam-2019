using UnityEngine;
using Zenject;

public class MonsterController : MonoBehaviour
{
    // 1. Consider approaching player
    // 2. Decide to do it
    // 3. But, for how long?
    // 4. Execute well established plan

    // Public config
    public float speed = 3F;
    public float damage = 2;
    public float damageModifier = 1;
    public float aggroRange = 8;
    public float attackRange = 4;
    public float aggressiveness = 0.7F;
    public float responsiveness = 10;

    // Public references
    [NotNull]
    public Rigidbody2D rigidBody;
    [NotNull]
    public Transform target;
    [NotNull]
    public Animator spriteAnimator;

    // Private references
    [Inject]
    private Damageable damageable;
    private Damageable targetDamageable;

    // Private state
    private float actionTimer = 0;
    private float attackTimer = 0;
    private bool isAwake = true;
    private bool persuingTarget = false;

    void Start()
    {
        this.damageable.OnDeath += this.OnDeath;
        this.targetDamageable = this.target.GetComponent<Damageable>();

        if (this.targetDamageable != null)
        {
            this.targetDamageable.OnDeath += this.OnTargetDeath;
        }
    }

    void Update()
    {
        if (isAwake)
        {
            Vector2 targetDelta = this.target.position - this.transform.position;

            // Determine actions (persue / idle)
            if (actionTimer <= 0)
            {
                this.persuingTarget = false;

                bool targetIsInAggroRange = (targetDelta.sqrMagnitude < (this.aggroRange * this.aggroRange));

                if ((Random.Range(0F, 1F) < this.aggressiveness) && targetIsInAggroRange)
                {
                    // Aggro target
                    this.persuingTarget = true;
                }

                // Reset action timer
                this.actionTimer = Random.Range(5F / this.responsiveness, 10F / this.responsiveness);
            }
            else
            {
                // Count down action timer
                actionTimer -= Time.deltaTime;
            }


            // Attack logic - only valid if the enemy is persuing the target
            if (this.persuingTarget)
            {
                // Move towards the target
                this.rigidBody.velocity = targetDelta.normalized * this.speed;

                // Only bother with this attack stuff if the target can be attacked
                if (this.targetDamageable != null)
                {
                    // Possibly attack
                    if (this.attackTimer <= 0)
                    {
                        bool targetIsInAttackRange = (targetDelta.sqrMagnitude < (this.attackRange * this.attackRange));

                        if ((Random.Range(0F, 1F) < Mathf.Sqrt(this.aggressiveness)) && targetIsInAttackRange)
                        {
                            // Attack target
                            Debug.Log("YEARGHHH!!!");
                            this.spriteAnimator.SetTrigger("Attack");
                            this.targetDamageable.Damage(this.damage * this.damageModifier);
                        }

                        // Reset action timer
                        this.attackTimer = Random.Range(5F / this.responsiveness, 10F / this.responsiveness);
                    }
                    else
                    {
                        // Count down attack timer
                        this.attackTimer -= Time.deltaTime;
                    }
                }

            }
        }
        else
        {
            this.rigidBody.velocity = Vector2.zero;
        }
    }

    void OnDeath()
    {
        // @TODO play an animation
        Destroy(this.gameObject);
    }

    void OnTargetDeath()
    {
        this.isAwake = false;
    }
}
