using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour {
	public enum GameState {
		Game_New,
		Game_Info,
		Game_Player_Move,
		Game_Player_End_Turn,
		Game_Over
	}
	public GameState State;

	// Update is called once per frame
	void Update() {

	}

	void NewGame() {
		//Clear out the cages
		//Clear out the bunnies list

	}
	void StateMachine() {
		switch (State) {
			case GameState.Game_New:
				Logging.print("Game New State Entered");
				break;
			case GameState.Game_Info:
				Logging.print("Game Info State Entered");
				break;
			case GameState.Game_Player_Move:
				Logging.print("Game Player Move State Entered");
				break;
			case GameState.Game_Player_End_Turn:
				Logging.print("Game Player End Turn State Entered");
				break;
			case GameState.Game_Over:
				Logging.print("Game Over State Entered");
				break;
		}
	}

}


