using UnityEngine;

public class Damageable : MonoBehaviour
{
    // Delegates 
    public delegate void OnDeathEventHandler();
    public delegate void OnDamageEventHandler();

    // Event Delegates
    public event OnDeathEventHandler OnDeath;
    public event OnDamageEventHandler OnDamage;



    // Public config
    public float healthPoints = 10;

    public void Damage(float damage)
    {
        this.healthPoints -= damage;

        if (this.OnDamage != null)
        {
            this.OnDamage();
        }

        if (this.healthPoints <= 0 && this.OnDeath != null)
        {
            this.OnDeath();
        }
    }
}
