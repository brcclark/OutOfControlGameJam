using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameOverMenu : MonoBehaviour {
	public GameObject mainMenu;
	public GameObject gameOverMenu;
	public TextMeshProUGUI finalScore;

	PenManager pen;
	void Start() {
	}
	public void PlayGame() {
		gameOverMenu.SetActive(false);
		SceneManager.LoadScene("GameScene");
	}

	public void ToMainMenu() {
		SceneManager.LoadScene("Main Menu");
	}
	//need to have the game over trigger GameOverData()
	public void GameOverData() {
		pen = GameObject.FindWithTag("Pen").GetComponent<PenManager>();
		gameOverMenu.SetActive(true);
		finalScore.text = pen.penScore.ToString();
	}
}

