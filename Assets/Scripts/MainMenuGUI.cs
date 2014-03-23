using UnityEngine;
using System.Collections;

public class MainMenuGUI : MonoBehaviour {

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
	}
}
