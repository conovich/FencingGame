using UnityEngine;
using System.Collections;

public class MatchOverGUI : MonoBehaviour {
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnGUI(){
		float buttonHeight = 30;
		float buttonWidth = 200;
		if(GUI.Button(new Rect(Screen.width/2 - buttonWidth/2, Screen.height/2 - buttonHeight/2, buttonWidth, buttonHeight), "PLAY AGAIN")){
			Application.LoadLevel("MainWithFencers");
			GameState.resetAllInPlay = true;
		}

		if(GUI.Button(new Rect(Screen.width/2 - buttonWidth/2, Screen.height/2 - buttonHeight/2 - buttonHeight, buttonWidth, buttonHeight), "RETURN TO MAIN MENU")){
			Application.LoadLevel("MainMenu");
			GameState.resetAllInPlay = true;
		}
	}
}
