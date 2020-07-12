using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
	public float playerSpeed = 10f;
	public float turnSpeed = 8;
	public float dashAmount = 5;

	public System.Action OnDashUsed;
	public System.Action OnDashReady;

	public Bark bark;

	public System.Action OnBarkUsed;
	public System.Action OnBarkReady;

	AudioSource barkAudio;

	Vector3 dashDir;
	float dashCoolDown = 3f;
	float dashTimer;
	bool dashRecharged = true;

	Rigidbody2D rb;
	TrailRenderer tr;

	enum PlayerMovementState { Player_Move, Player_Dash }
	PlayerMovementState playerMovementState;
	// Start is called before the first frame update
	void Start() {
		barkAudio = GetComponent<AudioSource>();
		rb = GetComponent<Rigidbody2D>();
		tr = GetComponent<TrailRenderer>();
	}

	void OnTriggerEnter2D(Collider2D col) {

	}
	void OnTriggerExit2D(Collider2D col) {

	}

	// Update is called once per frame
	void Update() {
		PlayerInput();

	}

	void PlayerInput() {
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
					for (int i = 0; i < 3; i++) {
						Quaternion rot = Quaternion.Euler(new Vector3(0, 0, Mathf.Atan2(inputDir.y, inputDir.x) * 180 / Mathf.PI + 20 * i - 1));
						Bark b = Instantiate(bark, transform.position, rot);
					}
					barkAudio.Play();
				}
				break;
			case PlayerMovementState.Player_Dash:
				//This should be more of an animation than a "teleport", should lerp the position i think
				StartCoroutine(Dash());
				playerMovementState = PlayerMovementState.Player_Move;
				break;
		}
		RechargeAbilities();
		transform.position += inputDir * playerSpeed * Time.deltaTime;
	}
	void MovePlayer(Vector3 inputd) {

	}

	IEnumerator Dash() {
		tr.enabled = true;
		Vector2 originalPosition = transform.position;
		Vector2 dashPosition = dashDir * dashAmount;

		float attackSpeed = 6f;
		float percent = 0;

		while (percent <= 1) {
			percent += Time.deltaTime * attackSpeed;
			//float interpolation = 4 * (-Mathf.Pow(percent, 2) + percent);
			//float interpolation = Mathf.Sqrt(percent);
			transform.position = Vector3.Lerp(originalPosition, dashPosition + originalPosition, percent);

			yield return null;
		}
		tr.enabled = false;
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
