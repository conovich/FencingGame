using UnityEngine;
using System.Collections;

public class JoystickInput : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetButtonDown("A Button")){
			Debug.Log("YAY A");
		}
		if(Input.GetButtonDown("B Button")){
			Debug.Log("YAY B");
		}
		if(Input.GetButtonDown("X Button")){
			Debug.Log("YAY X");
		}
		if(Input.GetButtonDown("Y Button")){
			Debug.Log("YAY Y");
		}
		if(Input.GetAxis("X Axis") > 0){
			//Debug.Log("HEYO +X AXIS" + Input.GetAxis("X Axis"));
		}
		if(Input.GetAxis("X Axis") < 0){
			//Debug.Log("HEYO -X AXIS" + Input.GetAxis("X Axis"));
		}
	}
}
