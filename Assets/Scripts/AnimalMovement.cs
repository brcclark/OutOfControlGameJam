using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalMovement : MonoBehaviour {
	public float moveRate;
	public float moveSpeed = 5f;
	public Vector2 startDirection;

	float lastMoveTime;
	Vector2 currentDirection;

	// Start is called before the first frame update
	void Start() {
		//choose a random start direction
		startDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
		//Hi Nick
		currentDirection = startDirection;
	}

	// Update is called once per frame
	void Update() {
		transform.position += (Vector3)currentDirection * moveSpeed * Time.deltaTime;
	}
}
