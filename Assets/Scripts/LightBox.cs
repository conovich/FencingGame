using UnityEngine;
using System.Collections;

public class LightBox : MonoBehaviour {
	public Light _GreenLight;
	public Light _RedLight;

	private float colorTimer;
	private float colorTimerMax = 2.0f;

	public enum LightState{
		off,
		green,
		red,
		greenAndRed
	}

	public LightState _myLightState;

	// Use this for initialization
	void Start () {
		InitLightBox();
	}
	
	// Update is called once per frame
	void Update () {
		switch (_myLightState){
			case LightState.off:
				ResetLights();
				break;
			case LightState.green:
				ChangeLightColor(_GreenLight, Color.green);
				UpdateColorTimer();
				break;
			case LightState.red:
				ChangeLightColor(_RedLight, Color.red);
				UpdateColorTimer();
				break;
			case LightState.greenAndRed:
				ChangeLightColor(_GreenLight, Color.green);
				ChangeLightColor(_RedLight, Color.red);
				UpdateColorTimer();
				break;
		}
	}

	void InitLightBox(){
		_myLightState = LightState.off;
		ResetColorTimer();
	}

	void UpdateColorTimer(){
		colorTimer = colorTimer - Time.deltaTime;
		if(colorTimer <= 0){
			ResetColorTimer();
			_myLightState = LightState.off;
		}
	}

	void ResetColorTimer(){
		colorTimer = colorTimerMax;
	}

	public void ChangeLightColor(Light light, Color newColor){
		light.color = newColor;
	}

	void ResetLights(){
		ChangeLightColor(_GreenLight, Color.white);
		ChangeLightColor(_RedLight, Color.white);
	}
}
