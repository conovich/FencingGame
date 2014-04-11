using UnityEngine;
using System.Collections;

public class PlayerInput : MonoBehaviour {
	public string stringToEdit = "Type move here!";
	public string actionString;

	public GUIText actionLabel;
	public GUIText swordStateLabel;
	public GUIText distanceLabel;
		
	public bool swordIsOutside;
	public int distance;

	// Use this for initialization
	void Start () {
		swordIsOutside = true;
	}
	
	// Update is called once per frame
	void Update () {
		actionLabel.text = actionString;

		if(swordIsOutside){
			swordStateLabel.text = "Sword Outside";
		}
		else{
			swordStateLabel.text = "Sword Inside";
		}

	}

	void OnGUI(){
		int buttonHeight = 30;
		int buttonWidth = 60;

		int row = 1;
		int numButton = 0;
		if(GUI.Button(new Rect(10 + numButton*buttonWidth, Screen.height - buttonHeight*row, buttonWidth, buttonHeight), "engarde")){
			actionString = "engarde";
		}
		numButton++;
		if(GUI.Button(new Rect(10 + numButton*buttonWidth, Screen.height - buttonHeight*row, buttonWidth, buttonHeight), "lungeRec")){
			actionString = "lunge recover";
		}
		numButton++;
		if(GUI.Button(new Rect(10 + numButton*buttonWidth, Screen.height - buttonHeight*row, buttonWidth, buttonHeight), "parry 6")){
			actionString = "parry six";
			swordIsOutside = false;
		}
		numButton++;
		if(GUI.Button(new Rect(10 + numButton*buttonWidth, Screen.height - buttonHeight*row, buttonWidth, buttonHeight), "parry 4")){
			actionString = "parry four";
			swordIsOutside = true;
		}
		numButton++;
		if(GUI.Button(new Rect(10 + numButton*buttonWidth, Screen.height - buttonHeight*row, buttonWidth, buttonHeight), "dis. cw")){
			actionString = "disengage in";
			if(swordIsOutside){
				swordIsOutside = false;
			}
		}
		numButton++;
		if(GUI.Button(new Rect(10 + numButton*buttonWidth, Screen.height - buttonHeight*row, buttonWidth, buttonHeight), "dis. ccw")){
			actionString = "disengage out";
			if(!swordIsOutside){
				swordIsOutside = true;
			}
		}

		row++;
		numButton = 0;
		if(GUI.Button(new Rect(10 + numButton*buttonWidth, Screen.height - buttonHeight*row, buttonWidth, buttonHeight), "circle 4")){
			actionString = "circle four";
			swordIsOutside = true;
		}
		numButton++;
		if(GUI.Button(new Rect(10 + numButton*buttonWidth, Screen.height - buttonHeight*row, buttonWidth, buttonHeight), "circle 4")){
			actionString = "circle six";
			swordIsOutside = false;
		}
		numButton++;
		if(GUI.Button(new Rect(10 + numButton*buttonWidth, Screen.height - buttonHeight*row, buttonWidth, buttonHeight), "advance")){
			actionString = "advance";
			distance--;
		}
		numButton++;
		if(GUI.Button(new Rect(10 + numButton*buttonWidth, Screen.height - buttonHeight*row, buttonWidth, buttonHeight), "retreat")){
			actionString = "retreat";
			distance++;
		}
		numButton++;
		if(GUI.Button(new Rect(10 + numButton*buttonWidth, Screen.height - buttonHeight*row, buttonWidth, buttonHeight), "advance")){
			actionString = "double advance";
			distance-=2;
		}
		numButton++;
		if(GUI.Button(new Rect(10 + numButton*buttonWidth, Screen.height - buttonHeight*row, buttonWidth, buttonHeight), "retreat")){
			actionString = "double retreat";
			distance+=2;
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
