using UnityEngine;
using System.Collections;

public class SwordTip : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	//not working -- collider and joint 2 not moving/rotating
	void OnCollisionEnter(Collision collision){
		Debug.Log("Hit something!");
		if(collision.collider.tag == "opponent"){ //an opponent hit! this needs to be more "if it's player two..." or something more versatile.
			Debug.Log("Hit opponent!"); //should tell gamestate to update score, lightbox to update lights
		}
	}
}
