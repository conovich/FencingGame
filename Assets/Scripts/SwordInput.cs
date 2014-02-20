using UnityEngine;
using System.Collections;

public class SwordInput : MonoBehaviour {
	public Transform _MyHilt;
	public Transform _MyTip;

	float xInput;
	float yInput;

	bool isInput = false;

	public float rotMagnitude = 10.0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		GetInput();
		RotateJoint(isInput);
	}

	void GetInput(){
		isInput = false;

		if(Input.GetAxis("X Axis") != 0){
			xInput = Input.GetAxis("X Axis");
			isInput = true;
		}
		if(Input.GetAxis("Y Axis") != 0){
			yInput = Input.GetAxis("Y Axis");
			isInput = true;
		}
	}

	void RotateJoint(bool isInput){
		if(isInput){
			//sin(theta) = opp/hyp --> y axis = opp, hyp = dist from hilt to tip
			float swordLength = (_MyHilt.position - _MyTip.position).magnitude;
			float thetaY = yInput/swordLength;

			float thetaX = xInput/swordLength;

			if(_MyHilt.eulerAngles.y > 
			_MyHilt.Rotate(0.0f, thetaY*rotMagnitude, 0.0f);
			_MyHilt.Rotate(0.0f, thetaY*rotMagnitude, -thetaX*rotMagnitude); //since sword rotated in scene, -global x = local z
		}
	}
}
