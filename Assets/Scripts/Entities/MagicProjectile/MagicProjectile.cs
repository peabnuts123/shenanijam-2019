using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class MagicProjectile : MonoBehaviour
{
    // Constants
    public static readonly float MAX_LIFETIME_SECONDS = 20;

    // Public references
    [NotNull]
    public Rigidbody2D rigidBody;
    [NotNull]
    public GameObject projectileSpriteObject;
    [NotNull]
    public GameObject fizzleSpriteObject;
    [NotNull]
    public AudioClip orbFizzleAudio;

    // Private references
    [Inject]
    private AudioPlayer audioPlayer;

    // Public config
    public int damage = 5;

    // Private config
    private float damageModifier = 1;

    // Private state
    private float timeAliveSeconds = 0F;

    public void Initialise(Vector2 velocity, float damageModifier)
    {
        this.rigidBody.velocity = velocity;
        this.damageModifier = damageModifier;
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        // Ensure no projectiles last too long
        this.timeAliveSeconds += Time.deltaTime;
        if (this.timeAliveSeconds >= MAX_LIFETIME_SECONDS)
        {
            Destroy(this.gameObject);
            this.gameObject.SetActive(false);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag != "Player" && other.tag != "Projectile" && !other.isTrigger)
        {
            // Play the fizzle animation
            this.projectileSpriteObject.SetActive(false);
            this.fizzleSpriteObject.SetActive(true);
            this.rigidBody.velocity = Vector2.zero;

            // Play fizzle sound
            this.audioPlayer.PlayClipRandom(this.orbFizzleAudio, 0.15F);

            // Damage the thing it hit
            var damageable = other.GetComponent<Damageable>();
            if (damageable != null)
            {
                damageable.Damage(this.damage * this.damageModifier);
            }
        }

    }
}
