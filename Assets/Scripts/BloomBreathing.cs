using UnityEngine;
using System.Collections;

public class BloomBreathing: MonoBehaviour {
	public Breathing breathCalc;
	public FastBloom LeftEye;
	public FastBloom RightEye;
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
			if(LeftEye.intensity <= 2.5f && Mathf.Abs (newValue) <= ((max + min)/2))
			{
				LeftEye.blurSize = Mathf.Lerp (LeftEye.blurSize, 5.5f, Time.deltaTime * breathCalc.breathingSpeed);
				RightEye.blurSize = Mathf.Lerp (RightEye.blurSize, 5.5f, Time.deltaTime * breathCalc.breathingSpeed);

				LeftEye.intensity = Mathf.Lerp (LeftEye.intensity, 2.5f, Time.deltaTime * breathCalc.breathingSpeed);
				RightEye.intensity = Mathf.Lerp (RightEye.intensity, 2.5f, Time.deltaTime * breathCalc.breathingSpeed);
				
				LeftEye.threshhold = Mathf.Lerp (LeftEye.threshhold, 0.01f, Time.deltaTime * breathCalc.breathingSpeed);
				RightEye.threshhold = Mathf.Lerp (RightEye.threshhold, 0.01f, Time.deltaTime * breathCalc.breathingSpeed);
			}
			else if(LeftEye.intensity >= 1.0f && Mathf.Abs(newValue) > ((max + min)/2))
			{
				LeftEye.intensity = Mathf.Lerp (LeftEye.intensity, 1.0f, Time.deltaTime * breathCalc.breathingSpeed);
				RightEye.intensity = Mathf.Lerp (LeftEye.intensity, 1.0f, Time.deltaTime * breathCalc.breathingSpeed);

				LeftEye.blurSize = Mathf.Lerp (LeftEye.blurSize, 0.0f, Time.deltaTime * breathCalc.breathingSpeed);
				RightEye.blurSize = Mathf.Lerp (RightEye.blurSize, 0.0f, Time.deltaTime * breathCalc.breathingSpeed);

				LeftEye.threshhold = Mathf.Lerp (LeftEye.threshhold, 0.25f, Time.deltaTime * breathCalc.breathingSpeed);
				RightEye.threshhold = Mathf.Lerp (RightEye.threshhold, 0.25f, Time.deltaTime * breathCalc.breathingSpeed);
			}
		}
	}
}	