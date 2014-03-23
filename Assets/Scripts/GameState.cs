using UnityEngine;
using System.Collections;

public class GameState : MonoBehaviour {
	public int P1Score;
	public int P2Score;

	private static GameState _instance;
	public static GameState Instance{
		get{return _instance;}
	}

	public enum State{
		inCountdown = 0,
		inPlay = 1,
		paused = 2,
		pointScored = 3,
		matchOver = 4
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
			break;
		case State.paused:
			break;
		case State.pointScored:
			break;
		case State.matchOver:
			break;
		}
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
