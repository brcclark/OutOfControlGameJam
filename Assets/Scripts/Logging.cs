using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Logging : MonoBehaviour {
	public static Logging Log = null;

	//just settings this to true right now, can be turned off eventually
	bool debug = true;
	// Start is called before the first frame update
	void Start() {

	}
	public void SetDebug(bool value) {
		debug = value;
	}

	public void Print(object msg) {
		if (debug) {
			Debug.Log(msg);
		}
	}
}
