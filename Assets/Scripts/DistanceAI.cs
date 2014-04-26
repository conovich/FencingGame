using UnityEngine;
using System.Collections;

public class DistanceAI : MonoBehaviour {
	public Transform _PlayerFencer;

	public float _LungeDistance;
	public float _ExtensionDistance;
	private int _distanceToPlayer;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(GameState.playerSelection = GameState.PlayerSelection.multiplayer){
			GetDistanceToPlayer();
		}
	}

	void GetDistanceToPlayer(){
		_distanceToPlayer = (transform.position - _PlayerFencer.transform.position).magnitude();
	}

	//based on distance to player (like within lunge distance or extension distance...),
	//...pick advance, retreat, double retreat, double advance, or lunge!
	//then AI fencer should do this. like actually.
	void ChooseMovement(){

	}
}
