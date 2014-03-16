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


	float currentWorldRotX;
	float currentWorldRotY;

	float xInput;
	float yInput;

	bool isInput = false;

	Quaternion originalRot;

	Move currentMove;

	public class Move{
		public Move(){

		}

		public bool isComplete = false;
		public Vector3 tipStartPos = Vector3.zero;
		public Vector3 tipLastPos = Vector3.zero;
		public MoveType currentMove = MoveType.none;

		public enum MoveType{ //add more
			none,
			nonrealMove,
			disengage,
			parryFour,
			parrySix,
			circleFour,
			circleSix
		}
	}

	// Use this for initialization
	void Start () {
		originalRot = _MyHilt.rotation;
		currentMove = new Move();
	}

	// Update is called once per frame
	void Update () {
		GetInput();
		RotateJoint(isInput);
		CheckForMove();
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
					//_MyHilt.Rotate(-worldThetaX*rotMagnitude, 0.0f, 0.0f, Space.World);
					RotateHiltX(-worldThetaX*rotMagnitude);
					if(_MyTip.position.y >= _TipCenter.y + _MaxYDist || _MyTip.position.y <= _TipCenter.y - _MaxYDist){ //cap to boundaries -- undo last rot
						//_MyHilt.Rotate(worldThetaX*rotMagnitude, 0.0f, 0.0f, Space.World);
						RotateHiltX(worldThetaX*rotMagnitude);
					}
				}

				if(_MyTip.position.x <= _TipCenter.x + _MaxXDist && _MyTip.position.x >= _TipCenter.x - _MaxXDist){
					//_MyHilt.Rotate(0.0f, worldThetaY*rotMagnitude, 0.0f, Space.World);
					RotateHiltY(worldThetaY*rotMagnitude);
					if(_MyTip.position.x >= _TipCenter.x + _MaxXDist || _MyTip.position.x <= _TipCenter.x - _MaxXDist){ //cap to boundaries -- undo last rot 
						//_MyHilt.Rotate(0.0f, -worldThetaY*rotMagnitude, 0.0f, Space.World);
						RotateHiltY(-worldThetaY*rotMagnitude);
					}
				}

			}

		}
		else{
			ReturnTipToCenter();
		}
	}

	void RotateHiltX(float angle){
		_MyHilt.Rotate(angle, 0.0f, 0.0f, Space.World);
		currentWorldRotX += angle;
	}

	void RotateHiltY(float angle){
		_MyHilt.Rotate(0.0f, angle, 0.0f, Space.World);
		currentWorldRotY += angle;
	}

	void ReturnTipToCenter(){
		float deltaAngle = 0.2f;

		if(currentWorldRotY > 0){ 
			RotateHiltY(-deltaAngle);
		}
		else if(currentWorldRotY < 0){
			RotateHiltY(deltaAngle);
		}

		if(currentWorldRotX > 0){
			RotateHiltX(-deltaAngle);
		}
		else if(currentWorldRotX < 0){
			RotateHiltX(deltaAngle);
		}
		

	}

	void CheckForMove(){
		if(isInput){
			currentMove.isComplete = false;
			if(currentMove.tipStartPos == Vector3.zero){ //start of new move!
				currentMove.tipStartPos = _MyTip.position;
			}
			EvaluateMove(); //assign move!
		}
		else{ //no more input!
			if(currentMove.tipStartPos != Vector3.zero){//*just* finished a move
				//set what move it is here!
				currentMove.isComplete = true;
				currentMove.tipStartPos = Vector3.zero;
			}
		}

	}

	void EvaluateMove(){ //based on start pos and last pos, can the move be defined?

	}

	void OnCollisionEnter(Collision collision){ //should be for rigidbody collisions -- blade, not tip

	}



}
