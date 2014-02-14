using UnityEngine;
using System.Collections;

public class LabelFollow : MonoBehaviour {
	public Transform _Target;
	public Vector3 _Offset;

	Vector3 targetScreenPoint;



	// Use this for initialization
	void Start () {
		GetTargetScreenPoint();
	}
	
	// Update is called once per frame
	void Update () {
		GetTargetScreenPoint();
		SetPosition();
	}

	void GetTargetScreenPoint(){
		if(_Target){
			targetScreenPoint = Camera.main.WorldToViewportPoint(_Target.position);
		}
	}

	void SetPosition(){
		transform.position = targetScreenPoint + _Offset;
	}
}
