using UnityEngine;
using System.Collections;

public class MainMenuGUI : MonoBehaviour {
	bool isSingleplayer = true;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI(){
		float buttonHeight = 30;
		float buttonWidth = 120;
		if(GUI.Button(new Rect(Screen.width/2 - buttonWidth/2, Screen.height/2 - buttonHeight/2, buttonWidth, buttonHeight), "START MATCH")){
			Application.LoadLevel("MainWithFencers");
		}
		//make multiplayer vs single player toggle here, to set gamestate's playerselection enum 
		if(GUI.Toggle(new Rect(10, 40, 120, 20), isSingleplayer, "Single Player")){
			isSingleplayer = true;
		}
		if(GUI.Toggle ( new Rect(10, 60, 120, 20), !isSingleplayer, "Multiplayer")){
			isSingleplayer = false;
		}
	}
}
