using UnityEngine;
using System.Collections;

public class PlayerInput : MonoBehaviour {
	public string stringToEdit = "Type move here!";
	public string actionString;

	public GUIText actionLabel;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		actionLabel.text = actionString;
	}

	void OnGUI(){
		int buttonHeight = 30;
		int buttonWidth = 60;

		int numButton = 0;
		if(GUI.Button(new Rect(10 + numButton*buttonWidth, Screen.height - buttonHeight, buttonWidth, buttonHeight), "engarde")){
			actionString = "engarde";
		}
		numButton++;
		if(GUI.Button(new Rect(10 + numButton*buttonWidth, Screen.height - buttonHeight, buttonWidth, buttonHeight), "lunge")){
			actionString = "lunge";
		}
		numButton++;
		if(GUI.Button(new Rect(10 + numButton*buttonWidth, Screen.height - buttonHeight, buttonWidth, buttonHeight), "parry 6")){
			actionString = "parry 6";
		}
		numButton++;
		if(GUI.Button(new Rect(10 + numButton*buttonWidth, Screen.height - buttonHeight, buttonWidth, buttonHeight), "parry 4")){
			actionString = "parry 4";
		}


		/*stringToEdit = GUI.TextField(new Rect(10, 10, 200, 20), stringToEdit, 25);
		Event e = Event.current;
		bool userHasHitReturn = false;
		if (e.keyCode == KeyCode.Return){
			userHasHitReturn = true;
		}
		if(userHasHitReturn){
			actionString = stringToEdit;
			stringToEdit = "";
			userHasHitReturn = false;
		}*/
	}
}
