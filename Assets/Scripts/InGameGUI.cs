using UnityEngine;
using System.Collections;

public class InGameGUI : MonoBehaviour {
	public GUIText P1Score;
	public GUIText P2Score;
	public GUIText CountdownText;
	
	private float countdownTimeMax = 1.0f;
	private float countdownTimer = 1.0f; //init to countdowntimemax
	private int countdownNum = 3;

	// Use this for initialization
	void Start () {
		CountdownText.text = "";
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI(){
		P1Score.text = "P1: " + GameState.Instance.P1Score;
		P2Score.text = "P2: " + GameState.Instance.P2Score;

		switch(GameState.Instance.debugView_CurrentState){
		case 0: //inCountdown
			Countdown();
			break;
		case 1: //inPlay
			if(GUI.Button(new Rect(0, 0, 50, 30), "Pause")){
				GameState.Instance.SetState(GameState.State.paused);
				Debug.Log(GameState.Instance.debugView_CurrentState);
			}
			break;
		case 2: //paused
			if(GUI.Button(new Rect(0, 0, 50, 30), "Play")){
				GameState.Instance.SetState(GameState.State.inPlay);
				Debug.Log(GameState.Instance.debugView_CurrentState);
			}
			break;
		case 3: //pointScored
			break;
		case 4: //matchOver
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
				GameState.Instance.SetState(GameState.State.inPlay);
			}
		}
	}

	void ResetCountdown(){
		CountdownText.text = "";
		countdownNum = 3;
		countdownTimer = countdownTimeMax;
	}
}
