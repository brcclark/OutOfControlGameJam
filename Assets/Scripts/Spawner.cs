using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

	public Transform sheep;
	public int numSheep = 3;

	Transform t;

	// Start is called before the first frame update
	void Start() {
		t = GetComponent<Transform>();
		for (int i = 0; i < numSheep; i++) {
			Transform sp = Instantiate(sheep, new Vector3(Random.Range(-15f, 15f), Random.Range(-15f, 15f), 0), Quaternion.identity) as Transform;
			sp.parent = t;
		}
	}

	// Update is called once per frame
	void Update() {

	}
}
