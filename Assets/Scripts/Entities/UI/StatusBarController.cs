using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusBarController : MonoBehaviour
{
    // Public references
    [SerializeField]
    [NotNull]
    private Text helixCountText;
    [SerializeField]
    [NotNull]
    private Text locationText;

    // Properties
    public int HelixCount
    {
        set { this.helixCountText.text = $"{value}"; }
    }

    public string Location
    {
        set { this.locationText.text = value; }
    }

}
