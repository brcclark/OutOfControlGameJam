using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bark : MonoBehaviour {
	public LayerMask collisionMask;
	float speed = 35;
	float lifetime = 0.5f;
	// Start is called before the first frame update
	void Start() {
		Destroy(gameObject, lifetime);

		Collider2D initialCollision = Physics2D.OverlapCircle(transform.position, .1f, collisionMask);
		if (initialCollision != null) {
			OnHitObject(initialCollision, transform.position);
		}
	}

	void Update() {
		float distanceToMove = speed * Time.deltaTime;
		transform.Translate(Vector3.right * distanceToMove);
	}

	void OnHitObject(Collider2D collider, Vector3 hitLocation) {
		//Need to tell that sheep to run away!
		collider.gameObject.GetComponent<AnimalMovement>().HitByBark(transform.position);
		Destroy(gameObject);
	}
	void OnTriggerEnter2D(Collider2D col) {
		if (col.gameObject.layer == 9) {
			OnHitObject(col, col.transform.position);
		}
	}
}
