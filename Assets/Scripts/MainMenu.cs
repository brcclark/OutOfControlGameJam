using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject optionsMenu;
    public Slider musicSlider;
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
    public void UpdateMusicVolume(){
        PlayerPrefs.SetFloat("MusicVolume", musicSlider.value);
    }
    void Start(){
        musicSlider.value = PlayerPrefs.GetFloat("MusicVolume", 0.75f);
    }
}

