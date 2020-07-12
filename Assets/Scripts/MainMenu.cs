using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject optionsMenu;
    public GameObject gameOverMenu;
    public Slider musicSlider;
    public Text finalScore;
    PenManager pen;
    void Start(){
        pen = GameObject.FindWithTag("Pen").GetComponent<PenManager>();
        musicSlider.value = PlayerPrefs.GetFloat("MusicVolume", 0.75f);
    }
    public void PlayGame(){
        SceneManager.LoadScene("GameScene");
    }
    public void ExitGame(){
        Application.Quit();
    }
    public void Options(){
        mainMenu.SetActive(false);
        optionsMenu.SetActive(true);
    }
    public void Back(){
        mainMenu.SetActive(true);
        optionsMenu.SetActive(false);
    }
    public void ToMainMenu(){
        mainMenu.SetActive(true);
        gameOverMenu.SetActive(false);
    }
    //Updates the Player Preferences on the computer
    public void UpdateMusicVolume(){
        PlayerPrefs.SetFloat("MusicVolume", musicSlider.value);
    }
    //need to have the game over trigger GameOverData()
    public void GameOverData(){
        SceneManager.LoadScene("MainMenu");
        mainMenu.SetActive(false);
        optionsMenu.SetActive(false);
        gameOverMenu.SetActive(true);
        finalScore.text = pen.penScore.ToString();
    }
}

