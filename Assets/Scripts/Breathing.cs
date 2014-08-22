using UnityEngine;
using System.Collections;
using Uniduino;

public class Breathing : MonoBehaviour {
	
	public Arduino arduino;
	

	private float MovementValue;
	private int tempMovementValue;
	public int pin = 0;
	public int pinValue;
	private int previousPinValue;
	public DisplayColor thePointCloud;
	private bool lockPush;
	private int iterator;
	private GameObject cube;
	
	void Start( )
	{
		lockPush = false;
	//	arduino = Arduino.global;
		arduino.Setup(ConfigurePins);
		previousPinValue = 0;
		iterator = 0;
	}
	
	void ConfigurePins( )
	{
		arduino.pinMode(pin, PinMode.ANALOG);
		arduino.reportAnalog(pin, 1);
	}
	
	void Update () 
	{       
		if(iterator == 0)
		{
			tempMovementValue = 0;
		}
		if(Input.GetKeyDown (KeyCode.Escape)){
			Application.Quit ();
		}
		pinValue = arduino.analogRead(pin);
		if(pinValue > previousPinValue)
		{
			thePointCloud.PushBody ();
		}
		else
		{
			thePointCloud.PullBody ();
		}
	//	Debug.Log ("Pin value: " + pinValue);

		tempMovementValue += pinValue;
		iterator++;
		if(iterator == 5)
		{
			MovementValue = tempMovementValue/iterator;
//			Debug.Log ("Updating Movement value to: " + tempMovementValue);
			iterator = 0;
		}
		previousPinValue = pinValue;
	}
	public float GetMovementValue()
	{
		return MovementValue;
	}
//	IEnumerator LockPush()
//	{
//		yield return new WaitForSeconds(0.5f);
//		lockPush = true;
//	}
}