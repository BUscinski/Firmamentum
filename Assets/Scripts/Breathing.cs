using UnityEngine;
using System.Collections;
using Uniduino;

public class Breathing : MonoBehaviour {
	
	private Arduino arduino;
	
	public float breathingSpeed;
	private float MovementValue;
	private int tempMovementValue;
	public int pin = 0;
	public int pinValue;
	private int previousPinValue;
	public DisplayColor thePointCloud;
	private int iterator;
	private GameObject cube;
	private bool currentlyBreathingOut;
	private int numBreathsTaken;
	private int breathingIn;
	private float min;
	private float max;
	private bool breathSensorInitialized;
	void Start( )
	{
		breathSensorInitialized = false;
		min = 5000;
		max = 0;
		arduino = GameObject.Find ("Uniduino(Clone)").GetComponent<Arduino>();
		breathingIn = 0;
	//	arduino = Arduino.global;
		arduino.Setup(ConfigurePins);
		previousPinValue = 0;
		iterator = 0;
		StartCoroutine (InitiateBreathSensor());
	}
	
	void ConfigurePins( )
	{
		arduino.pinMode(pin, PinMode.ANALOG);
		arduino.reportAnalog(pin, 1);
	}
	
	void Update () 
	{       
		if(breathSensorInitialized)
		{
		//	Debug.Log ("Num Breaths Taken" + numBreathsTaken);
			if(iterator == 0)
			{
				tempMovementValue = 0;
			}
			if(Input.GetKeyDown (KeyCode.Escape)){
				Application.Quit ();
			}
			pinValue = arduino.analogRead(pin);
		//	if(pinValue > previousPinValue)
			if(MovementValue > ((min + max)/2))
			{
				currentlyBreathingOut = true;
				breathingIn = 0;
				thePointCloud.PushBody ();
			}
			else
			{
				breathingIn++;
				if(breathingIn >= 10)
				{
					currentlyBreathingOut = false;
					breathingIn = 0;
				}
				thePointCloud.PullBody ();
			}
		//	Debug.Log ("Pin value: " + pinValue);
	
			tempMovementValue += pinValue;
			iterator++;
			if(iterator == 5)
			{
				MovementValue = tempMovementValue/iterator;
//				Debug.Log ("Updating Movement value to: " + tempMovementValue);
				if(MovementValue > max)
				{
					max = MovementValue;
				}
				if(MovementValue < min)
				{
					min = MovementValue;
				}
				if(MovementValue < (min + max)/2 && currentlyBreathingOut == false)
				{
					numBreathsTaken++;
				}
				iterator = 0;
			}
			previousPinValue = pinValue;
		}
	}
	public float GetMovementValue()
	{
		return MovementValue;
	}
	private IEnumerator breathOut()
	{
		currentlyBreathingOut = true;
		yield return new WaitForSeconds(1.0f);
		currentlyBreathingOut = false;
	}
	public int GetNumBreathsTaken()
	{
		return numBreathsTaken;
	}
	public void resetNumBreathsTaken()
	{
		numBreathsTaken = 0;
	}
	IEnumerator InitiateBreathSensor()
	{
		yield return new WaitForSeconds(10.0f);
		breathSensorInitialized = true;
	}
}