using UnityEngine;
using System.Collections;
using Uniduino;

public class Breathing : MonoBehaviour {
	
	public Arduino arduino;
	
	public int pin = 0;
	public int pinValue;
	private int previousPinValue;
	public DisplayColor thePointCloud;
	private bool lockPush;
	
	private GameObject cube;
	
	void Start( )
	{
		lockPush = false;
		//arduino = Arduino.global;
		arduino.Setup(ConfigurePins);
		previousPinValue = 0;
	}
	
	void ConfigurePins( )
	{
		arduino.pinMode(pin, PinMode.ANALOG);
		arduino.reportAnalog(pin, 1);
	}
	
	void Update () 
	{       
		if(Input.GetKeyDown (KeyCode.Escape)){
			Application.Quit ();
		}
		pinValue = arduino.analogRead(pin);
		Debug.Log (pinValue);
		if(pinValue > previousPinValue)
		{
			thePointCloud.PushBody ();
		}
		else
		{
			thePointCloud.PullBody ();
		}
		previousPinValue = pinValue;
	}
//	IEnumerator LockPush()
//	{
//		yield return new WaitForSeconds(0.5f);
//		lockPush = true;
//	}
}