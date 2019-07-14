using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuController : MonoBehaviour
{

    // Public references
    [NotNull]
    public GameObject pauseMenuObject;

    public void ShowPauseMenu()
    {
        this.pauseMenuObject.SetActive(true);
    }

    public void HidePauseMenu()
    {
        this.pauseMenuObject.SetActive(false);
    }

    public void TogglePauseMenu()
    {
        this.pauseMenuObject.SetActive(!this.pauseMenuObject.activeSelf);
    }

    public void ExitApplication()
    {
#if UNITY_EDITOR 
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            this.TogglePauseMenu();
        }
    }


}
