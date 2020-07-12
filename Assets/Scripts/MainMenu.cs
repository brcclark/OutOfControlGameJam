using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject optionsMenu;
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
}

