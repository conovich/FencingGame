using UnityEngine;
using System.Collections;

public class MainMenuGUI : MonoBehaviour {
	bool isSingleplayer = true;
	public Font _MyFont; 

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI(){
		GUI.skin.font = _MyFont;

		float buttonHeight = 30;
		float buttonWidth = 120;
		if(GUI.Button(new Rect(Screen.width/2 - buttonWidth/2, Screen.height/2 - buttonHeight/2, buttonWidth, buttonHeight), "START MATCH")){
			Application.LoadLevel("MainWithFencers");
			GameState.Instance.SetState(GameState.State.inCountdown);
			GameState.resetAllInPlay = true;
		}
		//make multiplayer vs single player toggle here, to set gamestate's playerselection enum 
		if(GUI.Toggle(new Rect(10, 40, 120, 20), isSingleplayer, "Single Player")){
			isSingleplayer = true;
			GameState.playerSelection = GameState.PlayerSelection.singlePlayer;

		}
		if(GUI.Toggle ( new Rect(10, 60, 120, 20), !isSingleplayer, "Multiplayer")){
			isSingleplayer = false;
			GameState.playerSelection = GameState.PlayerSelection.multiplayer;
			//GameState.Instance._MyControllerInput.GetControllers();
		}
	}
}
