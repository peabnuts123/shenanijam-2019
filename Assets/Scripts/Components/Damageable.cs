using UnityEngine;

public class Damageable : MonoBehaviour
{
    // Delegates 
    public delegate void OnDeathEventHandler();

    // Event Delegates
    public event OnDeathEventHandler OnDeath;


    // Public config
    public float healthPoints = 10;

    public void Damage(float damage)
    {
        this.healthPoints -= damage;

        if (this.healthPoints <= 0 && this.OnDeath != null)
        {
            this.OnDeath();
        }
    }
}
