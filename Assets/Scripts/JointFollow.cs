using UnityEngine;
using System.Collections;

public class JointFollow: MonoBehaviour {
	public Transform _MyRoot;
	public Transform _JointToFollow;

	public Vector3 _Offset;

	public Vector3 _ChangeInRot;

	Vector3 _origFollowRot;
	Vector3 _origMyRot;
	// Use this for initialization
	void Start () {
		_origFollowRot = _JointToFollow.rotation.eulerAngles;
		_origMyRot = _MyRoot.rotation.eulerAngles;
	}
	
	// Update is called once per frame
	void Update () {
		UpdateMyPosition();

		UpdateMyRotation();

		
	}

	void UpdateMyPosition(){
		_MyRoot.position = _JointToFollow.position + _Offset;
	}

	void UpdateMyRotation(){
		_ChangeInRot = (_JointToFollow.rotation.eulerAngles - _origFollowRot);
		Vector3 newRot = _origMyRot + _ChangeInRot;
		_MyRoot.eulerAngles.Set(newRot.x, newRot.y, newRot.z);// += _JointToFollow.rotation - _oldFollowRot;

	}
}
