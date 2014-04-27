using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DistanceAI : MonoBehaviour {
	public Transform _PlayerFencer;

	public float _LungeDistance;

	private List<string> MovementListClose;
	private List<string> MovementListFar;

	// Use this for initialization
	void Start () {
		InitMovementLists();
	}

	void InitMovementLists(){
		//add in double retreats and double advances
		MovementListClose = new List<string>();
		MovementListFar = new List<string>();

		MovementListClose.Add("Advance 1");
		MovementListClose.Add("Retreat");
		MovementListClose.Add("Retreat");
		MovementListClose.Add("LungeRecover");
		MovementListClose.Add("Retreat");
		MovementListClose.Add("LungeRecover");

		MovementListFar.Add("Advance 1");
		MovementListFar.Add("Advance 1");
		MovementListFar.Add("Advance 1");
		MovementListFar.Add("Advance 1");
		MovementListFar.Add("Retreat");
		MovementListFar.Add("Retreat");
	}

	// Update is called once per frame
	void Update () {

	}

	float GetDistanceToPlayer(){
		float distance = (transform.position - _PlayerFencer.transform.position).magnitude;
		return distance;
	}

	//based on distance to player (like within lunge distance or extension distance...),
	//...pick advance, retreat, double retreat, double advance, or lunge!
	//then AI fencer should do this. like actually.
	public string ChooseMovement(){
		//KissAndrei();
		if(GetDistanceToPlayer() < _LungeDistance){
			int random = Random.Range(0, MovementListClose.Count);
			return MovementListClose[random];
		}
		else{
			int random = Random.Range(0, MovementListFar.Count);
			return MovementListFar[random];
		}


	}
}
