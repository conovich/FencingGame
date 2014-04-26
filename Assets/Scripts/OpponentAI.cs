using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public class OpponentAI : MonoBehaviour {

	public Player _Player1;

		
	public List<Action> _MotherList;
	public TextAsset _ActionResponseText;

	public GUIText _MyResponse;

	private string _OldPlayerAction;


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
		_Player1 = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
			
		InstantiateMotherList();
		Debug.Log("startinggggg");
		_OldPlayerAction = _Player1._MyActionText.text;
	}
		
	// Update is called once per frame
	void Update () {
		IsInMotherList("hi");//debug
		if(_OldPlayerAction != _Player1._MyActionText.text){ //if the player action has changed... oldplayeraction gets set in ParsePlayerAction()
			ParsePlayerAction();
		}
	}
		
	bool IsInMotherList(string name){
		for(int i = 0; i < _MotherList.Count; i++){
			if(_MotherList[i].name == name){
				return true;
			}
		}
		return false;
	}

	Action FindInMotherList(string name){
		for(int i = 0; i < _MotherList.Count; i++){
			if(_MotherList[i].name == name){
				return _MotherList[i];
			}
		}
		return null;
	}
		
	void InstantiateMotherList(){
		_MotherList = new List<Action>();

		string actionResponseString = _ActionResponseText.text;
		string[] actionResponseArr = Regex.Split(actionResponseString,"\r\n");
		for(int i = 0; i < actionResponseArr.Length; i++){
			string actionResponseLine = actionResponseArr[i];
			string[] actionAndResponses = actionResponseLine.Split(',');
			string name = actionAndResponses[0]; //first item is the action, the rest are the responses
			if(!IsInMotherList(name)){ //if the first item doesn't already exist in the motherlist, make it!
				//create the action
				Action a = new Action(name);
				PopulatePossibilityList(a, actionAndResponses);
				_MotherList.Add(a);
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
		_OldPlayerAction = _Player1._MyActionText.text;
		switch (_Player1._MyActionText.text){
		case "engarde":
			_MyResponse.text = "engarde";
			break;
		case "lunge recover":
			//check inside or outside
			/*if(_Player1.swordIsOutside){
				SelectResponse("lunge recover outside");
			}
			else{
				SelectResponse("lunge recover inside");
			}	*/
			break;
		case "extend":
			//check inside or outside -- introduce this case later???
			/*if(_Player1.swordIsOutside){
				SelectResponse("extend outside");
			}
			else{
				SelectResponse("extend inside");
			}*/
			break;
		case "parry six":
			SelectResponse(_Player1._MyActionText.text);
			break;
		case "parry four":
			SelectResponse(_Player1._MyActionText.text);
			break;
		case "disengage in":
			SelectResponse(_Player1._MyActionText.text);
			break;
		case "disengage out":
			SelectResponse(_Player1._MyActionText.text);
			break;
		case "circle four":
			SelectResponse(_Player1._MyActionText.text);
			break;
		case "circle six":
			SelectResponse(_Player1._MyActionText.text);
			break;
		}
	}
		
	void SelectResponse(string playerActionString){
		Action playerAction = FindInMotherList(playerActionString);
		int random = Random.Range(0, playerAction._PossibilitySpace.Count);
		//Debug.Log(playerAction._PossibilitySpace[random]);
		_MyResponse.text = playerAction._PossibilitySpace[random].name;
	}

}
