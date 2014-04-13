using UnityEngine;
using System.Collections;

public class Timer : MonoBehaviour {
	public GUIText TimerText;
	public bool TimeOut;

	public int DefaultMinutes = 3;
	public int DefaultSeconds = 0;

	public static int minutes = 3;
	public static int seconds = 0;
	float secondCounter = 1.0f;
	bool isCounting;

	// Use this for initialization
	void Start () {
		if(TimerText == null){
			TimerText = (GameObject.FindGameObjectWithTag("TimerText")).guiText;
			if(TimerText == null){
				Debug.Log("No TimerText.");
			}
		}

		if(TimerText != null){
			SetText();
		}
	}

	public void ResetTimer(){
		SetTimer(3, 0);
		SetText();
		isCounting = false;
		TimeOut = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(isCounting){
			UpdateCounting();
		}
	}

	void UpdateCounting(){
		secondCounter -= Time.deltaTime*10;
		if(secondCounter <= 0.0f){
			if(seconds > 0){
				seconds--;
				secondCounter = 1.0f;
			}
			if(seconds == 0){
				if(minutes > 0){
					minutes--;
					seconds = 59;
					secondCounter = 1.0f;
				}
				else{
					TimeOut = true;
				}
			}
		}
		SetText();
	}

	void SetText(){
		if(seconds > 9){
			TimerText.text = minutes.ToString() + ":" + seconds.ToString();
		}
		else{
			TimerText.text = minutes.ToString() + ":0" + seconds.ToString();
		}
	}

	public void StartOrStop(bool shouldStart){
		if(shouldStart){
			isCounting = true;
		}
		else if(!shouldStart){
			isCounting = false;
		}
	}

	void SetTimer(int newMinutes, int newSeconds){
		minutes = newMinutes;
		seconds = newSeconds;
		secondCounter = 1.0f;
		TimeOut = false;
	}
}
