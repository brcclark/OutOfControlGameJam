using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Manager : MonoBehaviour {
	public Transform[] flowers;

	public Text gameScoreUI;

	PenManager pen;
	public float score;

	List<Transform> spawnedFlowers;

	GameState State;
	enum GameState {
		Menu,
		Playing,
		Game_Over
	}

	void Start() {
		State = GameState.Playing;
		pen = GameObject.FindWithTag("Pen").GetComponent<PenManager>();
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


