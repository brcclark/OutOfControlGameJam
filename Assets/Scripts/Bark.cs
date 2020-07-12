using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bark : MonoBehaviour {
	float speed = 5;
	float lifetime = 3;
	// Start is called before the first frame update
	void Start() {
		Destroy(gameObject, lifetime);
	}

	void Update() {
		float distanceToMove = speed * Time.deltaTime;
		transform.Translate(Vector3.right * distanceToMove);
	}
}
