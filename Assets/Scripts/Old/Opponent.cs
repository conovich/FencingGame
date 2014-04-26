using UnityEngine;
using System.Collections;

public class Opponent : MonoBehaviour {
	private Player _thePlayer;
	
	// Use this for initialization
	void Start () {
		GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
		if(playerObject){
			_thePlayer = playerObject.GetComponent<Player>();	
		}
		if(_thePlayer == null){
			Debug.Log("Player not initialized.");	
		}
	}
	
	// Update is called once per frame
	void Update () {
		if(_thePlayer._CurrentState == Player.State.engarde){
			animation.Play("EngardePosition");
		}
		else if(_thePlayer._CurrentState == Player.State.advance){
			animation.Play("Retreat");
		}
		else if(_thePlayer._CurrentState == Player.State.retreat){
			animation.Play("Advance 1");
		}
		else if(_thePlayer._CurrentState == Player.State.lungeRecover){
			animation.Play("DoubleRetreat");
		}
		else if(_thePlayer._CurrentState == Player.State.idle){
			
		}
	}
}
