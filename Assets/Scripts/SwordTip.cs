using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SwordTip : MonoBehaviour {
	public GameObject _LightBox;
	public Transform _Hilt;
	public CrossHares _CrossHares;

	public GameObject _MyPlayer;

	public List<string> _MySequence;
	public bool _SwordInput;

	private LightBox _LightBoxScript;

	public SwordAI _SwordAI;


	// Use this for initialization
	void Start () {
		//comment out for the move-mapping test scene
		GetLightBoxScript();
	}
	
	// Update is called once per frame
	void Update () {
		CheckTipOnTarget();
	}
	
	void OnCollisionEnter(Collision collision){
		if(collision.collider.tag == "TipConstraint"){

		}
		if(collision.collider.tag == "Player2Collider"){ //an opponent hit! this needs to be more "if it's player two..." or something more versatile.
			//should tell gamestate to update score, lightbox to update lights
			P1HitP2();
			if(GameState.playerSelection == GameState.PlayerSelection.singlePlayer && _MyPlayer.tag == "Player2"){
				if(_SwordAI.lastMotionExecuted != ""){
					_SwordAI.UpdateSuccessfulMove(_SwordAI.lastMotionExecuted, -1);
				}
			}
		}
		else if(collision.collider.tag == "Player1Collider"){
			P2HitP1();
			if(GameState.playerSelection == GameState.PlayerSelection.singlePlayer && _MyPlayer.tag == "Player2"){
				if(_SwordAI.lastMotionExecuted != ""){
					_SwordAI.UpdateSuccessfulMove(_SwordAI.lastMotionExecuted, 1);
				}
			}
		}
		if(collision.collider.tag == "TipTargetP1" && _MyPlayer.tag == "Player"){
			ChangeTipTargetAlpha(1.0f, collision.gameObject);
			if(_SwordInput){
				_MySequence.Add(collision.gameObject.name);
			}
		}
		else if(collision.collider.tag == "TipTargetP2" && _MyPlayer.tag == "Player2"){
			ChangeTipTargetAlpha(1.0f, collision.gameObject);
			if(_SwordInput){
				_MySequence.Add(collision.gameObject.name);
			}
		}
	}

	public void ClearInputList(){
		_MySequence.Clear();
	}

	void OnCollisionExit(Collision collision){
		if(collision.collider.tag == "TipTargetP1" && _MyPlayer.tag == "Player"){
			ChangeTipTargetAlpha(100.0f/255.0f, collision.gameObject);
		}
		if(collision.collider.tag == "TipTargetP2" && _MyPlayer.tag == "Player2"){
			ChangeTipTargetAlpha(100.0f/255.0f, collision.gameObject);
		}

		//use the following for the move-mapping test scene
		/*
		if(collision.collider.tag == "TipTargetP1"){
			ChangeTipTargetAlpha(1.0f, collision.gameObject);
			if(_SwordInput){
				_MySequence.Add(collision.gameObject.name);
			}
		}
		else if(collision.collider.tag == "TipTargetP2"){
			ChangeTipTargetAlpha(1.0f, collision.gameObject);
			if(_SwordInput){
				_MySequence.Add(collision.gameObject.name);
			}
		}
		*/
	}

	void ChangeTipTargetAlpha(float newAlpha, GameObject target){
		ColorLerper myLerper = target.GetComponent<ColorLerper>();
		myLerper.SetAlpha(newAlpha);
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
