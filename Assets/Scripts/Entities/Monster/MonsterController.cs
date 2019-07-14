using UnityEngine;
using Zenject;

public class MonsterController : MonoBehaviour
{
    // 1. Consider approaching player
    // 2. Decide to do it
    // 3. But, for how long?
    // 4. Execute well established plan

    // Public config
    // public float speed = 3F;
    // public float damage = 2;
    public float damageModifier = 1;
    public float aggroRange = 8;
    public float attackRange = 4;
    public float aggressiveness = 0.7F;
    public float responsiveness = 10;
    [NotNull]
    public AudioClip monsterHitAudio;
    [NotNull]
    public AudioClip monsterDieAudio;
    public Color peakThreatColor;


    // Public references
    [NotNull]
    public Rigidbody2D rigidBody;
    [NotNull(IgnorePrefab = true)]
    public Transform target;
    [NotNull]
    public Animator spriteAnimator;
    [NotNull]
    public SpriteRenderer spriteRenderer;

    // Private references
    [Inject]
    private Damageable damageable;
    private Damageable targetDamageable;
    [Inject]
    private AudioPlayer audioPlayer;
    [Inject]
    private MonsterStats stats;

    // Private State
    private float actionTimer = 0;
    private float attackTimer = 0;
    private bool isAwake = true;
    private bool persuingTarget = false;

    void Start()
    {
        this.damageable.OnDeath += this.OnDeath;
        this.damageable.OnDamage += this.OnDamage;
        this.targetDamageable = this.target.GetComponent<Damageable>();

        if (this.targetDamageable != null)
        {
            this.targetDamageable.OnDeath += this.OnTargetDeath;
        }


    }

    public void UpdateDifficultyColor()
    {
        // Lerp difficulty up to full saturation at, say, level 10
        //  From there. Lerp slowly to black 
        if (this.stats.difficulty < 10)
        {
            this.spriteRenderer.color = Color.Lerp(this.spriteRenderer.color, this.peakThreatColor, this.stats.difficulty / 10F);
        }
        else
        {
            this.spriteRenderer.color = Color.Lerp(this.peakThreatColor, Color.black, (this.stats.difficulty - 10) / 40F);
        }
        // Color baseColor = this.spriteRenderer.color;
        // float h_base, s_base, v_base;
        // Color.RGBToHSV(baseColor, out h_base, out s_base, out v_base);

        // // float h_peak = h_base;
        // // float s_peak = 1;
        // // float v_peak = 1;
        // // Color peakColor = Color.HSVToRGB(h_peak, s_peak, v_peak);

        // this.spriteRenderer.color = peakColor;
    }

    void Update()
    {
        if (Time.timeScale == 0)
        {
            return;
        }

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
                this.rigidBody.velocity = targetDelta.normalized * this.stats.Speed;

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
                            this.spriteAnimator.SetTrigger("Attack");
                            this.targetDamageable.Damage(this.stats.Damage * this.damageModifier);
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

    void OnDamage()
    {
        this.spriteAnimator.SetTrigger("Damage");
        this.audioPlayer.PlayClipRandom(this.monsterHitAudio);
    }

    void OnDeath()
    {
        Destroy(this.gameObject);
        this.audioPlayer.PlayClipRandom(this.monsterDieAudio);
    }

    void OnTargetDeath()
    {
        this.isAwake = false;
    }
}
