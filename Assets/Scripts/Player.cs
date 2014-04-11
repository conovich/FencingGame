using UnityEngine;
using System.Collections;
using XInputDotNetPure;

public class Player : MonoBehaviour {
	public ControllerInput _MyControllers;

	//public items to be set in inspector
	public Transform _MyShadow;
	public GUIText _MyActionText;
	public Transform _MyReference;
	public Transform EngardeLine;

	private AnimationState _anim;
	private bool _inStartSequence;
	private Vector3 _changeInDistance;
	private Vector3 _currentDistance;
	private Transform _hips;
	private Player _thePlayer;

	private bool _opponentOn; //for toggling controller -- whether opponent should move or not
	
	public enum State{
		engarde,
		advance,
		retreat,
		lungeRecover,
		parryOne,
		parryFour,
		parrySix,
		parrySeven,
		parryEight,
		idle
	}
	public State _CurrentState;
	private State _animState;
	
	// Use this for initialization
	void Start (){
		ResetToEngardeLine(); 

		_opponentOn = true;

		_inStartSequence = true;
		animation.CrossFadeQueued("ReadyPosition");
		animation.CrossFadeQueued("Salute 1");
		animation.CrossFadeQueued("EngardePosition");
		
		_anim = animation["ReadyPosition"];
		SetState(State.engarde);
		SetAnimState(State.engarde);
		_changeInDistance = new Vector3(0.0f, 0.0f, 0.0f);
		_currentDistance = new Vector3(0.0f, 0.0f, 0.0f);
		_hips = transform.FindChild("Hips");
		if(_hips == null){
			_hips = transform.FindChild("Hips1");
		}
		
		_MyReference.position = _hips.position;
		
		GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
		if(playerObject){
			_thePlayer = playerObject.GetComponent<Player>();	
		}
		if(_thePlayer == null){
			Debug.Log("Player not initialized.");	
		}
	}

	public void Reset(){
		ResetToEngardeLine();
		_MyReference.position = _hips.position;
		PlayMyAnimation("EngardePosition", State.engarde);


	}

	void ResetToEngardeLine(){
		Vector3 startingPos = EngardeLine.position;
		startingPos.y = transform.position.y;
		startingPos.x = transform.position.x;
		transform.position = startingPos;

		//_MyReference.position = _hips.position;
		//_changeInDistance = _hips.position - _MyReference.position;
		_changeInDistance = new Vector3(0.0f, 0.0f, 0.0f);

		//to reset or not to reset?
		_currentDistance = new Vector3(0.0f, 0.0f, 0.0f); //DONT RESET THIS.



		_hips = transform.FindChild("Hips");
		if(_hips == null){
			_hips = transform.FindChild("Hips1");
		}
		
		_MyReference.position = _hips.position;
	}

	void FixedUpdate (){
		if(_MyShadow){
			Vector3 newPos = new Vector3(_hips.position.x, _MyShadow.position.y, _hips.position.z);
			_MyShadow.position = newPos;	
		}
		
		if(!animation.isPlaying && GameState.Instance.CurrentState == GameState.State.inPlay){
			_inStartSequence = false;
			SetState(State.idle);
		}

		//refactor so we don't need inStartSequence?
		if(!_inStartSequence && GameState.Instance.CurrentState == GameState.State.inPlay){
			_changeInDistance = _hips.position - _MyReference.position;
			//Debug.Log("hips: " + _hips.position.ToString());
			//Debug.Log("MyReferencePosition!: " + _MyReference.position.ToString());
			if(gameObject.tag == "Player"){
				GetInputPlayer();	
			}
			else if(gameObject.tag == "Player2"){
				GetInputPlayer2();
			}
			else if(gameObject.tag == "Opponent"){
				GetInputOpponent();
				if(!animation.isPlaying && _animState == State.retreat && _thePlayer._animState == State.lungeRecover){
					PlayMyAnimation("Advance 1", State.advance);	
				}
			}
		}
	}
	
