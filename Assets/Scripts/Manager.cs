using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Manager : MonoBehaviour {
	public Transform flowerPrefab;
	public Transform flowerSpawn;
	public GameOverMenu menu;

	public AudioClip[] musicThemes;
	AudioSource audio;
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
		audio = GetComponent<AudioSource>();
		SpawnFlowers();
	}

	void PlayMusic() {
		if (!audio.isPlaying) {
			//Choose a random audio clip and start playing it
			audio.clip = musicThemes[Random.Range(0, musicThemes.Length)];
			audio.volume = PlayerPrefs.GetFloat("MusicVolume");
			audio.Play();

		}
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
		StateMachine();
		UpdateScore();
		PlayMusic();
		if (Input.GetKeyDown(KeyCode.P)) {
			menu.GameOverData();
		}
	}
	void UpdateScore() {
		if (State == GameState.Playing) {
			score = pen.penScore;
			gameScoreUI.text = string.Format("{0:0.##}", score);
		}
	}
	void FlowerRemoved() {
		flowerCount--;
		if (flowerCount <= 0) {
			menu.GameOverData();
		}
	}
	void StateMachine() {
		switch (State) {
			case GameState.Menu:
				break;
			case GameState.Playing:
				break;
			case GameState.Game_Over:
				break;
		}
	}

}


