using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Squirell : MonoBehaviour {
	public PlayerController player;

	float speed = 15f;
	Renderer r;
	bool inScene = false;
	// Start is called before the first frame update
	void Start() {
		if (player == null)
			player = FindObjectOfType<PlayerController>();
		r = GetComponentInChildren<Renderer>();
		Spawned();
	}
	void Spawned() {
		player.SquirellSpawned(gameObject);
	}
	void MoveSquirell() {
		float distanceToMove = speed * Time.deltaTime;
		transform.Translate(Vector3.left * distanceToMove);
	}
	// Update is called once per frame
	void Update() {
		MoveSquirell();
		if (inScene)
			CheckOffScreen();
		else {
			if (r.isVisible) {
				inScene = true;
			}
		}

	}
	void CheckOffScreen() {
		//Check to see if we're off the screen, if so, destroythe object and tell the player we're gone
		if (!r.isVisible) {
			player.SquirellLeft();
			Destroy(gameObject);
		}
	}
}
