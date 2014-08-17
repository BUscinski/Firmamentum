using UnityEngine;
using System.Collections;
using System.IO.Ports;

public class ArduinoInput : MonoBehaviour {
	SerialPort stream= new SerialPort("COM4", 9600);
	string distance;
	public BodyMovement bodyParticles;
	




	// Use this for initialization
	void Start () {
		try{
			stream.Open();
			stream.DataReceived += DataReceivedHandler;
			stream.ErrorReceived += DataErrorReceivedHandler;
//			StartCoroutine (StreamData());
//			Debug.Log(stream.DataReceived += DataReceivedHandler);
		}
		catch(System.Exception e){
			Debug.Log("Could not open serial port: " + e.Message);
			
		}
		
	}
	IEnumerator StreamData()
	{
		while(true)
		{
			yield return new WaitForSeconds(0.25f);
			Debug.Log (stream.ReadLine ().ToString ());
	
		}
	}
	void Update()
	{
		string currentBreathData = stream.ReadLine ();
		float value = float.Parse (currentBreathData);
		if(value > 200.0f){
			bodyParticles.PushBody();
		}
		else if (value > 100.0f && value < 200.0f){
			bodyParticles.PullBody();
		}
		else {
			bodyParticles.StopBody();
		}


		//Debug.Log (stream.ReadLine ().ToString ());
	}

	private void DataReceivedHandler(
		object sender,
		SerialDataReceivedEventArgs e)
	{
		Debug.Log("WHAT THE FUCK?");
		SerialPort sp = (SerialPort)sender;
		string distance = sp.ReadLine();
		Debug.Log(distance);
	}
	private void DataErrorReceivedHandler(object sender, 
	                                      SerialErrorReceivedEventArgs e)
	{
		Debug.Log("Serial port error:" + e.EventType.ToString ("G"));            
	}
}