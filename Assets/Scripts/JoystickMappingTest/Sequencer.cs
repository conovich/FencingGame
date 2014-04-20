using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public class Sequencer : MonoBehaviour {
	public List<List<string>> SequenceList;
	public TextAsset SequenceText;

	// Use this for initialization
	void Start () {
		InstantiateSequenceList();
	}
	
	// Update is called once per frame
	void Update () {

	}

	void InstantiateSequenceList(){
		SequenceList = new List<List<string>>();
		
		string allSequences = SequenceText.text;
		string[] sequenceArr = Regex.Split(allSequences,"\r\n");
		for(int i = 0; i < sequenceArr.Length; i++){
			string singleSequence = sequenceArr[i];
			string[] singleSequencePieces = singleSequence.Split(',');
			string name = singleSequencePieces[0]; //first item is the action, the rest are the responses

			AddSequence(singleSequencePieces);
		}
	}

	void AddSequence(string[] singleSequencePieces){
		List<string> singleSequenceList = new List<string>();
		for(int j = 0; j < singleSequencePieces.Length; j++){
			singleSequenceList.Add(singleSequencePieces[j]);
		}
		SequenceList.Add(singleSequenceList);
	}

	public string CheckSequence(List<string> inputSequence){
		string move = "nonreal move";

		for(int i = 0; i < SequenceList.Count; i++){
			if(SequenceList[i].Count - 1 == inputSequence.Count){ //first item in sequence list is the name!
				for(int j = 0; j < inputSequence.Count; j++){ //first item in input sequence is a valid move
					if(SequenceList[i][j + 1] != inputSequence[j]){
						break;
					}
					else if(j == inputSequence.Count - 1){ //if you get to the end of the input and everything matches, you found a valid move!
						move = SequenceList[i][0];
						return move;
					}
				}
			}
		}
		return move;
	}

}
