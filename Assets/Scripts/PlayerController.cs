using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
	public float playerSpeed = 10f;
	public float turnSpeed = 8;
	public float dashAmount = 5;

	Transform transform;
	Vector3 dashDir;
	enum PlayerMovementState { Player_Move, Player_Dash }

	PlayerMovementState playerMovementState;
	// Start is called before the first frame update
	void Start() {
		transform = GetComponent<Transform>();
	}

	void OnTriggerEnter2D(Collider2D col) {

	}
	void OnTriggerExit2D(Collider2D col) {

	}

	// Update is called once per frame
	void Update() {
		PlayerMovement();
	}

	void PlayerMovement() {
		Vector3 inputDir = new Vector3();
		switch (playerMovementState) {
			case PlayerMovementState.Player_Move:
				inputDir = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0).normalized;
				if (Input.GetButtonDown("Dash")) {
					//Preform a dash movement
					//Need a dash cooldown
					dashDir = inputDir;
					playerMovementState = PlayerMovementState.Player_Dash;
				}
				break;
			case PlayerMovementState.Player_Dash:
				//This should be more of an animation than a "teleport", should lerp the position i think
				transform.position += dashDir * dashAmount;
				playerMovementState = PlayerMovementState.Player_Move;
				break;
		}


		transform.position += inputDir * playerSpeed * Time.deltaTime;
	}
}
