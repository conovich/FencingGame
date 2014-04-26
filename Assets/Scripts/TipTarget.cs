using UnityEngine;
using System.Collections;

public class TipTarget : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(GameState.Instance.CurrentState == GameState.State.inPlay){
			renderer.enabled = true;
		}
		else{
			renderer.enabled = false;
		}
	}
}
