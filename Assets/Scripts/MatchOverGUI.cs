using UnityEngine;
using System.Collections;

public class MatchOverGUI : MonoBehaviour {
	string winText;

	// Use this for initialization
	void Start () {
		if(GameState.P1Score == 5 && GameState.P2Score < 5){
			winText = "Player 1 wins!";
		}
		else if(GameState.P1Score < 5 && GameState.P2Score == 5){
			winText = "Player 2 wins!";
		}
		else{
			winText = "We have a tie!";
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnGUI(){
		GUI.color = Color.white;
		GUIStyle myStyle = new GUIStyle();
		myStyle.alignment = TextAnchor.MiddleCenter;

		GUI.Label(new Rect(0, Screen.height/4.0f, Screen.width, 80), winText, myStyle);

		float buttonHeight = 80;
		float buttonWidth = 350;
		if(GUI.Button(new Rect(Screen.width/2 - buttonWidth/2, Screen.height/2 - buttonHeight/2, buttonWidth, buttonHeight), "PLAY AGAIN")){
			Application.LoadLevel("MainWithFencers");
			GameState.resetAllInPlay = true;
		}

		if(GUI.Button(new Rect(Screen.width/2 - buttonWidth/2, Screen.height*0.65f, buttonWidth, buttonHeight), "RETURN TO MAIN MENU")){
			Application.LoadLevel("MainMenu");
			GameState.resetAllInPlay = true;
		}
	}
}
