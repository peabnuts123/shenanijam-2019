using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicAttack : MonoBehaviour
{
    // Public config
    public float damage = 10;
    
    // Private config
    private float damageModifier;
    private float sizeModifier;

    public void Initialise(float damageModifier, float sizeModifier)
    {
        this.damageModifier = damageModifier;
        this.sizeModifier = sizeModifier;

        this.transform.localScale *= this.sizeModifier;
    }
    
    public void DestroySelf()
    {
        Destroy(this.gameObject);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag != "Player" && !other.isTrigger)
        {
            // Damage the thing it hit
            var damageable = other.GetComponent<Damageable>();
            if (damageable != null)
            {
                damageable.Damage(this.damage * this.damageModifier);
            }
        }

    }
}
