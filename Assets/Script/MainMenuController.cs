using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    public Button playbutton;
    public Button exitbutton;


    void Update() {
        playbutton.onClick.AddListener(StartGame);
        exitbutton.onClick.AddListener(EndGame);
    }

    void StartGame() {
        SceneManager.LoadScene(1);
    }
    void EndGame() {
        Application.Quit();
    }
}
