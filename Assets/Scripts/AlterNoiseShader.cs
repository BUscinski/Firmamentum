using UnityEngine;
using System.Collections;

public class AlterNoiseShader : MonoBehaviour {
	public Breathing breathCalc;
	private float min;
	private float max;
	private float newValue;
	private float newBreathValue;
	private bool breathSensorInit = false;
	// Use this for initialization
	void Start () {
		min = 10000;
		max = 1;
		newValue = 0;
	}
	
	// Update is called once per frame
	void Update () {
		newBreathValue = breathCalc.GetMovementValue ();
		if(newBreathValue != 0 || breathSensorInit == true)
		{
			breathSensorInit = true;
			newValue = newBreathValue;
			if(newValue > max)
			{
				max = newValue;
			}
			if(newValue < min && newValue != 0)
			{
				min = newValue;
			}
			//	Debug.Log (newValue);
			//Debug.Log (breathCalc.GetMovementValue ());
			if(renderer.material.GetFloat ("_Amplitude") <= 6f && Mathf.Abs (newValue) > ((max + min)/2))
			{
				renderer.material.SetFloat ("_Amplitude", Mathf.Lerp (renderer.material.GetFloat ("_Amplitude"), 6f, Time.deltaTime * breathCalc.breathingSpeed));
			}
			else if(renderer.material.GetFloat ("_Amplitude") >=3f && Mathf.Abs(newValue) <= ((max + min)/2))
			{
				renderer.material.SetFloat ("_Amplitude", Mathf.Lerp (renderer.material.GetFloat ("_Amplitude"), 1.0f, Time.deltaTime * breathCalc.breathingSpeed));
			}
		}
	}
}	