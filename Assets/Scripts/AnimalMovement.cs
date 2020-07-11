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
	Vector2 playerToSheepDir;
	Vector2 currentDirection;
	Transform player;

	bool inPen = false;

	// Start is called before the first frame update
	void Start() {
		//Bringing in the player position
		player = GameObject.FindGameObjectWithTag("Player").transform;

		//choose a random start direction
		startDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
		currentDirection = startDirection;
	}

	void OnTriggerEnter2D(Collider2D col) {
		if (col.gameObject.CompareTag("Pen")) {
			inPen = true;
		}
	}
	void OnTriggerExit2D(Collider2D col) {
		if (col.gameObject.CompareTag("Pen")) {
			inPen = false;
		}
	}
	// Update is called once per frame
	void Update() {
		//Determine the next position
		//Check to see if the player is in our Zone of influence
		PlayerVisible();
		//Check to see if we are in the pen
		//If none of that has happened, we'll just check to randomly change our direction
		RandomPositionTimer();
		//Update the position
		UpdatePosition();
	}

	void RandomPositionTimer() {
		if (currentMoveTimer < timeUntilDirChange) {
			currentMoveTimer += Time.deltaTime;
		}
		currentMoveTimer = Mathf.Clamp(currentMoveTimer, 0, timeUntilDirChange);

		if (currentMoveTimer >= timeUntilDirChange) {
			currentDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
			currentMoveTimer = 0f;
		}
	}

	void UpdatePosition() {
		transform.position += (Vector3)currentDirection * moveSpeed * Time.deltaTime;
	}

	void PlayerVisible() {
		if(Vector2.Distance(transform.position,player.position) <= scareDistance){
			playerToSheepDir = (transform.position- player.position).normalized;
			print(Vector2.Distance(transform.position,player.position));
			currentDirection = playerToSheepDir;
			currentMoveTimer = 0f;
		}
	}
}
