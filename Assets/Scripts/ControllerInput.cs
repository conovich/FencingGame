using UnityEngine;
using System.Collections;
using XInputDotNetPure;

public class ControllerInput : MonoBehaviour {
	//controller variables
	bool player1IndexSet = false;
	bool player2IndexSet = false;
	PlayerIndex player1Index;
	PlayerIndex player2Index;
	public GamePadState state1;
	public GamePadState prevState1;
	public GamePadState state2;
	public GamePadState prevState2;


	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		GetState1();
		if(GameState.playerSelection == GameState.PlayerSelection.multiplayer){
			GetState2();
		}
	}

	public void GetControllers(){
		Debug.Log("MEEP" + GameState.playerSelection);

		// Find a PlayerIndex, for a single player game
		// Will find the first controller that is connected and use it
		if (!player1IndexSet || !prevState1.IsConnected)
		{
			for (int i = 0; i < 4; ++i)
			{
				PlayerIndex testPlayerIndex = (PlayerIndex)i;
				GamePadState testState = GamePad.GetState(testPlayerIndex);
				if (testState.IsConnected)
				{
					if(!player1IndexSet){
						Debug.Log(string.Format("GamePad found {0}", testPlayerIndex));
						player1Index = testPlayerIndex;
						player1IndexSet = true;
					}
				}
			}
		}
		
		if ((!player2IndexSet || !prevState2.IsConnected) && GameState.playerSelection == GameState.PlayerSelection.multiplayer)
		{
			for (int i = 0; i < 4; ++i)
			{
				PlayerIndex testPlayerIndex = (PlayerIndex)i;
				GamePadState testState = GamePad.GetState(testPlayerIndex);
				
				if(testPlayerIndex != player1Index){
					if (testState.IsConnected)
					{
						if(!player2IndexSet){
							Debug.Log(string.Format("GamePad found {0}", testPlayerIndex));
							player2Index = testPlayerIndex;
							player2IndexSet = true;
						}
					}
				}
			}
		}
		
		prevState1 = state1;
		state1 = GamePad.GetState(player1Index);

		if(GameState.playerSelection == GameState.PlayerSelection.multiplayer){
			prevState2 = state2;
			state2 = GamePad.GetState(player2Index);
		}
	}

	public void GetState1(){
		prevState1 = state1;
		state1 = GamePad.GetState(player1Index);
	}

	public void GetState2(){
		prevState2 = state2;
		state2 = GamePad.GetState(player2Index);
	}
}
