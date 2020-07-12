using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalMovement : MonoBehaviour {
	public float moveRate;
	public float moveSpeed = 5f;
	//for Sheep timer to change direction
	public float timeUntilDirChange = 3f;
	public float cantEscapeTime = 2f;
	public float scareDistance = 10f;
	public float escapeDecayTime = 10f;
	public float penWallDistanceCheck = 100f;
	public Vector2 startDirection;

	PenManager penManager;

	float barkTimer = 1f;
	float lastBarkTime = 0;
	float barkAvoidSpeed = 8;

	float cantEscapeTimer = 0f;
	float maybeEscapesTimer = 0f;
	float currentEscapeChance = 0f;
	float escapeCheckNumber = 100f;
	float lastMoveTime;
	float currentMoveTimer;
	float placidSheep = 0f;
	float originalMoveSpeed = 0f;
	Vector2 jailbreakDirection = Vector2.up;
	Vector2 sheepPosPrevious;
	Vector2 sheepCamPos;
	Vector2 playerToSheepDir;
	Vector2 currentDirection;
	Transform player;
	Transform pen;
	Camera cam;
	Rigidbody2D rb;
	public LayerMask mask;

	enum SheepState { Sheep_Wander, Sheep_Avoid, Sheep_In_Pen, Sheep_Escaping, Sheep_Barked }

	SheepState sheepState;
	bool exiting = false;
	bool inPen = false;

	// Start is called before the first frame update
	void Start() {
		//Bringing in the player position
		player = GameObject.FindGameObjectWithTag("Player").transform;
		pen = GameObject.FindGameObjectWithTag("Pen").transform;
		penManager = GameObject.FindGameObjectWithTag("Pen").GetComponent<PenManager>();
		cam = GameObject.FindObjectOfType<Camera>();
		rb = GetComponent<Rigidbody2D>();
		originalMoveSpeed = moveSpeed;
		//choose a random start direction
		startDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
		currentDirection = startDirection;
	}

	void OnTriggerEnter2D(Collider2D col) {
		if (col.gameObject.CompareTag("Pen")) {
			penManager.sheepInPenCount++;
			inPen = true;
		}
	}
	void OnTriggerExit2D(Collider2D col) {
		if (col.gameObject.CompareTag("Pen")) {
			penManager.sheepInPenCount--;
			inPen = false;
		}
	}
	void OnCollisionEnter2D(Collision2D collision) {
		if (collision.gameObject.layer == 8 && !exiting) {
			exiting = true;
			currentDirection = -currentDirection;
		}
	}

	void OnCollisionExit2D(Collision2D collision) {
		if (collision.gameObject.layer == 8 && exiting) {
			exiting = false;
		}
	}
	// Update is called once per frame
	void Update() {
		//Determine the next position

		switch (sheepState) {
			case SheepState.Sheep_Wander:
				//Check to see if the player is in our Zone of influence
				PlayerVisible();
				//If none of that has happened, we'll just check to randomly change our direction
				RandomPositionTimer();
				//Check to see if we're in the pen
				InPen();
				break;
			case SheepState.Sheep_Avoid:
				//Check to see if the player is in our Zone of influence
				PlayerVisible();
				//Check to see if we're in the pen
				InPen();
				break;
			case SheepState.Sheep_Barked:
				//Sheep has been barked at, keep moving until we're done
				moveSpeed = barkAvoidSpeed;
				if (lastBarkTime >= barkTimer) {
					//Done avoiding the bark
					moveSpeed = originalMoveSpeed;
					sheepState = SheepState.Sheep_Wander;
				}
				else {
					lastBarkTime += Time.deltaTime;
				}
				break;
			case SheepState.Sheep_In_Pen:
				//Check to see if we're in the pen
				InPen();
				//Check to see if we are escapings
				JailbreakPlanning();
				break;
			case SheepState.Sheep_Escaping:
				//Running out of the pen
				Jailbreak();
				break;
		}
		//Check to see if they are outside camera range
		OnCamEdge();
		//Check to see if they collide with something
		//Update the position
		// UpdatePosition();
	}

	void FixedUpdate() {
		UpdatePosition();
	}

	void RandomPositionTimer() {
		if ((currentMoveTimer < timeUntilDirChange) && !inPen) {
			currentMoveTimer += Time.deltaTime;
		}
		currentMoveTimer = Mathf.Clamp(currentMoveTimer, 0, timeUntilDirChange);

		if (currentMoveTimer >= timeUntilDirChange) {
			currentDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
			currentMoveTimer = 0f;
		}
	}

	void UpdatePosition() {
		rb.MovePosition(rb.position + currentDirection * moveSpeed * Time.fixedDeltaTime);
	}

	void PlayerVisible() {
		if ((Vector2.Distance(transform.position, player.position) <= (scareDistance / 2))) {
			moveSpeed += 1f;
			if (Vector2.Distance(transform.position, player.position) <= scareDistance) {
				sheepState = SheepState.Sheep_Avoid;
				playerToSheepDir = (transform.position - player.position).normalized;
				currentDirection = playerToSheepDir;
				currentMoveTimer = 0f;
			}
		}
		else {
			moveSpeed = originalMoveSpeed;
			sheepState = SheepState.Sheep_Wander;
		}
	}
	public void HitByBark(Vector3 pos) {
		//The sheep was hit by a bark
		if (sheepState == SheepState.Sheep_Wander || sheepState == SheepState.Sheep_Avoid) {
			//Only do this if we're in the wander state or avoid state (to make sure you can't bark at sheep in the pen)
			lastBarkTime = 0;
			currentDirection = (transform.position - pos).normalized;
			sheepState = SheepState.Sheep_Barked;

			//Will this cause a problem with colliding other sheep?
		}
	}
	//Add potential for the sheep to escape. After a set time, or if the player is within scare distance
	void JailbreakPlanning() {
		//check if initial time, guaranteed no escape
		if (cantEscapeTimer < cantEscapeTime) {
			cantEscapeTimer += Time.deltaTime;
		}
		cantEscapeTimer = Mathf.Clamp(cantEscapeTimer, 0, cantEscapeTime);
		//start increasing a chance for the sheep to escape
		if (cantEscapeTimer >= cantEscapeTime) {
			currentEscapeChance = Mathf.InverseLerp(0f, escapeDecayTime, maybeEscapesTimer);
			maybeEscapesTimer += Time.deltaTime;
			escapeCheckNumber = Random.Range(0, 1000) / 10.0f;
		}
		//check to see if they escape. checking a random number against being under the currentEscapeChance

		if (escapeCheckNumber <= (Mathf.Round(currentEscapeChance * 10f) / 10f)) {
			cantEscapeTimer = 0;
			escapeCheckNumber = 100;
			maybeEscapesTimer = 0;
			currentEscapeChance = 0;
			sheepState = SheepState.Sheep_Escaping;
		}

	}
	//add fox? that can herd them past those barriers
	void Jailbreak() {

		//pick a new random? direction
		if (jailbreakDirection != currentDirection) {
			jailbreakDirection = new Vector2(Random.Range(-1f, 0f), Random.Range(-1f, 1f)).normalized;
		}
		currentDirection = jailbreakDirection;
		//check to see if that will let them out of the pen

		if (!inPen) {
			sheepState = SheepState.Sheep_Wander;
		}
	}
	bool placid = false;
	Collision2D insideCollide;
	void InPen() {
		if (inPen) {
			if (sheepState != SheepState.Sheep_In_Pen) {
				placid = false;
				placidSheep = 0f;
			}
			currentMoveTimer = 0f;
			sheepState = SheepState.Sheep_In_Pen;
			placidSheep += Time.deltaTime;
			if (placidSheep >= 3f) {
				currentDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
				placid = true;
			}

			if (placid) {
			}

			if ((Vector2.Distance(transform.position, pen.position) > 0.1f) && !placid) {
				currentDirection = (pen.position - transform.position).normalized;
			}
		}

	}
	void OnCamEdge() {
		sheepCamPos = cam.WorldToViewportPoint(transform.position);
		if ((sheepCamPos.x < 0f) || (sheepCamPos.x > 1f) || (sheepCamPos.y < 0f) || (sheepCamPos.y > 1f)) {
			currentDirection = -currentDirection;
		}
	}

}
