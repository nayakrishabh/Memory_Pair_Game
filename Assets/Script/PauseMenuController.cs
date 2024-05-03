using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenuController : MonoBehaviour
{
    public Button resumebutton;
    public Button mainmenubutton;
    public Button exitbutton;

   
    void Update() {
        resumebutton.onClick.AddListener(ResumeGame);
        mainmenubutton.onClick.AddListener(MainMenu);
        exitbutton.onClick.AddListener(ExitGame);
    }

    void ResumeGame() {
        Time.timeScale = 1.0f;
        gameObject.SetActive(false);
    }
    void MainMenu() {
        SceneManager.LoadScene(0);
    }
    void ExitGame() {
        Application.Quit();
    }
}
