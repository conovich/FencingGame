using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public class SwordAI : MonoBehaviour {

	public Player _Player1;

		
	public static List<Action> _MotherList;
	public TextAsset _ActionResponseText;

	public GUIText _MyResponse;

	private string _OldPlayerAction;
	private SwordController _myAISwordController;
	private SwordMotionParser _mySwordMotionParser;


	private bool shouldExecuteMotion = false;
	public string lastMotionExecuted = "";

	public class Action{
		public List<Action> _PossibilitySpace;
		public string name;

		public int weight;

		public Action(){
			_PossibilitySpace = new List<Action>();
		}
		public Action(string newName){
			name = newName;
			_PossibilitySpace = new List<Action>();
		}  
	}

	// Use this for initialization
	void Start () {
		_Player1 = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
		_myAISwordController = GameObject.FindGameObjectWithTag("Player2Sword").GetComponent<SwordController>();
		_mySwordMotionParser = GameObject.FindGameObjectWithTag("Player2Sword").GetComponent<SwordMotionParser>();

		if(_MotherList == null){
			InstantiateMotherList();
		}
		Debug.Log("startinggggg");
		_OldPlayerAction = _Player1._MyActionText.text;
	}
		
	// Update is called once per frame
	void Update () {
		IsInMotherList("hi");//debug
		if(_OldPlayerAction != _Player1._MyActionText.text){ //if the player action has changed... oldplayeraction gets set in ParsePlayerAction()
			ParsePlayerAction();
		}

		if(shouldExecuteMotion){
			ExecuteResponse();
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
		case "parry eight":
			SelectResponse(_Player1._MyActionText.text);
			break;
		case "parry seven":
			SelectResponse(_Player1._MyActionText.text);
			break;
		}
	}
		
	void SelectResponse(string playerActionString){
		Action playerAction = FindInMotherList(playerActionString);

	
		/*original
		int random = Random.Range(0, playerAction._PossibilitySpace.Count);
		string myResponse = playerAction._PossibilitySpace[random].name;
		*/
		//new with WEIGHTS
		int bestWeight = GetMaxWeight(playerAction._PossibilitySpace);
		List<Action> bestActions = GetBestActions(playerAction._PossibilitySpace, bestWeight);
		int random = Random.Range(0, bestActions.Count);
		string myResponse = bestActions[random].name;


		_MyResponse.text = myResponse;
		

		_mySwordMotionParser.FillXYLists(myResponse);
		lastMotionExecuted = myResponse;
		shouldExecuteMotion = true;
	}

	List<Action> GetBestActions(List<Action> aPossibilitySpace, int bestWeight){
		List<Action> bestActions = new List<Action>();
		for (int i = 0; i < aPossibilitySpace.Count; i++){
			if(aPossibilitySpace[i].weight == bestWeight){
				bestActions.Add(aPossibilitySpace[i]);
			}
		}
		return bestActions;
	}

	int GetMaxWeight(List<Action> aPossibilitySpace){
		int maxWeight = 0;
		for (int i = 0; i < aPossibilitySpace.Count; i++){
			if(aPossibilitySpace[i].weight > maxWeight){
				maxWeight = aPossibilitySpace[i].weight;
			}
		}
		return maxWeight;
	}

	//working on this! debugging just parry six for now
	void ExecuteResponse(){
		float nextXRot = 0.0f;
		float nextYRot = 0.0f;
		if(_mySwordMotionParser.GetNextX(out nextXRot)){
			if(_mySwordMotionParser.GetNextY(out nextYRot)){
				//rotate sword hilt by nextXRot and nextYRot much.
				_myAISwordController.RotateHiltX(nextXRot);
				_myAISwordController.RotateHiltY(nextYRot);
			}
		}
		else{
			//set this once we're done executing the motion!
			shouldExecuteMotion = false;
		}
	}

	public void UpdateSuccessfulMove(string move, int additionalWeight){
		Action moveAction = FindInMotherList(move);
		moveAction.weight += additionalWeight;
		lastMotionExecuted = "";
		Debug.Log("updated successful move " + move + moveAction.weight.ToString());
	}


}
