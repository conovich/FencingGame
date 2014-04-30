using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public class SwordMotionPlayer : MonoBehaviour {
	List<float> xRots;
	List<float> yRots;

	//rotation files
	public TextAsset parrySixText;
	public TextAsset parryFourText;
	public TextAsset circleFourText;
	public TextAsset circleSixText;
	public TextAsset parryEightText;
	public TextAsset parrySevenText;
	public TextAsset disengageInText;
	public TextAsset disengageOutText;
	public TextAsset oneTwoInOutText;
	public TextAsset oneTwoOutInText;


	// Use this for initialization
	void Start () {
		FillXYLists("parry six");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void FillXYLists(string actionName){
		xRots = new List<float>();
		yRots = new List<float>();

		TextAsset myFile = new TextAsset();
		switch(actionName){
		case "parry six":
			myFile = parrySixText;
			break;
		case "parry four":
			myFile = parryFourText;
			break;
		case "circle six":
			myFile = circleSixText;
			break;
		case "circle four":
			myFile = circleFourText;
			break;
		case "parry eight":
			myFile = parryEightText;
			break;
		case "parry seven":
			myFile = parrySevenText;
			break;
		case "disengage in":
			myFile = disengageInText;
		break;
		case "disengage out":
				myFile = disengageOutText;
			break;
		case "one two in out":
			myFile = oneTwoInOutText;
			break;
		case "one two out in":
			myFile = oneTwoOutInText;
			break;
		}


		//form: x,angle,y,angle
		//one line per file
		string rotations = myFile.text;
		string[] splitRotations = rotations.Split(',');

		for(int i = 0; i < splitRotations.Length - 3; i+=4){ //we access 4 items per loop
			xRots.Add(float.Parse(splitRotations[i+1]));
			yRots.Add(float.Parse(splitRotations[i+3]));
		}
		bool debugBool = true;
	}
}
