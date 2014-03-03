using UnityEngine;
using System.Collections;

public class SwordTip : MonoBehaviour {
	public GameObject _LightBox;
	private LightBox _LightBoxScript;

	// Use this for initialization
	void Start () {
		GetLightBoxScript();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	//not working -- collider and joint 2 not moving/rotating
	void OnCollisionEnter(Collision collision){
		Debug.Log("Hit something!");
		if(collision.collider.tag == "TipConstraint"){

		}
		if(collision.collider.tag == "Player2Collider"){ //an opponent hit! this needs to be more "if it's player two..." or something more versatile.
			Debug.Log("Hit player 2!"); //should tell gamestate to update score, lightbox to update lights
			_LightBoxScript._myLightState = LightBox.LightState.green;
		}
		else if(collision.collider.tag == "Player1Collider"){
			Debug.Log("Hit player 1!");
			_LightBoxScript._myLightState = LightBox.LightState.red;
		}
	}

	void GetLightBoxScript(){
		_LightBoxScript = _LightBox.GetComponent<LightBox>();
	}
}
