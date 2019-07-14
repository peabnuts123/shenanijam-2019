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
        Time.timeScale = 0;
    }

    public void HidePauseMenu()
    {
        this.pauseMenuObject.SetActive(false);
        Time.timeScale = 1;
    }

    public void TogglePauseMenu()
    {
        if (this.pauseMenuObject.activeSelf)
        {
            HidePauseMenu();
        }
        else
        {
            ShowPauseMenu();
        }
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
