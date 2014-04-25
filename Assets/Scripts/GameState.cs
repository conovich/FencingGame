using UnityEngine;
using System.Collections;

public class GameState : MonoBehaviour {
	public static bool resetAllInPlay = false;

	public static int P1Score;
	public static int P2Score;

	public Player Player1;
	public Player Player2;

	public Timer myTimer;

	public ControllerInput _MyControllerInput;



	private static GameState _instance;
	public static GameState Instance{
		get{return _instance;}
	}

	public enum PlayerSelection{
		singlePlayer,
		multiplayer
	}

	//toggle-able in the main menu!!
	public static PlayerSelection playerSelection;

	public enum State{
		inCountdown = 0,
		inPlay = 1,
		paused = 2,
		pointScored = 3,
		matchOver = 4,
		resetPoint = 5,
		mainMenu = 6 //yeah, weird, i know.
	}

	public State CurrentState = State.mainMenu;
	public int debugView_CurrentState {get {return (int) CurrentState; }}

	void Awake(){
		if(_instance != null){
			Debug.Log("Instance already exists.");
			return;
		}
		_instance = this;
	}

	// Use this for initialization
	void Start () {

	}

	public void Reset(){
		ResetScores();
		if(myTimer){
			myTimer.ResetTimer();
		}
	}
	
	// Update is called once per frame
	void Update () {
		Debug.Log("My player state is: " + playerSelection);
		if(Application.loadedLevel == 1){
			if(resetAllInPlay){
				Reset ();
				resetAllInPlay = false;
			}
		}

		switch (CurrentState){
		case State.inCountdown:
			break;
		case State.inPlay:
			UpdatePlay();
			break;
		case State.paused:
			UpdatePaused();
			break;
		case State.pointScored:
			break;
		case State.resetPoint:
			UpdateResetPoint ();
			break;
		case State.matchOver:
			UpdateMatchOver();
			break;
		case State.mainMenu:
			break;
		}

		if(CurrentState != State.inPlay && myTimer != null){
			myTimer.StartOrStop(false);
		}
	}

	void UpdatePlay(){
		Time.timeScale = 1;
		myTimer.StartOrStop(true);
		if(myTimer.TimeOut){
			SetState(State.matchOver); //if time runs out, gameover!
		}
	}

	void UpdatePaused(){
		Time.timeScale = 0;
	}

	void UpdateResetPoint(){
		//check if someone won!
		if(P1Score == 5 || P2Score == 5){
			SetState(State.matchOver);
		}
		else{
			//Should:
			//1. tell players someone scored. --> happens in pointScored
				//handled by GUI
			//2. reset players to engarde lines
			ResetPlayers();
			//3. countdown.
			SetState(State.inCountdown);
		}
	}

	void UpdateMatchOver(){
		//should transition to this instead somehow
		Application.LoadLevel("MatchOver");
	}

	//Should change this to a reload scene perhaps? That's all Player1.reset does right now...
	void ResetPlayers(){
		Player1.ResetScene();
		//Player2.Reset();
	}

	public void SetState(State newState){
		CurrentState = newState;
	}

	void ResetScores(){
		P1Score = 0;
		P2Score = 0;
	}

	public void IncrementP1Score(){
		P1Score++;
		Debug.Log(P1Score);
	}

	public void IncrementP2Score(){
		P2Score++;
		if(P2Score == 5){
			SetState(State.matchOver);
		}
	}
}
