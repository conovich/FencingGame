using UnityEngine;
using System.Collections;

public class SwordController : MonoBehaviour {
	public ControllerInput _MyControllers;

	public Player _MyPlayer;

	public GUIText _MyActionText;

	public Transform _MyHiltTransform;
	public Transform _MyTipTransform;
	public SwordTip _MyTip;
	public float WorldRotMagnitudeX;
	public float WorldRotMagnitudeY;

	public float returnToCenterTime = 0.04f;

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

	public Quaternion originalRot;
	public Quaternion currentRot;

	// Use this for initialization
	void Start () {
		originalRot = _MyHiltTransform.rotation;
	}

	// Update is called once per frame
	void Update () {
		currentRot = _MyHiltTransform.rotation;

		GetInput();

		RotateJoint();
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

		_MyTip._SwordInput = isInput;
	}

	void RotateJoint(){
		if(isInput){
			//sin(theta) = opp/hyp --> y axis = opp, hyp = dist from hilt to tip
			float swordLength = (_MyHiltTransform.position - _MyTipTransform.position).magnitude;
			float worldThetaX = yInput/swordLength;
			float worldThetaY = xInput/swordLength;


			//if(tag == "Player1Sword"){

				if(_MyTipTransform.position.y <= _TipCenter.y + _MaxYDist && _MyTipTransform.position.y >= _TipCenter.y - _MaxYDist){
					//_MyHilt.Rotate(-worldThetaX*rotMagnitude, 0.0f, 0.0f, Space.World);
					RotateHiltX(-worldThetaX*WorldRotMagnitudeX);
					if(_MyTipTransform.position.y >= _TipCenter.y + _MaxYDist || _MyTipTransform.position.y <= _TipCenter.y - _MaxYDist){ //cap to boundaries -- undo last rot
						//_MyHilt.Rotate(worldThetaX*rotMagnitude, 0.0f, 0.0f, Space.World);
						RotateHiltX(worldThetaX*WorldRotMagnitudeX);
					}
				}

				if(_MyTipTransform.position.x <= _TipCenter.x + _MaxXDist && _MyTipTransform.position.x >= _TipCenter.x - _MaxXDist){
					//_MyHilt.Rotate(0.0f, worldThetaY*rotMagnitude, 0.0f, Space.World);
					RotateHiltY(worldThetaY*WorldRotMagnitudeY);
					if(_MyTipTransform.position.x >= _TipCenter.x + _MaxXDist || _MyTipTransform.position.x <= _TipCenter.x - _MaxXDist){ //cap to boundaries -- undo last rot 
						//_MyHilt.Rotate(0.0f, -worldThetaY*rotMagnitude, 0.0f, Space.World);
						RotateHiltY(-worldThetaY*WorldRotMagnitudeY);
					}
				}

			//}

		}
		else{
			ReturnTipToCenter();
		}
	}



	public bool ShouldWrite = false;
	public FileWriter _MyFileWriter;

	void WriteAngle(string XOrY, float angle){
		_MyFileWriter = GameObject.FindGameObjectWithTag("FileWriter").GetComponent<FileWriter>();
		_MyFileWriter.AppendToFile(XOrY + "," + angle + ",");
	}




	void RotateHiltX(float angle){
		_MyHiltTransform.Rotate(angle, 0.0f, 0.0f, Space.World);
		currentWorldRotX += angle;


		//WRITING THINGS
		WriteAngle("x", angle);
	}

	void RotateHiltY(float angle){
		_MyHiltTransform.Rotate(0.0f, angle, 0.0f, Space.World);
		currentWorldRotY += angle;

		//WRITING THINGS
		WriteAngle("y", angle);
	}

	void ReturnTipToCenter(){
		_MyHiltTransform.rotation = Quaternion.Slerp(_MyHiltTransform.rotation, originalRot, returnToCenterTime);
	}

	void CheckForMove(){
		if(!isInput){
			if(_MyTip._MySequence.Count > 0){
				EvaluateMove();
			}
		}

	}


	void EvaluateMove(){ //based on start pos and last pos, can the move be defined?
		string newMove = gameObject.GetComponent<Sequencer>().CheckSequence(_MyTip._MySequence);
		_MyTip.ClearInputList();
		SetCurrentMove(newMove);
	}


	void SetCurrentMove(string newMove){
		if(_MyPlayer){
			_MyPlayer.SetMyActionText(newMove);
		}
		else if(_MyActionText){
			_MyActionText.text = "Action: " + newMove;
		}
	}

	void OnCollisionEnter(Collision collision){ //should be for rigidbody collisions -- blade, not tip

	}

	public void AIParrySix(){
		float slerpTime = 0.04f;
		Quaternion target = new Quaternion(originalRot.x, originalRot.y, originalRot.z, originalRot.w - 45.0f);
		_MyHiltTransform.rotation = Quaternion.Slerp(_MyHiltTransform.rotation, target, slerpTime);
	}

	public void AIParryFour(){

	}

	public void AICircleSix(){
		
	}

	public void AICircleFour(){
		
	}

	public void AIParryEight(){

	}

	public void AIParrySeven(){

	}

	public void AIDisengageIn(){

	}

	public void AIDisengageOut(){

	}


}
