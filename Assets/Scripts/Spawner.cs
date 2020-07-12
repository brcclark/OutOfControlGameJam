using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

	public Transform sheep;
	public Transform squirrel;
	public int startingSheep = 3;
	public float sheepSpawnInterval = 10;

	public float squirrelSpawnInterval = 30;

	Transform t;
	float spawnTimer;
	float diffucltyIncreaseTime = 20;
	float currentDifficultyTime = 0;
	int currentDifficulty = 0;

	// Start is called before the first frame update
	void Start() {
		t = GetComponent<Transform>();
		for (int i = 0; i < startingSheep; i++) {
			SpawnSheep();
		}
		SpawnSquirell();

	}

	void SpawnSquirell() {
		float rotation;
		rotation = Mathf.Rad2Deg * Mathf.Atan2(Random.Range(-1f, 1f), Random.Range(0f, 1f));
		Transform sq = Instantiate(squirrel, new Vector3(Random.Range(-15f, 15f), Random.Range(-15f, 15f), 0), Quaternion.Euler(0, 0, rotation)) as Transform;
		sq.parent = t;
	}

	void SpawnSheep() {
		Transform sp = Instantiate(sheep, new Vector3(Random.Range(-15f, 15f), Random.Range(-15f, 15f), 0), Quaternion.identity) as Transform;
		sp.parent = t;
	}
	void CheckDifficulty() {
		if (currentDifficultyTime >= diffucltyIncreaseTime) {
			currentDifficulty++;
			currentDifficultyTime = 0;
			SpawnSquirell();
		}
		else {
			currentDifficultyTime += Time.deltaTime;
		}
	}
	void CheckSheepSpawner() {
		if ((spawnTimer < sheepSpawnInterval)) {
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
		if (Input.GetKeyDown(KeyCode.RightControl)) {
			SpawnSquirell();
		}
	}
}
