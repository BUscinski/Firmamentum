using UnityEngine;
using System.Collections;
using System.IO.Ports;
using Uniduino;

public class ArduinoInput : MonoBehaviour {
	//SerialPort stream = new SerialPort("COM5", 9600);
	public Arduino arduino;

	public int pin = 0;
	float max = 0f;
	float min = 1000f;
	int breathValue = 0;
	string distance;
	public DisplayColor bodyParticles;

	// Use this for initialization
	void Start () {
		print ("Is this running?");
		arduino = Arduino.global;
		arduino.Setup (ConfigurePins);
		
	}
	void Update()
	{
		//		string currentBreathData = stream.ReadLine ();
		//		stream.ReadTimeout = 55;
		Debug.Log ("Its running");
		breathValue = arduino.analogRead (pin);
		//		if(breathValue > max)
		//		{
		//			max = value;
		//		}
		//		if(breathValue < min)
		//		{
		//			min = value;
		//		}
		float normalizedValue = breathValue/(max - min);
		Debug.Log (breathValue);
		if(breathValue > 400f){
			bodyParticles.PushBody();
		}
		else{
			bodyParticles.PullBody();
		}
		//		else {
		//			bodyParticles.StopBody();
		//		}
		
		
		//Debug.Log (stream.ReadLine ().ToString ());
	}
	void ConfigurePins()
	{
		arduino.pinMode (0, PinMode.ANALOG);
		arduino.reportAnalog (0,1);
	}



}