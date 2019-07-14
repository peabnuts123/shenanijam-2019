using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatsBarController : MonoBehaviour
{
    [SerializeField]
    [NotNull]
    private Text BlastStrengthText;
    [SerializeField]
    [NotNull]
    private Text BlastSizeText;
    [SerializeField]
    [NotNull]
    private Text OrbStrengthText;
    [SerializeField]
    [NotNull]
    private Text OrbCountText;
    [SerializeField]
    [NotNull]
    private Text SpeedText;
    [SerializeField]
    [NotNull]
    private Text HealthText;

    // Private state
    private int maxHealth = 0;
    private int currentHealth = 0;

    private void UpdateHealthText()
    {
        this.HealthText.text = $"{this.currentHealth} / {this.maxHealth}";
    }

    public int BlastStrength
    {
        set
        {
            this.BlastStrengthText.text = $"{value}";
        }
    }
    public int BlastSize
    {
        set
        {
            this.BlastSizeText.text = $"{value}";
        }
    }
    public int OrbStrength
    {
        set
        {
            this.OrbStrengthText.text = $"{value}";
        }
    }
    public int OrbCount
    {
        set
        {
            this.OrbCountText.text = $"{value}";
        }
    }
    public int Speed
    {
        set
        {
            this.SpeedText.text = $"{value}";
        }
    }
    public int CurrentHealth
    {
        set
        {
            this.currentHealth = value;
            UpdateHealthText();
        }
    }
    public int MaxHealth
    {
        set
        {
            this.maxHealth = value;
            UpdateHealthText();
        }
    }

}
