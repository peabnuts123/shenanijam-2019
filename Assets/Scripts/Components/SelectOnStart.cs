using UnityEngine;
using UnityEngine.UI;

public class SelectOnStart : MonoBehaviour
{
    void Start()
    {
        var selectable = GetComponent<Selectable>();
        if (selectable == null)
        {
            Debug.LogWarning("Failed to select selectable - no selectable component found");
        }
        else
        {
            selectable.Select();
        }
    }
}
