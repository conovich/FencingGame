using UnityEngine;
using System.Collections;

public class SwordController : MonoBehaviour {
	public ControllerInput _MyControllers;

	public Player _MyPlayer;

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
		public MoveType moveType = MoveType.none;

		public enum MoveType{ //add more
			none,
			nonrealMove,
			disengage,
			parryFour,
			parrySix,
			circleFour,
			circleSix
		}

		public void SetState(Move.MoveType newType){
			moveType = newType;
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

		if(gameObject.tag == "Player1Sword"){
			if(_MyControllers.state1.ThumbSticks.Left.X != 0){
				xInput = _MyControllers.state1.ThumbSticks.Left.X;
				isInput = true;
			}
			if(_MyControllers.state1.ThumbSticks.Left.Y != 0){
				yInput = -_MyControllers.state1.ThumbSticks.Left.Y;
				isInput = true;
			}
		}
		else if(gameObject.tag == "Player2Sword"){ 
			if(_MyControllers.state2.ThumbSticks.Left.X != 0){
				xInput = -_MyControllers.state2.ThumbSticks.Left.X; //NEGATIVE ONLY FOR DEMO SINGLE PLAYER CAMERA
				isInput = true;
			}
			if(_MyControllers.state2.ThumbSticks.Left.Y != 0){
				yInput = _MyControllers.state2.ThumbSticks.Left.Y;
				isInput = true;
			}
		}
	}

	void RotateJoint(bool isInput){
		if(isInput){
			//sin(theta) = opp/hyp --> y axis = opp, hyp = dist from hilt to tip
			float swordLength = (_MyHilt.position - _MyTip.position).magnitude;
			float worldThetaX = yInput/swordLength;
			float worldThetaY = xInput/swordLength;


			//if(tag == "Player1Sword"){

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

			//}

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

		if(currentWorldRotY > 0 + deltaAngle){ 
			RotateHiltY(-deltaAngle);
		}
		else if(currentWorldRotY < 0 - deltaAngle){
			RotateHiltY(deltaAngle);
		}

		if(currentWorldRotX > 0 + deltaAngle){
			RotateHiltX(-deltaAngle);
		}
		else if(currentWorldRotX < 0 - deltaAngle){
			RotateHiltX(deltaAngle);
		}
	}

	void CheckForMove(){
		if(isInput){
			currentMove.isComplete = false;
			if(currentMove.tipStartPos == Vector3.zero){ //start of new move!
				currentMove.tipStartPos = _MyTip.position;
				currentMove.tipLastPos = _MyTip.position;
				SetCurrentMoveType(Move.MoveType.none);
			}
			else{ //if it's not the start of a new move...
				EvaluateMove(); //assign move!
			}
		}
		else{ //no more input!
			if(currentMove.tipStartPos != Vector3.zero){//*just* finished a move
				//set what move it is here!
				currentMove.isComplete = true;
				currentMove.tipStartPos = Vector3.zero;
				currentMove.tipLastPos = Vector3.zero;
			}
		}

	}

	void EvaluateMove(){ //based on start pos and last pos, can the move be defined?
		//currentMove.tipLastPos = _MyTip.position;
		//parry six
		Debug.Log("X Rot: " + currentWorldRotX);
		Debug.Log("Y Rot: " + currentWorldRotY);

		if(currentMove.tipLastPos.x > _MyTip.position.x && (Mathf.Abs(currentWorldRotX) < 1.0f)){
			SetCurrentMoveType(Move.MoveType.parrySix);
		}
		else if(currentMove.tipLastPos.x < _MyTip.position.x && (Mathf.Abs(currentWorldRotX) < 1.0f)){
			SetCurrentMoveType(Move.MoveType.parryFour);
		}
		else{
			SetCurrentMoveType(Move.MoveType.nonrealMove);
		}
		currentMove.tipLastPos = _MyTip.position;
	}

	void SetCurrentMoveType(Move.MoveType newtype){
		currentMove.SetState(newtype);
		_MyPlayer.SetMyActionText(currentMove.moveType.ToString());
	}

	void OnCollisionEnter(Collision collision){ //should be for rigidbody collisions -- blade, not tip

	}



}
