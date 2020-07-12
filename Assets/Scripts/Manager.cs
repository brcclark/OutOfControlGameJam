using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Manager : MonoBehaviour {
	public Transform flowerPrefab;
	public Transform flowerSpawn;

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

	int flowerCount = 100;

	void Start() {
		State = GameState.Playing;
		pen = GameObject.FindWithTag("Pen").GetComponent<PenManager>();
		SpawnFlowers();
	}
	void SpawnFlowers() {
		for (int i = 0; i < flowerCount; i++) {
			Transform t = Instantiate(flowerPrefab, new Vector3(Random.Range(-40, 25), Random.Range(-22, 22), 0), Quaternion.identity);
			t.GetComponent<Flower>().OnDeath += FlowerRemoved;
			t.parent = flowerSpawn;
		}
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
	void FlowerRemoved() {
		flowerCount--;
		if (flowerCount <= 0) {
			print("Game Over!");
		}
	}
	void StateMachine() {

	}

}


