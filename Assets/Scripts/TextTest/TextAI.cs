using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public class TextAI : MonoBehaviour {
	public PlayerInput myPlayer;

	public List<Action> MotherList;
	public TextAsset ActionResponseText;



	public class Action{
		public List<Action> _PossibilitySpace;
		public string name;
		
		public Action(){
			_PossibilitySpace = new List<Action>  ();
		}
		public Action(string newName){
			name = newName;
			_PossibilitySpace = new List<Action>();
		}  
	}

	// Use this for initialization
	void Start () {
		InstantiateMotherList();
		Debug.Log("startinggggg");
	}
	
	// Update is called once per frame
	void Update () {
		IsInMotherList("hi");
		ParsePlayerAction();
	}

	bool IsInMotherList(string name){
		for(int i = 0; i < MotherList.Count; i++){
			if(MotherList[i].name == name){
				return true;
			}
		}
		return false;
	}

	Action FindInMotherList(string name){
		for(int i = 0; i < MotherList.Count; i++){
			if(MotherList[i].name == name){
				return MotherList[i];
			}
		}
		return null;
	}

	void InstantiateMotherList(){
		MotherList = new List<Action>();

		string actionResponseString = ActionResponseText.text;
		string[] actionResponseArr = Regex.Split(actionResponseString,"\r\n");
		for(int i = 0; i < actionResponseArr.Length; i++){
			string actionResponseLine = actionResponseArr[i];
			string[] actionAndResponses = actionResponseLine.Split(',');
			string name = actionAndResponses[0]; //first item is the action, the rest are the responses
			if(!IsInMotherList(name)){ //if the first item doesn't already exist in the motherlist, make it!
				//create the action
				Action a = new Action(name);
				PopulatePossibilityList(a, actionAndResponses);
				MotherList.Add(a);
			}
			else{
				Action a = FindInMotherList(name);
				PopulatePossibilityList(a, actionAndResponses);
			}
		}
	}

	void PopulatePossibilityList(Action a, string[] actionAndResponses){
		for(int j = 1; j < actionAndResponses.Length; j++){
			string responseName = actionAndResponses[j];
			Action response = FindInMotherList(responseName);
			if(response != null){//if response is already in the MotherList, add it!
				a._PossibilitySpace.Add(response);
			}
			else{//if response if not already in the MotherList, create it and add it!
				Action b = new Action(responseName);
				a._PossibilitySpace.Add(b);
			}
		}
	}
	
	void ParsePlayerAction(){
		switch (myPlayer.actionString){
		case "engarde":
			break;
		case "lunge recover":
			break;
		case "parry 6":
			break;
		case "parry 4":
			break;
		case "disengage cw":
			break;
		case "disengage ccw":
			break;
		case "circle 4":
			break;
		case "circle 6":
			break;
		}
	}
}
