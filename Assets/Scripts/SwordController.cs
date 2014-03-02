using UnityEngine;
using System.Collections;

public class SwordController : MonoBehaviour {
	public Transform _MyHilt;
	public Transform _MyTip;
	public float rotMagnitude = 10.0f;

	public float rotMaxX;
	public float rotMaxY;
	public float rotMinX;
	public float rotMinY;

	public Vector2 _TipCenter;


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
			float thetaY = yInput/swordLength;

			float thetaX = xInput/swordLength;
			if(tag == "Player1Sword"){
				Debug.Log(_MyHilt.rotation.eulerAngles.z);
				//Debug.Log(thetaY);

				//capping angles. instead constrain tip position?
				if((_MyHilt.rotation.eulerAngles.y >= rotMinY && thetaY > 0)){
					_MyHilt.Rotate(0.0f, thetaY*rotMagnitude, 0.0f);
				}
				else if((_MyHilt.rotation.eulerAngles.y <= rotMaxY && thetaY < 0)){
					_MyHilt.Rotate(0.0f, thetaY*rotMagnitude, 0.0f);
				}

				//since sword rotated in scene, -global x = local z
				//if((_MyHilt.rotation.eulerAngles.z >= rotMinX && thetaX > 0)){
					_MyHilt.Rotate(0.0f, 0.0f, -thetaX*rotMagnitude);
				//}
				//else if((_MyHilt.rotation.eulerAngles.z <= rotMaxX && thetaX < 0)){
				//	_MyHilt.Rotate(0.0f, 0.0f, -thetaX*rotMagnitude);
				//}_
				Debug.Log(_MyTip.position);
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
