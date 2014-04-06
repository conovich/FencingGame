using UnityEngine;
using System.Collections;

public class FencingDecisionMakerText : MonoBehaviour {
	public PlayerInput myPlayer;

	public ArrayList _actionKeys = new ArrayList();
	
	public class Action{
		public ArrayList _PossibilitySpace;
		public string name;
		
		public Action(){
			_PossibilitySpace = new ArrayList();
		}
		public Action(string newName){
			name = newName;
			_PossibilitySpace = new ArrayList();
		}
	}
	// Use this for initialization
	void Start () {
		CreateActionKeys();
		MapActionKeys();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void CreateActionKeys(){
		foreach(AnimationState anim in animation){
			_actionKeys.Add(new Action(anim.name));
		}
	}
	
	void MapActionKeys(){
		for(int i = 0; i < _actionKeys.Count; i++){
			Action currentAction = (Action)_actionKeys[i];
			
			switch (currentAction.name){
			case "engarde":
				break;
			case "lunge recover":
				currentAction._PossibilitySpace.Add(new Action("parry 6"));
				currentAction._PossibilitySpace.Add(new Action("parry 4"));
				currentAction._PossibilitySpace.Add(new Action("retreat"));
				break;
			case "parry 6":
				currentAction._PossibilitySpace.Add(new Action("disengage cw"));
				break;
			case "parry 4":
				currentAction._PossibilitySpace.Add(new Action("disengage ccw"));
				break;
			case "disengage cw":
				currentAction._PossibilitySpace.Add(new Action("lunge recover"));
				break;
			case "disengage ccw":
				currentAction._PossibilitySpace.Add(new Action("lunge recover"));
				break;
			case "circle 4":
				break;
			case "circle 6":
				break;
			case "advance":
				currentAction._PossibilitySpace.Add(new Action("retreat"));
				currentAction._PossibilitySpace.Add(new Action("lunge recover"));
				break;
			case "retreat":
				currentAction._PossibilitySpace.Add(new Action("advance"));
				currentAction._PossibilitySpace.Add(new Action("lunge recover"));
				break;
			}
			/*case "Advance":
				//vvvadd something like below to check if the action is in the action keys!vvv
				//if(_actionKeys.Contains(..
				currentAction._PossibilitySpace.Add(new Action("Retreat"));
				currentAction._PossibilitySpace.Add(new Action("DoubleRetreat"));
				currentAction._PossibilitySpace.Add(new Action("LungeRecover"));
				break;
			*/
		}
	}
	
	public string ChooseRandomResponse(string key){
		for(int i = 0; i < _actionKeys.Count; i++){
			Action currentAction = (Action)_actionKeys[i];
			if(currentAction.name == key){
				int randomChoice = Random.Range(0, currentAction._PossibilitySpace.Count);
				return ((Action)currentAction._PossibilitySpace[randomChoice]).name;
			}
		}
		Debug.Log("No reponses available.");
		return "";
	}
}
