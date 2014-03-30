using UnityEngine;
using System.Collections;

public class GameState : MonoBehaviour {
	public int P1Score;
	public int P2Score;

	public Player Player1;
	public Player Player2;

	private static GameState _instance;
	public static GameState Instance{
		get{return _instance;}
	}

	public enum State{
		inCountdown = 0,
		inPlay = 1,
		paused = 2,
		pointScored = 3,
		matchOver = 4,
		resetPoint = 5
	}

	public State CurrentState = State.inCountdown;
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
		Reset ();
	}

	void Reset(){
		ResetScores();
	}
	
	// Update is called once per frame
	void Update () {
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
			break;
		}
	}

	void UpdatePlay(){
		Time.timeScale = 1;
	}

	void UpdatePaused(){
		Time.timeScale = 0;
	}

	void UpdateResetPoint(){
		//Should:
		//1. tell players someone scored. --> happens in pointScored
			//handled by GUI
		//2. reset players to engarde lines
		ResetPlayers();
		//3. countdown.
		SetState(State.inCountdown);
	}

	void ResetPlayers(){
		Player1.Reset();
		Player2.Reset();
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
	}
}
