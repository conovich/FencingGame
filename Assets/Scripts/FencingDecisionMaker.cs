using UnityEngine;
using System.Collections;

//going to have to add multiple layers. brainstormed ways:
//1) pointers so that you only ever create one of each type, next levels point to location of originals
//2) make a new one every time...
//3) make one of each type at the very beginning, copy this type every time...

public class FencingDecisionMaker : MonoBehaviour {
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
			case "Advance":
				//vvvadd something like below to check if the action is in the action keys!vvv
				//if(_actionKeys.Contains(..
				currentAction._PossibilitySpace.Add(new Action("Retreat"));
				currentAction._PossibilitySpace.Add(new Action("DoubleRetreat"));
				currentAction._PossibilitySpace.Add(new Action("LungeRecover"));
				break;
			case "Retreat":
				currentAction._PossibilitySpace.Add(new Action("Advance"));
				currentAction._PossibilitySpace.Add(new Action("LungeRecover"));
				break;
			case "LungeRecover":
				currentAction._PossibilitySpace.Add(new Action("ParrySix"));
				currentAction._PossibilitySpace.Add(new Action("Retreat"));
				currentAction._PossibilitySpace.Add(new Action("DoubleRetreat"));
				currentAction._PossibilitySpace.Add(new Action("ParryEight"));
				currentAction._PossibilitySpace.Add(new Action("ParrySeven"));
				break;
			}
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
 