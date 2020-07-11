using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
	public float playerSpeed = 10f;
	public float turnSpeed = 8;
	public float dashAmount = 5;

	public System.Action OnDashUsed;
	public System.Action OnDashReady;

	public System.Action OnBarkUsed;
	public System.Action OnBarkReady;

	Vector3 dashDir;
	float dashCoolDown = 3f;
	float dashTimer;
	bool dashRecharged = true;

	enum PlayerMovementState { Player_Move, Player_Dash }
	PlayerMovementState playerMovementState;
	// Start is called before the first frame update
	void Start() {
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
				if (Input.GetButtonDown("Dash") && dashRecharged) {
					OnDashUsed();
					dashRecharged = false;
					//Preform a dash movement
					dashDir = inputDir;
					playerMovementState = PlayerMovementState.Player_Dash;
				}
				if (Input.GetButtonDown("Bark")) {
					print("Bark!");
				}
				break;
			case PlayerMovementState.Player_Dash:
				//This should be more of an animation than a "teleport", should lerp the position i think
				transform.position += dashDir * dashAmount;
				playerMovementState = PlayerMovementState.Player_Move;
				break;
		}
		RechargeAbilities();

		transform.position += inputDir * playerSpeed * Time.deltaTime;
	}

	void RechargeAbilities() {
		RechargeDash();
	}
	void RechargeDash() {
		if (!dashRecharged) {
			if (dashTimer > dashCoolDown) {
				dashTimer = 0;
				dashRecharged = true;
				OnDashReady();
			}
			else {
				dashTimer += Time.deltaTime;
			}
		}
	}
}
