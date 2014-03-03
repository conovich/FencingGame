using UnityEngine;
using System.Collections;

public class SwordController : MonoBehaviour {
	public Transform _MyHilt;
	public Transform _MyTip;
	public float rotMagnitude = 10.0f;

	public float worldRotMaxX;
	public float worldRotMaxY;
	public float worldRotMinX;
	public float worldRotMinY;

	public Vector2 _TipCenter;
	public float _MaxXDist;
	public float _MaxYDist;


	float xInput;
	float yInput;

	bool isInput = false;

	Quaternion originalRot;

	// Use this for initialization
	void Start () {
		originalRot = _MyHilt.rotation;
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
			float worldThetaX = yInput/swordLength;
			float worldThetaY = xInput/swordLength;


			//ellipse equation
			float ellipseTipValueX = Mathf.Pow(_TipCenter.x - _MyTip.position.x, 2)/Mathf.Pow(_MaxXDist,2);
			float ellipseTipValueY = Mathf.Pow(_TipCenter.y - _MyTip.position.y, 2)/Mathf.Pow(_MaxYDist,2);

			float ellipseTipValue = ellipseTipValueX + ellipseTipValueY;

			if(tag == "Player1Sword"){

				if(_MyTip.position.y <= _TipCenter.y + _MaxYDist && _MyTip.position.y >= _TipCenter.y - _MaxYDist){
					_MyHilt.Rotate(-worldThetaX*rotMagnitude, 0.0f, 0.0f, Space.World);
					if(_MyTip.position.y >= _TipCenter.y + _MaxYDist || _MyTip.position.y <= _TipCenter.y - _MaxYDist){ //cap to boundaries -- undo last rot
						_MyHilt.Rotate(worldThetaX*rotMagnitude, 0.0f, 0.0f, Space.World);
					}
				}

				if(_MyTip.position.x <= _TipCenter.x + _MaxXDist && _MyTip.position.x >= _TipCenter.x - _MaxXDist){
					_MyHilt.Rotate(0.0f, worldThetaY*rotMagnitude, 0.0f, Space.World);
					if(_MyTip.position.x >= _TipCenter.x + _MaxXDist || _MyTip.position.x <= _TipCenter.x - _MaxXDist){ //cap to boundaries -- undo last rot 
						_MyHilt.Rotate(0.0f, -worldThetaY*rotMagnitude, 0.0f, Space.World);
					}
				}

			}

		}
		else{
			ReturnTipToCenter();
		}
	}

	void ReturnTipToCenter(){
		/*//should LERP next

		Quaternion currentRot = _MyHilt.rotation;
		float deltaThetaY = originalRot.y + currentRot.y;
		float deltaThetaX = originalRot.x - currentRot.x;

		_MyHilt.Rotate(0.0f, deltaThetaY, 0.0f);*/

	}

	void OnCollisionEnter(Collision collision){ //should be for rigidbody collisions -- blade, not tip

	}
}
