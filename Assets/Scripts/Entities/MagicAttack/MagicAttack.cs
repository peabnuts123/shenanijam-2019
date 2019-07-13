using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicAttack : MonoBehaviour
{
    // Public config
    public float damage = 10;
    
    // Private config
    private float damageModifier;

    public void Initialise(float damageModifier)
    {
        this.damageModifier = damageModifier;
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
