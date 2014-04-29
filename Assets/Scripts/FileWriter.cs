using UnityEngine;
using System.Collections;
using System.IO;

public class FileWriter : MonoBehaviour {
	public string FileName;
	//"C:\blahblah_yourfilepath\yourtextfile.txt"
	public string FilePath;// = "C:\Users\Corey\Documents\GitHub\FencingGame\Assets\TextFiles";

	// Use this for initialization
	void Start () {
		FilePath = @"C:\Users\Corey\Documents\GitHub\FencingGame\Assets\TextFiles\FencingMoves";

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
			Debug.Log("ParrySeven");
		}
		//parry eight
		if(Input.GetKeyDown(KeyCode.Alpha8)){
			AddMoveToFile("ParryEight");
			Debug.Log("ParryEight");
		}

		//circle six

		//circle four

		//disengage in

		//disengage out

		//one-two in

		//one-two out


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
