using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverController : MonoBehaviour
{

    // Public references
    [NotNull]
    public GameObject gameOverObject;
    [SerializeField]
    [NotNull]
    private Text helixCountText;

    public void ShowGameOverScreen()
    {
        this.gameOverObject.SetActive(true);
        Time.timeScale = 0;
    }

    public void HideGameOverScreen()
    {
        this.gameOverObject.SetActive(false);
        Time.timeScale = 1;
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene("Game");
    }

    public int HelixCount
    {
        set { this.helixCountText.text = $"{value}"; }
    }
}
