using UnityEngine;
using System.Collections;

public class CrossHares : MonoBehaviour {
	public Material _OnTargetMat;
	public Material _OffTargetMat;
	public Light _MyLight;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void ShowOnTarget(){
		gameObject.renderer.material = _OnTargetMat;
		_MyLight.intensity = 1;
	}

	public void ShowOffTarget(){
		gameObject.renderer.material = _OffTargetMat;
		_MyLight.intensity = 0;
	}
}
