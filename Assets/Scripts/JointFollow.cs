using UnityEngine;
using System.Collections;

public class JointFollow: MonoBehaviour {
	public Transform _MyRoot;
	public Transform _JointToFollow;

	public Vector3 _Offset;

	public Vector3 _ChangeInRot;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		UpdateMyPosition();

		//UpdateMyRotation();

		
	}

	void UpdateMyPosition(){
		_MyRoot.position = _JointToFollow.position + _Offset;
	}

	/*void UpdateMyRotation(){
		Debug.Log("Updating Rot");

		_ChangeInRot = (_JointToFollow.rotation.eulerAngles - _origFollowRot);
		Vector3 newRot = _origMyRot + _ChangeInRot;
		_MyRoot.eulerAngles.Set(newRot.x, newRot.y, newRot.z);// += _JointToFollow.rotation - _oldFollowRot;

	}*/
}
