using UnityEngine;
using System.Collections;
using Uniduino;

public class Breathing : MonoBehaviour {
	private enum breathStatus
	{
		breathingIn,
		breathingOut
	}

	private breathStatus currentBreathStatus;
	private breathStatus previousBreathStatus;

	private Arduino arduino;
	private bool personThere;
	public float breathingSpeed;
	private float MovementValue;
	private int tempMovementValue;
	public int pin = 0;
	public int pinValue;
	private int previousPinValue;
	public DisplayColor thePointCloud;
	private int iterator;
	private GameObject cube;
	private bool breathSwitcher;
	private int numBreathsTaken;
	private int breathingOut;
	private int breathingIn;
	private float min;
	private float max;
	private bool breathSensorInitialized;
	void Start( )
	{
		personThere = true;
		breathSensorInitialized = false;
		min = 5000;
		max = 0;
		arduino = GameObject.Find ("Uniduino(Clone)").GetComponent<Arduino>();
		breathingOut = 0;
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
//			Debug.Log ("Num Breaths Taken" + numBreathsTaken);
			//Debug.Log (pinValue);
			if(iterator == 0)
			{
				tempMovementValue = 0;
			}
			if(Input.GetKeyDown (KeyCode.Escape)){
				Application.Quit ();
			}
			pinValue = arduino.analogRead(pin);
			if(pinValue >=200f)
			{
				personThere = true;
			}
			//	if(pinValue > previousPinValue)
			if(MovementValue > ((min + max)/2))
			{
				breathingOut++;
				thePointCloud.PushBody ();
			}
			else
			{
				breathingIn++;
				thePointCloud.PullBody ();
			}
		//	Debug.Log ("Pin value: " + pinValue);
	
			tempMovementValue += pinValue;
			iterator++;
			if(iterator >= 5)
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
				iterator = 0;

			}
			if(breathingIn + breathingOut >= 5)
			{
				if(breathingIn > breathingOut)
				{
					currentBreathStatus = breathStatus.breathingIn;
				}
				if(breathingOut > breathingIn)
				{
					currentBreathStatus = breathStatus.breathingOut;
					//breathingOut
				}
				breathingIn = 0;
				breathingOut = 0;
				if(currentBreathStatus != previousBreathStatus && currentBreathStatus == breathStatus.breathingIn)
				{
					numBreathsTaken++;
				}
				previousBreathStatus = currentBreathStatus;
			}

			previousPinValue = pinValue;
		}
	}
	public float GetMovementValue()
	{
		return MovementValue;
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
		yield return new WaitForSeconds(5.0f);
		breathSensorInitialized = true;
	}
	public bool getPersonThere()
	{
		return personThere;
	}
}