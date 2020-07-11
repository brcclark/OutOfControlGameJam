using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

	public Transform sheep;
	public int startingSheep = 3;
	public float spawnInterval = 10;


	Transform t;
	float spawnTimer;
	float diffucltyIncreaseTime = 5;
	float currentDifficultyTime = 0;
	int currentDifficulty = 0;
	// Start is called before the first frame update
	void Start() {
		t = GetComponent<Transform>();
		for (int i = 0; i < startingSheep; i++) {
			SpawnSheep();
		}
	}

	void SpawnSheep() {
		Transform sp = Instantiate(sheep, new Vector3(Random.Range(-15f, 15f), Random.Range(-15f, 15f), 0), Quaternion.identity) as Transform;
		sp.parent = t;
	}
	void CheckDifficulty() {
		if (currentDifficultyTime >= diffucltyIncreaseTime) {
			currentDifficulty++;
			currentDifficultyTime = 0;
		}
		else {
			currentDifficultyTime += Time.deltaTime;
		}
	}
	void CheckSheepSpawner() {
		if ((spawnTimer < spawnInterval)) {
			spawnTimer += Time.deltaTime + Time.deltaTime * currentDifficulty;
		}
		else {
			spawnTimer = 0;
			SpawnSheep();
		}
	}

	// Update is called once per frame
	void Update() {
		CheckDifficulty();
		CheckSheepSpawner();
	}
}
