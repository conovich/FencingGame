using UnityEngine;
using System.Collections;

public class SwordTip : MonoBehaviour {
	public GameObject _LightBox;
	public Transform _Hilt;
	public CrossHares _CrossHares;
	private LightBox _LightBoxScript;


	// Use this for initialization
	void Start () {
		GetLightBoxScript();
	}
	
	// Update is called once per frame
	void Update () {
		CheckTipOnTarget();
	}
	
	void OnCollisionEnter(Collision collision){
		Debug.Log("Hit something!");
		if(collision.collider.tag == "TipConstraint"){

		}
		if(collision.collider.tag == "Player2Collider"){ //an opponent hit! this needs to be more "if it's player two..." or something more versatile.
			Debug.Log("Hit player 2!"); //should tell gamestate to update score, lightbox to update lights
			P1HitP2();
		}
		else if(collision.collider.tag == "Player1Collider"){
			Debug.Log("Hit player 1!");
			P2HitP1();
		}
		if(collision.collider.tag == "TipTarget"){
			Debug.Log("Hit Tip Target: " + collision.gameObject.name);
			ColorLerper myLerper = collision.gameObject.GetComponent<ColorLerper>();
			myLerper.SetAlpha(1.0f);
		}
	}

	//instead should set all tiptarget colors back to normal when no more input???
	void OnCollisionExit(Collision collision){
		if(collision.collider.tag == "TipTarget"){
			ColorLerper myLerper = collision.gameObject.GetComponent<ColorLerper>();
			myLerper.SetAlpha(100.0f/255.0f);
		}
	}

	void P1HitP2(){
		if(GameState.Instance.CurrentState == GameState.State.inPlay){
			GameState.Instance.IncrementP1Score();
			_LightBoxScript._myLightState = LightBox.LightState.green;
			GameState.Instance.SetState(GameState.State.pointScored);
		}
	}

	void P2HitP1(){
		if(GameState.Instance.CurrentState == GameState.State.inPlay){
			GameState.Instance.IncrementP2Score();
			_LightBoxScript._myLightState = LightBox.LightState.red;
			GameState.Instance.SetState(GameState.State.pointScored);
		}
	}


	void GetLightBoxScript(){
		_LightBoxScript = _LightBox.GetComponent<LightBox>();
	}

	void CheckTipOnTarget(){
		_CrossHares.ShowOffTarget();
		RaycastHit myHit = new RaycastHit();
		Debug.DrawRay(_Hilt.position ,_Hilt.position + 20000*(transform.position - _Hilt.position).normalized, Color.red); 
		if(Physics.Raycast(_Hilt.position, (transform.position - _Hilt.position).normalized, out myHit)){ //first collision with tip box collider
			if(Physics.Raycast(myHit.transform.position, (transform.position - _Hilt.position).normalized, out myHit)){ //second collision with tip box collider
				if(Physics.Raycast(myHit.transform.position, (transform.position - _Hilt.position).normalized, out myHit)){	//third collision with player collider
					if(myHit.collider.tag == "Player2Collider"){
						_CrossHares.ShowOnTarget();
					}
				}
			}
		}

	}
}
