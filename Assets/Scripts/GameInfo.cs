using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInfo : MonoBehaviour {
	// Start is called before the first frame update
	void Start() {
		//Seed the random number generator, ideally we want to save this? maybe?
		Random.InitState((int)Time.time);
	}

	// Update is called once per frame
	void Update() {

	}
}
