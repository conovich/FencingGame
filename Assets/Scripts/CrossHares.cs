using UnityEngine;
using System.Collections;

public class CrossHares : MonoBehaviour {
	public Material _OnTargetMat;
	public Material _OffTargetMat;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void ShowOnTarget(){
		gameObject.renderer.material = _OnTargetMat;
	}

	public void ShowOffTarget(){
		gameObject.renderer.material = _OffTargetMat;
	}
}
