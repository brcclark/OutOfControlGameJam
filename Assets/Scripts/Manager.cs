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

	PenManager pen;
	public float score;
	AudioSource audio;

	void Start() {
		audio.playOnAwake = true;
		State = GameState.Playing;
		pen = GameObject.FindWithTag("Pen").GetComponent<PenManager>();
		audio = GetComponent<AudioSource>();
	}
	// Update is called once per frame
	void Update() {
		UpdateScore();
	}
	void UpdateScore() {
		if (State == GameState.Playing) {
			score = pen.penScore;
			gameScoreUI.text = score.ToString();
		}
	}
	void StateMachine() {

	}

}


