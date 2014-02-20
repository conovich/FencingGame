using UnityEngine;
using System.Collections;

public class LightBox : MonoBehaviour {
	public Light _GreenLight;
	public Light _RedLight;

	public enum LightState{
		off,
		green,
		red,
		greenAndRed
	}

	public LightState _myLightState;

	// Use this for initialization
	void Start () {
		_myLightState = LightState.off;
	}
	
	// Update is called once per frame
	void Update () {
		switch (_myLightState){
			case LightState.off:
				ResetLights();
				break;
			case LightState.green:
				ChangeLightColor(_GreenLight, Color.green);
				break;
			case LightState.red:
				ChangeLightColor(_RedLight, Color.red);
				break;
			case LightState.greenAndRed:
				ChangeLightColor(_GreenLight, Color.green);
				ChangeLightColor(_RedLight, Color.red);
				break;
		}
	}

	void UpdateColorTimer(){

	}

	void ChangeLightColor(Light light, Color newColor){
		light.color = newColor;
	}

	void ResetLights(){
		ChangeLightColor(_GreenLight, Color.white);
		ChangeLightColor(_RedLight, Color.white);
	}
}
