using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CompareScreenController : MonoBehaviour
{
    private static readonly Color COLOR_IMPROVED = new Color(0.63F, 0.93F, 1);
    private static readonly Color COLOR_DOWNGRADED = new Color(1, 0.56F, 0.75F);

    // Public references
    [SerializeField]
    [NotNull]
    private Text blastPowerCurrentText;
    [SerializeField]
    [NotNull]
    private Text blastPowerMultiplierText;
    [SerializeField]
    [NotNull]
    private Text blastPowerResultText;

    [SerializeField]
    [NotNull]
    private Text blastSizeCurrentText;
    [SerializeField]
    [NotNull]
    private Text blastSizeMultiplierText;
    [SerializeField]
    [NotNull]
    private Text blastSizeResultText;

    [SerializeField]
    [NotNull]
    private Text orbPowerCurrentText;
    [SerializeField]
    [NotNull]
    private Text orbPowerMultiplierText;
    [SerializeField]
    [NotNull]
    private Text orbPowerResultText;

    [SerializeField]
    [NotNull]
    private Text orbCountCurrentText;
    [SerializeField]
    [NotNull]
    private Text orbCountMultiplierText;
    [SerializeField]
    [NotNull]
    private Text orbCountResultText;

    [SerializeField]
    [NotNull]
    private Text speedCurrentText;
    [SerializeField]
    [NotNull]
    private Text speedMultiplierText;
    [SerializeField]
    [NotNull]
    private Text speedResultText;

    [SerializeField]
    [NotNull]
    private Text healthCurrentText;
    [SerializeField]
    [NotNull]
    private Text healthMultiplierText;
    [SerializeField]
    [NotNull]
    private Text healthResultText;

    // Private state
    private int _blastPowerCurrent;
    private float _blastPowerMultiplier;
    private int _blastPowerResult;
    private int _blastSizeCurrent;
    private float _blastSizeMultiplier;
    private int _blastSizeResult;
    private int _orbPowerCurrent;
    private float _orbPowerMultiplier;
    private int _orbPowerResult;
    private int _orbCountCurrent;
    private float _orbCountMultiplier;
    private int _orbCountResult;
    private int _speedCurrent;
    private float _speedMultiplier;
    private int _speedResult;
    private int _healthCurrent;
    private float _healthMultiplier;
    private int _healthResult;

    private void ApplyColorModifierToText(float oldValue, float newValue, Text text)
    {
        if (newValue < oldValue)
        {
            text.color = COLOR_DOWNGRADED;
        }
        else if (newValue > oldValue)
        {
            text.color = COLOR_IMPROVED;
        }
        else
        {
            text.color = Color.white;
        }
    }

    // Blast Power
    public int BlastPowerCurrent
    {
        set
        {
            this.blastPowerCurrentText.text = $"{value}";
            this._blastPowerCurrent = value;
        }
    }
    public float BlastPowerMultiplier
    {
        set
        {
            this.blastPowerMultiplierText.text = $"{value.ToString("0.0x")}";
            this._blastSizeMultiplier = value;

            ApplyColorModifierToText(1, value, this.blastPowerMultiplierText);
        }
    }
    public int BlastPowerResult
    {
        set
        {
            this.blastPowerResultText.text = $"{value}";
            this._blastPowerResult = value;

            ApplyColorModifierToText(this._blastPowerCurrent, value, this.blastPowerResultText);
        }
    }

    // Blast Size
    public int BlastSizeCurrent
    {
        set
        {
            this.blastSizeCurrentText.text = $"{value}";
            this._blastSizeCurrent = value;
        }
    }
    public float BlastSizeMultiplier
    {
        set
        {
            this.blastSizeMultiplierText.text = $"{value.ToString("0.0x")}";
            this._blastSizeMultiplier = value;

            ApplyColorModifierToText(1, value, this.blastSizeMultiplierText);
        }
    }
    public int BlastSizeResult
    {
        set
        {
            this.blastSizeResultText.text = $"{value}";
            this._blastSizeResult = value;

            ApplyColorModifierToText(this._blastSizeCurrent, value, this.blastSizeResultText);

        }
    }

    // Orb Power
    public int OrbPowerCurrent
    {
        set
        {
            this.orbPowerCurrentText.text = $"{value}";
            this._orbPowerCurrent = value;
        }
    }
    public float OrbPowerMultiplier
    {
        set
        {
            this.orbPowerMultiplierText.text = $"{value.ToString("0.0x")}";
            this._orbPowerMultiplier = value;

            ApplyColorModifierToText(1, value, this.orbPowerMultiplierText);
        }
    }
    public int OrbPowerResult
    {
        set
        {
            this.orbPowerResultText.text = $"{value}";
            this._orbPowerResult = value;

            ApplyColorModifierToText(this._orbPowerCurrent, value, this.orbPowerResultText);
        }
    }

    // Orb Count
    public int OrbCountCurrent
    {
        set
        {
            this.orbCountCurrentText.text = $"{value}";
            this._orbCountCurrent = value;
        }
    }
    public float OrbCountMultiplier
    {
        set
        {
            this.orbCountMultiplierText.text = $"{value.ToString("0.0x")}";
            this._orbCountMultiplier = value;

            ApplyColorModifierToText(1, value, this.orbCountMultiplierText);
        }
    }
    public int OrbCountResult
    {
        set
        {
            this.orbCountResultText.text = $"{value}";
            this._orbCountResult = value;

            ApplyColorModifierToText(this._orbCountCurrent, value, this.orbCountResultText);
        }
    }

    // Speed
    public int SpeedCurrent
    {
        set
        {
            this.speedCurrentText.text = $"{value}";
            this._speedCurrent = value;
        }
    }
    public float SpeedMultiplier
    {
        set
        {
            this.speedMultiplierText.text = $"{value.ToString("0.0x")}";
            this._speedMultiplier = value;

            ApplyColorModifierToText(1, value, this.speedMultiplierText);
        }
    }
    public int SpeedResult
    {
        set
        {
            this.speedResultText.text = $"{value}";
            this._speedResult = value;

            ApplyColorModifierToText(this._speedCurrent, value, this.speedResultText);
        }
    }

    // Health
    public int HealthCurrent
    {
        set
        {
            this.healthCurrentText.text = $"{value}";
            this._healthCurrent = value;
        }
    }
    public float HealthMultiplier
    {
        set
        {
            this.healthMultiplierText.text = $"{value.ToString("0.0x")}";
            this._healthMultiplier = value;

            ApplyColorModifierToText(1, value, this.healthMultiplierText);
        }
    }
    public int HealthResult
    {
        set
        {
            this.healthResultText.text = $"{value}";
            this._healthResult = value;

            ApplyColorModifierToText(this._healthCurrent, value, this.healthResultText);
        }
    }
}
