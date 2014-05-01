using UnityEngine;
using System.Collections;

public class InGameGUI : MonoBehaviour {
	public GUIText P1Score;
	public GUIText P2Score;
	public GUIText CountdownText;
	public GUIText TellScoreText;
	
	private float countdownTimeMax = 1.0f;
	private float countdownTimer = 1.0f; //init to countdowntimemax
	private int countdownNum = 3;

	private float tellScoreTimeMax = 4.0f;
	private float tellScoreTimer = 4.0f;

	// Use this for initialization
	void Start () {
		CountdownText.text = "";
		TellScoreText.text = "";
		GameState.Instance._MyControllerInput.GetControllers();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI(){
		P1Score.text = "P1: " + GameState.P1Score;
		P2Score.text = "P2: " + GameState.P2Score;

		switch(GameState.Instance.debugView_CurrentState){ //I DID IT THIS WAY BECAUSE THE DAMN ENUM WAS UNLOADED
		case 0: //inCountdown
			Countdown();
			break;
		case 1: //inPlay
			if(GUI.Button(new Rect(0, 0, Screen.width/7.0f, Screen.height/10.0f), "Pause")){
				GameState.Instance.SetState(GameState.State.paused);
				Debug.Log(GameState.Instance.debugView_CurrentState);
			}

			if(GUI.Button(new Rect((Screen.width/7.0f)+20.0f, 0, Screen.width/7.0f, Screen.height/10.0f), "Restart")){ //in play reset button
				Application.LoadLevel("MainMenu");
			}
			break;
		case 2: //paused
			if(GUI.Button(new Rect(0, 0, Screen.width/7.0f, Screen.height/10.0f), "Play")){
				GameState.Instance.SetState(GameState.State.inPlay);
				Debug.Log(GameState.Instance.debugView_CurrentState);
			}

			if(GUI.Button(new Rect((Screen.width/7.0f)+20.0f, 0, Screen.width/7.0f, Screen.height/10.0f), "Restart")){ //pause reset button
				Application.LoadLevel("MainMenu");
			}
			break;
		case 3: //pointScored
			TellScore();
			break;
		case 4: //matchOver
			break;
		case 5: //restPoint
			break;
		}
	}

	void Countdown(){
		if(countdownTimer > 0){
			countdownTimer -= Time.deltaTime;
		}
		else if(countdownTimer <= 0){
			if(countdownNum > 0){
				CountdownText.text = (countdownNum--).ToString();
				countdownTimer = countdownTimeMax;
			}
			else if(countdownNum == 0){
				CountdownText.text = "Go!";
				countdownTimer = countdownTimeMax;
				countdownNum--;
			}
			else if(countdownNum == -1){
				CountdownText.text = "";
				countdownTimer = countdownTimeMax;
				countdownNum = 3;
				GameState.Instance.SetState(GameState.State.inPlay);
			}
		}
	}

	void ResetCountdown(){
		CountdownText.text = "";
		countdownNum = 3;
		countdownTimer = countdownTimeMax;
	}

	void TellScore(){
		if(tellScoreTimer > 0){
			tellScoreTimer -= Time.deltaTime;
			TellScoreText.text = "Player" + "Scored!" + "\n" + "P1: " + P1Score.text.ToString() + ", P2: " + P2Score.text.ToString();
			Debug.Log(TellScoreText.text);
		}
		else{
			TellScoreText.text = "";
			tellScoreTimer = tellScoreTimeMax;
			GameState.Instance.SetState(GameState.State.resetPoint);
		}
	}
}
