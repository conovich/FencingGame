using UnityEngine;
using System.Collections;

public class Timer : MonoBehaviour {
	public GUIText TimerText;
	public bool TimeOut;

	int minutes;
	int seconds;
	float secondCounter = 1.0f;
	bool isCounting;

	// Use this for initialization
	void Start () {
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
		secondCounter -= Time.deltaTime;
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
