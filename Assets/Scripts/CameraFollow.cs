using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {
	public Transform Target;
	public bool X;
	public bool Y;
	public bool Z;

	Vector3 initDistance;

	// Use this for initialization
	void Start () {
		initDistance = Target.position - transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 moveTo = Target.position - initDistance;
		if(!X){
			moveTo.x = transform.position.x;
		}
		if(!Y){
			moveTo.y = transform.position.y;
		}
		if(!Z){
			moveTo.z = transform.position.z;
		}
		transform.position = moveTo;
	}
}
