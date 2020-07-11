using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalMovement : MonoBehaviour {
	public float moveRate;
	public float moveSpeed = 5f;
	//for Sheep timer to change direction
	public float timeUntilDirChange = 3f;
	public float scareDistance = 10f;
	public Vector2 startDirection;

	float lastMoveTime;
	float currentMoveTimer;
	Vector2 currentDirection;
	Transform player;

	// Start is called before the first frame update
	void Start() {
		//Bringing in the player position
		player = GameObject.FindGameObjectWithTag("Player").transform;

		//choose a random start direction
		startDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
		currentDirection = startDirection;
	}

	// Update is called once per frame
	void Update() {
		transform.position += (Vector3)currentDirection * moveSpeed * Time.deltaTime;

		if (currentMoveTimer < timeUntilDirChange) {
			currentMoveTimer += Time.deltaTime;
		}
		currentMoveTimer = Mathf.Clamp(currentMoveTimer, 0, timeUntilDirChange);

		if (currentMoveTimer >= timeUntilDirChange) {
			currentDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
			currentMoveTimer = 0f;
		}
	}
}
