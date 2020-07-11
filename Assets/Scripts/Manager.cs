using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Manager : MonoBehaviour {
	public enum GameState {
		Menu,
		Playing,
		Game_Over
	}

	public Text gameScoreUI;
	public GameState State;

	public float score;

	void Start() {
		State = GameState.Playing;
	}
	// Update is called once per frame
	void Update() {
		UpdateScore();
	}
	void UpdateScore() {
		if (State == GameState.Playing) {
			score += Time.deltaTime;
			gameScoreUI.text = score.ToString();
		}
	}
	void StateMachine() {
	}

}