	private void GetInputPlayer(){

		if(!animation.isPlaying){

			if(Input.GetKey (KeyCode.RightArrow) || (_MyControllers.prevState1.Buttons.RightShoulder == ButtonState.Pressed && _MyControllers.state1.Buttons.RightShoulder == ButtonState.Released)){
				PlayMyAnimation("Advance 1", State.advance);
				//Debug.Log("right bumper");
			}
			else if(Input.GetKey (KeyCode.LeftArrow) || (_MyControllers.prevState1.Buttons.LeftShoulder == ButtonState.Pressed && _MyControllers.state1.Buttons.LeftShoulder == ButtonState.Released)){
				PlayMyAnimation("Retreat", State.retreat);
				//Debug.Log("left bumper");
			}
			else if(Input.GetKey (KeyCode.Space) || (_MyControllers.prevState1.Buttons.A == ButtonState.Pressed && _MyControllers.state1.Buttons.A == ButtonState.Released)){
				PlayMyAnimation("LungeRecover", State.lungeRecover);
			}
			else if(Input.GetKey (KeyCode.Q) || (_MyControllers.prevState1.Buttons.Y == ButtonState.Pressed && _MyControllers.state1.Buttons.Y == ButtonState.Released)){
				PlayMyAnimation("ParryOne", State.parryOne);
			}


			//not yet in the FENCER animations
			/*else if(Input.GetKey (KeyCode.Keypad4)){
				PlayMyAnimation("ParryFourNoExt", State.lungeRecover);
			}*/

			/*else if(Input.GetKey (KeyCode.W) || Input.GetButtonDown("B Button")){
				PlayMyAnimation("ParrySix", State.parrySix);
			}
			else if(Input.GetKey (KeyCode.E)){
				PlayMyAnimation("ParrySeven", State.parrySeven);
			}
			else if(Input.GetKey (KeyCode.R) || Input.GetButtonDown("X Button")){
				PlayMyAnimation("ParrySeven", State.parrySeven);
			}*/

			//not working with FENCER
			/*
			else if(Input.GetKey (KeyCode.R)){
				PlayMyAnimation("ParryEight", State.lungeRecover);
			}*/
			else{
				SetState(State.idle);
			}
		}
	}	

	private void GetInputPlayer2(){

		if(!animation.isPlaying){
			if(Input.GetKey (KeyCode.D) || (_MyControllers.prevState2.Buttons.LeftShoulder == ButtonState.Pressed && _MyControllers.state2.Buttons.LeftShoulder == ButtonState.Released)){
				PlayMyAnimation("Advance 1", State.advance);
				Debug.Log("right bumper");
			}
			else if(Input.GetKey (KeyCode.A) || (_MyControllers.prevState2.Buttons.RightShoulder == ButtonState.Pressed && _MyControllers.state2.Buttons.RightShoulder == ButtonState.Released)){
				PlayMyAnimation("Retreat", State.retreat);
				Debug.Log("left bumper");
			}
			else if((_MyControllers.prevState2.Buttons.A == ButtonState.Pressed && _MyControllers.state2.Buttons.A == ButtonState.Released)){
				PlayMyAnimation("LungeRecover", State.lungeRecover);
			}
			else if((_MyControllers.prevState2.Buttons.Y == ButtonState.Pressed && _MyControllers.state2.Buttons.Y == ButtonState.Released)){
				PlayMyAnimation("ParryOne", State.parryOne);
			}
			else{
				SetState(State.idle);
			}
		}
	}


	
	private void GetInputOpponent(){
		if(Input.GetButtonDown("Start Button")){
			_opponentOn = !_opponentOn;
		}
		if(!animation.isPlaying && _opponentOn){
			if(_thePlayer._CurrentState == Player.State.advance){
				PlayMyAnimation("Retreat", State.retreat);
			}
			else if(_thePlayer._CurrentState == Player.State.retreat){
				PlayMyAnimation("Advance 1", State.advance);
			}
			else if(_thePlayer._CurrentState == Player.State.lungeRecover){
				PlayMyAnimation("DoubleRetreat", State.retreat);
			}
			else if(_thePlayer._CurrentState == Player.State.idle){
				
			}
		}
	}
	
	private void PlayMyAnimation(string myAnimation, State newState){
		if(!animation.isPlaying){
			animation.Play(myAnimation);
			//animation.Blend(myAnimation,0.2f, 1.0f);
			_anim = animation[myAnimation];
			SetState(newState);
			SetAnimState(newState);
			
		
			_MyReference.position = _hips.position;
			_currentDistance += new Vector3(0.0f, 0.0f, _changeInDistance.z);
			//Debug.Log(_currentDistance.ToString());
			transform.position += _currentDistance;
		}
		else{
			animation.Stop(_anim.name);
			animation.Play (myAnimation);
			_anim = animation[myAnimation];
			SetState(newState);
			
			_MyReference.position = _hips.position;
			_currentDistance += new Vector3(0.0f, 0.0f, _changeInDistance.z);
			//Debug.Log(_currentDistance.ToString());
			transform.position += _currentDistance;
		}
	}

	void SetState(Player.State newState){
		_CurrentState = newState;
		//SetMyActionText();
	}

	void SetAnimState(Player.State newState){
		_animState = newState;
	}

	void SetMyActionText(){
		_MyActionText.text = _CurrentState.ToString();
	}

	public void SetMyActionText(string action){
		_MyActionText.text = action;
	}
}
