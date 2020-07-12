using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PenManager : MonoBehaviour {

	public int sheepInPenCount = 0;

	public float inPenScoreMultiplyer = 0.5f;

	public float penScore;

	void Start() {
	}

	void UpdateScore() {
		penScore += sheepInPenCount * inPenScoreMultiplyer * Time.deltaTime;

	}
	// Update is called once per frame
	void Update() {
		UpdateScore();
	}

}
