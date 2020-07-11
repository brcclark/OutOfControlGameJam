using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
	public float playerSpeed = 10f;
	public float turnSpeed = 8;

	Transform transform;

	// Start is called before the first frame update
	void Start() {
		transform = GetComponent<Transform>();
	}

	void OnTriggerEnter2D(Collider2D col) {
		print(col.gameObject.CompareTag("Pen"));
	}
	void OnTriggerExit2D(Collider2D col) {
		print("exit" + col.gameObject.CompareTag("Pen"));
	}
	// Update is called once per frame
	void Update() {
		Vector3 inputDir = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0).normalized;

		transform.position += inputDir * playerSpeed * Time.deltaTime;
	}
}
