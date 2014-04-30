using UnityEngine;
using System.Collections;
using System.IO;

public class FileWriter : MonoBehaviour {
	public string FileName;
	//"C:\blahblah_yourfilepath\yourtextfile.txt"
	public string FilePath;// = "C:\Users\Corey\Documents\GitHub\FencingGame\Assets\TextFiles";

	// Use this for initialization
	void Start () {
		FilePath = @"C:\Users\Corey\Documents\GitHub\FencingGame\Assets\TextFiles\Output\FencingMoves";

		// This text is added only once to the file. 
		if (!System.IO.File.Exists(FilePath))
		{
			Debug.Log("Writing!lskdjflsdk");
			// Create a file to write to. 
			string createText = "Hello and Welcome" + "\n";
			System.IO.File.WriteAllText(FilePath, createText);
		}
	}
	
	// Update is called once per frame
	void Update () {
		GetInput();
	}

	public void GetInput(){
		//parry four
		if(Input.GetKeyDown(KeyCode.Alpha4)){
			AddMoveToFile("ParryFour");
			Debug.Log("ParryFour");
		}
		//parry six
		if(Input.GetKeyDown(KeyCode.Alpha6)){
			AddMoveToFile("ParrySix");
			Debug.Log("ParrySix");
		}
		//parry seven
		if(Input.GetKeyDown(KeyCode.Alpha7)){
			AddMoveToFile("ParrySeven");
			Debug.Log("ParrySeven ");
		}
		//parry eight
		if(Input.GetKeyDown(KeyCode.Alpha8)){
			AddMoveToFile("ParryEight");
			Debug.Log("ParryEight");
		}

		//circle six
		if(Input.GetKeyDown(KeyCode.S)){
			AddMoveToFile("CircleSix");
			Debug.Log("CircleSix");
		}

		//circle four
		if(Input.GetKeyDown(KeyCode.F)){
			AddMoveToFile("CircleFour");
			Debug.Log("CircleFour");
		}

		//disengage in
		if(Input.GetKeyDown(KeyCode.I)){
			AddMoveToFile("DisengageIn");
			Debug.Log("DisengageIn");
		}

		//disengage out
		if(Input.GetKeyDown(KeyCode.O)){
			AddMoveToFile("DisengageOut");
			Debug.Log("DisengageOut");
		}

		//one-two in
		if(Input.GetKeyDown(KeyCode.Alpha1)){
			AddMoveToFile("OneTwoIn");
			Debug.Log("OneTwoIn");
		}

		//one-two out
		if(Input.GetKeyDown(KeyCode.Alpha2)){
			AddMoveToFile("OneTwoOut");
			Debug.Log("OneTwoOut");
		}


	}

	public void AddMoveToFile(string newName){
		AppendToFile("\n");
		AppendToFile(newName + ",");
	}
	
	public void AppendToFile(string text){
		Debug.Log("Writing!" + text);
		System.IO.File.AppendAllText(FilePath, text);
	}
}
