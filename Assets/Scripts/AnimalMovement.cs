using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalMovement : MonoBehaviour {
	public float moveRate;
	public float moveSpeed = 5f;
	//for Sheep timer to change direction
	public float timeUntilDirChange = 3f;
	public float cantEscapeTime = 10f;
	public float scareDistance = 10f;
	public float escapeDecayTime = 30f;
	public Vector2 startDirection;

	float cantEscapeTimer = 0f;
	float maybeEscapesTimer = 0f;
	float currentEscapeChance = 0f;
	float escapeCheckNumber = 100f;
	float lastMoveTime;
	float currentMoveTimer;
	float updatePreviousPosition;
	Vector2 sheepPosPrevious;
	Vector2 sheepCamPos;
	Vector2 playerToSheepDir;
	Vector2 currentDirection;
	Transform player;
	Transform pen;
	Camera cam;

	enum SheepState { Sheep_Wander, Sheep_Avoid, Sheep_In_Pen }

	SheepState sheepState;

	bool inPen = false;

	// Start is called before the first frame update
	void Start() {
		//Bringing in the player position
		player = GameObject.FindGameObjectWithTag("Player").transform;
		pen = GameObject.FindGameObjectWithTag("Pen").transform;
		cam = GameObject.FindObjectOfType<Camera>();

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
			case SheepState.Sheep_In_Pen:
				//Check to see if we are escaping
				Jailbreak();
				//Check to see if we're in the pen
				InPen();
				break;
		}
		//Check to see if they are outside camera range
		OnCamEdge();
		//Update the position
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
		transform.position += (Vector3)currentDirection * moveSpeed * Time.deltaTime;
	}

	void PlayerVisible() {
		if(Vector2.Distance(transform.position, player.position) <= scareDistance) {
			sheepState = SheepState.Sheep_Avoid;
			playerToSheepDir = (transform.position - player.position).normalized;
			currentDirection = playerToSheepDir;
			currentMoveTimer = 0f;
		}
		else {
			sheepState = SheepState.Sheep_Wander;
		}
	}
	//Add potential for the sheep to escape. After a set time, or if the player is within scare distance
	bool stopPlease = false;
	void Jailbreak(){
		//check if initial time, guaranteed no escape
		if(cantEscapeTimer < cantEscapeTime) {
			cantEscapeTimer += Time.deltaTime;
		}
		cantEscapeTimer = Mathf.Clamp(cantEscapeTimer, 0, cantEscapeTime);
		//start increasing a chance for the sheep to escape
		if(cantEscapeTimer >= cantEscapeTime){
			currentEscapeChance = Mathf.InverseLerp(0f,escapeDecayTime,maybeEscapesTimer);
			maybeEscapesTimer += Time.deltaTime;
			escapeCheckNumber = Random.Range(0,1000) / 10.0f;
		}
		//check to see if they escape. checking a random number against being under the currentEscapeChance
		if(stopPlease == false){
			if(escapeCheckNumber <= (Mathf.Round(maybeEscapesTimer * 10f)/ 10f)){
			print("Let's get out of here!");
			print("Escape Check Number: " + escapeCheckNumber);
			print("Maybe Escapes Timer Number: " + (Mathf.Round(maybeEscapesTimer * 10f)/ 10f));
			stopPlease = true;
			} else{
			print("Escape Check Number: " + escapeCheckNumber);
			print("Maybe Escapes Timer Number: " + (Mathf.Round(maybeEscapesTimer * 10f)/ 10f));
			}
		}
		//if they escape, change direction to out of pen
	}
//add fox? that can herd them past those barriers
	void InPen() {
		if (inPen) {
			currentMoveTimer = 0f;
			sheepState = SheepState.Sheep_In_Pen;
			updatePreviousPosition += Time.deltaTime;
			if (updatePreviousPosition >= 0.5f) {
				sheepPosPrevious = transform.position;
				updatePreviousPosition = 0f;
			}

			if(Vector2.Distance(transform.position,pen.position) > 0.1f) {
				currentDirection = (pen.position - transform.position).normalized;
				if ((updatePreviousPosition >= 0.1f) && Mathf.Abs(Vector2.Distance(transform.position, sheepPosPrevious)) < 0.3f) {
					currentDirection = Vector2.zero;
				}

			}
		}

	}
	void OnCamEdge() {
		sheepCamPos = cam.WorldToViewportPoint(transform.position);
		if((sheepCamPos.x < 0f) || (sheepCamPos.x > 1f) || (sheepCamPos.y < 0f) || (sheepCamPos.y > 1f)){
			currentDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
		}
	}
}
